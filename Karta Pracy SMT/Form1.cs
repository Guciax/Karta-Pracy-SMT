using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public partial class MainForm : Form
    {
        Dictionary<string, EfficiencyStructure> efficiencNormyPerModel = new Dictionary<string, EfficiencyStructure>();
        List<LedLeftovers> ledLeftSaveBuffer = new List<LedLeftovers>();
        Dictionary<string, EfficiencyNormsPerModel> normPerModel = new Dictionary<string, EfficiencyNormsPerModel>();
        double normlotsPerShift = 15;

        public MainForm()
        {
            InitializeComponent(); 
        }

        bool checkMirae = false;
        private void MainForm_Load(object sender, EventArgs e)
        {
            string chkMirae = ConfigurationManager.AppSettings["CheckMirae"];
            //checkMirae = Convert.ToBoolean(chkMirae);
            labelLine.Text = "Linia: " + smtLine;
            AddRecordsFromDb();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "Karta pracy SMT " + version;
            panelRight.Width = this.Width / 2;
            pbChart.Width = this.Width-(panelRight.Location.X + 300);
            dataGridView3DaysInfo.Width = 380;// panelLeft.Width - button1.Width - pictureBoxShifts.Width - 10;
            pictureBoxShifts.Width = panelLeft.Width - (dataGridView3DaysInfo.Width + 100);
            dataGridView3DaysInfo.ScrollBars = ScrollBars.None;

#if DEBUG
                button2.Visible=true;
            EfficiencyTimer.Interval = 1000;
            dataGridView3DaysInfo.ReadOnly = false;
            dataGridView3DaysInfo.EditMode = DataGridViewEditMode.EditOnKeystroke;
#endif

            normPerModel = EfficiencyTools.CreateEfficiencyNorm();
            EfficiencyTick();
            if (smtLine == "SMT7" || smtLine == "SMT8" || smtLine == "SMT1")
            {
                normlotsPerShift = 11;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int notSavedQty = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ColumnSaved"].Style.BackColor == Color.Red)
                {
                    notSavedQty++;
                }

            }

            if (notSavedQty < 2)
            {
                NewLotForm fmNewLot = new NewLotForm(this, dataGridView1, miraeCurrentProgram, checkMirae);
                fmNewLot.ShowInTaskbar = false;
                fmNewLot.ShowDialog();
            }
            else
            {
                MessageBox.Show("Maksymalnie dwa nieskończone LOTy.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 & e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridView1.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnButtonLed")
                    {
                        LedLeftovers clickedLeftovers = (LedLeftovers)cell.Tag;
                        Add_LED_leftovers editLeftovers = new Add_LED_leftovers(dataGridView1, cell, ledLeftSaveBuffer);
                        editLeftovers.ShowInTaskbar = false;
                        editLeftovers.ShowDialog();
                    }
                    
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnQualityCheck")
                    {
                        //DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        ChangeOverConfirmationCard changeOverForm = new ChangeOverConfirmationCard(cell);
                        changeOverForm.ShowDialog();
                    }
                }
            }
        }

        DateTime miraeFileLatModificationDate;
        public string miraeCurrentProgram = ""; 
        private void timerMiraeStalker_Tick(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["MiraePath"];
            if (System.IO.File.Exists(filePath))
            {
                DateTime lastModifiedCurrentRading = System.IO.File.GetLastWriteTime(filePath);
                if (lastModifiedCurrentRading!= miraeFileLatModificationDate)
                {
                    miraeCurrentProgram = Tools.GetCurrenMiraeProgram();
                }
            }
        }

        private bool IsRowReadyToBeSaved(DataGridViewRow row)
        {
            bool result = true;
            if (Tools.getCellValue( row.Cells["ColumnQualityCheck"]) == "BRAK") result= false;
            if (Tools.getCellValue(row.Cells["goodQty"]) == "") result = false;
            if (Tools.getCellValue(row.Cells["ColumnButtonLed"]) == "BRAK") result = false;

            //Debug.WriteLine("Row " + row.Index + " rdy to save: " + result);
            return result;
        }


        bool suspendCellVelueChangedEvent = false;
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!suspendCellVelueChangedEvent)
            {
                int ngIndex = dataGridView1.Columns.IndexOf(dataGridView1.Columns["Ng"]);
                int scrapIndex = dataGridView1.Columns.IndexOf(dataGridView1.Columns["Scrap"]);
               // Debug.WriteLine("Cell value changed: " + e.RowIndex + " " + e.ColumnIndex);
                //calculate good qty
                if (dataGridView1.Rows.Count > 0 & (e.ColumnIndex == ngIndex || e.ColumnIndex == scrapIndex))
                {
                    int rowIndex = e.RowIndex;

                    DataGridViewCell ngCell = dataGridView1.Rows[rowIndex].Cells["Ng"];
                    DataGridViewCell scrapCell = dataGridView1.Rows[rowIndex].Cells["Scrap"];
                    
                    double ngValue = -9999;
                    double scrapValue = -9999;

                    if (ngCell.Value != null)
                        if (!(double.TryParse(ngCell.Value.ToString(), out ngValue)))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[ngIndex].Value = null;
                            ngValue = -9999;
                        }

                    if (dataGridView1.Columns["Scrap"].Visible)
                    {
                        if (scrapCell.Value != null)
                            if (!(double.TryParse(scrapCell.Value.ToString(), out scrapValue)))
                            {
                                dataGridView1.Rows[e.RowIndex].Cells[scrapIndex].Value = null;
                                scrapValue = -9999;
                            }
                    }
                    else
                    {
                        scrapValue = 0;
                    }

                    if (ngValue > -9999 & scrapValue >-9999 & dataGridView1.Rows[e.RowIndex].Cells["ColumnQty"].Value!=null)
                    {
                        double orderedQty = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["ColumnQty"].Value.ToString());
                        double goodQty = orderedQty - ngValue - scrapValue;
                        dataGridView1.Rows[e.RowIndex].Cells["goodQty"].Value = goodQty;
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["goodQty"].Value = "";
                    }
                }

                //saving finished LOT
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //Debug.WriteLine("Checking chkBox row:" + row.Index + " " + Convert.ToBoolean(row.Cells["ColumnSaved"].Value));
                    if (!Convert.ToBoolean(row.Cells["ColumnSaved"].Value))
                    {
                        if (IsRowReadyToBeSaved(row))
                        {
                            //Debug.WriteLine("Saving row:" + row.Index);
                            DataGridViewCheckBoxCell ch = (DataGridViewCheckBoxCell)row.Cells["ColumnSaved"];
                            suspendCellVelueChangedEvent = true;
                            lotFinished(ch, row.Index);
                            suspendCellVelueChangedEvent = false;

                            DateTime startDate = DateTime.ParseExact(row.Cells["StartDate"].Value.ToString(), "HH:mm:ss dd-MM-yyyy", CultureInfo.InvariantCulture);
                            DateTime endDate = DateTime.ParseExact(row.Cells["EndDate"].Value.ToString(), "HH:mm:ss dd-MM-yyyy", CultureInfo.InvariantCulture);
                            string firstPieceCheck = "";

                            LedLeftovers ledLeftovers = (LedLeftovers)row.Cells["ColumnButtonLed"].Tag;
                            string leftovers = ledLeftovers.RankA[0].ID + ":" + ledLeftovers.RankA[0].Nc12 + ":" + ledLeftovers.RankA[0].Qty + "|" + ledLeftovers.RankB[0].ID + ":" + ledLeftovers.RankB[0].Nc12 + ":" + ledLeftovers.RankB[0].Qty;
                            for (int i = 0; i < ledLeftovers.RankA.Count; i++)
                            {
                                if (i > 0)
                                {
                                    leftovers += "#" + ledLeftovers.RankA[i].ID + ":" + ledLeftovers.RankA[i].Nc12 + ":" + ledLeftovers.RankA[i].Qty + "|" + ledLeftovers.RankB[i].ID + ":" + ledLeftovers.RankB[i].Nc12 + ":" + ledLeftovers.RankB[i].Qty;
                                }
                            }

                            if (row.Cells["ColumnQualityCheck"].Tag != null)
                            {
                                firstPieceCheck = row.Cells["ColumnQualityCheck"].Tag.ToString();
                            }

                            string scrap = "0";
                            if (dataGridView1.Columns["Scrap"].Visible) scrap = row.Cells["Scrap"].Value.ToString();

                            SqlOperations.SaveRecordToDb(
                                startDate,
                                endDate,
                                smtLine,
                                row.Cells["Operator"].Value.ToString(),
                                row.Cells["ColumnLot"].Value.ToString(),
                                row.Cells["ColumnModel"].Value.ToString(),
                                row.Cells["goodQty"].Value.ToString(),
                                row.Cells["Ng"].Value.ToString(),
                                scrap,
                                firstPieceCheck,
                                leftovers);
                           // Debug.WriteLine("Saved");
                            Tools.CleanUpDgv(dataGridView1);
                        }
                    }
                }
                dataGridView1.HorizontalScrollingOffset = 0;
            }
        }
        string smtLine= ConfigurationManager.AppSettings["SMTLine"];
        private void lotFinished(DataGridViewCheckBoxCell checkBoxCell, int rowIndex)
        {
            dataGridView1.Rows[rowIndex].Cells["EndDate"].Value = System.DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");
            dataGridView1.Rows[rowIndex].Cells["ColumnSaved"].Style.BackColor = Color.White;
            checkBoxCell.Value = true;
        }

        string currentStencilQR = "";
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!suspendCellVelueChangedEvent)
            {
                if(dataGridView1.Rows[e.RowIndex].Cells["ColumnQualityCheck"].Value != null)
                {
                    currentStencilQR = dataGridView1.Rows[e.RowIndex].Cells["Stencil"].Value.ToString();
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Stencil"].Value = currentStencilQR;
                }
            }
           // Debug.WriteLine(dataGridView1.Rows.Count);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Insert(0, 1);
            //dataGridView1.Rows[0].Cells["StartDate"].Value = System.DateTime.Now.ToLongTimeString();
            //dataGridView1.Rows[0].Cells["ColumnSaved"].Style.BackColor = Color.Red;
            //Tools.CleanUpDgv(dataGridView1);
            // dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            // labelWasteLed.Text = EfficiencyTools.CalculateWasteLevel(dataGridView1, normPerModel) + "%";
            // labelLotsThisShift.Text = EfficiencyTools.HowManyLotsThisShift(dataGridView1).ToString();
            //EfficiencyTools.QuantityDictionaryToGrid(dataGridView2, EfficiencyTools.quantityPerDayPerShift(SqlOperations.GetSmtRecordsFromDbQuantityOnly(5, smtLine)));
            //Charting.DrawEfficiencyChart(pbChart, 80);
            Charting.DrawDayByDayEfficiency(dataGridView3DaysInfo, pictureBoxShifts);
        }

        bool saveLedLeftWorking = false;
        private void timerLedLeftSave_Tick(object sender, EventArgs e)
        {
            if (ledLeftSaveBuffer.Count>0 & !saveLedLeftWorking)
            {
                saveLedLeftWorking = true;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    List<int> indexToDelete = new List<int>();

                    for (int i = 0; i < ledLeftSaveBuffer.Count; i++)
                    {
                        if (SqlOperations.UpdateLedLeftovers(ledLeftSaveBuffer[i]))
                        {
                            indexToDelete.Add(i);
                        }
                    }

                    foreach (var ind in indexToDelete)
                    {
                        ledLeftSaveBuffer.RemoveAt(ind);
                    }

                    //Debug.WriteLine("done...");
                    saveLedLeftWorking = false;
                }).Start();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveLedLeftWorking)
            {
                e.Cancel = true;
                MessageBox.Show("Trwa zapis do bazy danych, czekaj....");
            }
        }

        public static LedLeftovers CreateLedLeftovers(string sqlLeftovers, string lotNo, Dictionary<string, string[]> lotToRankABQty)
        {

            string leftoverRaw = sqlLeftovers;
            string[] leftoverReels = leftoverRaw.Split('#');

            List<string[]> rankaAlist = new List<string[]>();
            List<string[]> rankaBlist = new List<string[]>();

            List<RankStruc> rankAList = new List<RankStruc>();
            List<RankStruc> rankBList = new List<RankStruc>();

            foreach (var item in leftoverReels)
            {
                string[] rankA = item.Split('|')[0].Split(':');
                string[] rankB = item.Split('|')[1].Split(':');

                double qtyA = double.Parse(rankA[2]);
                double qtyB = double.Parse(rankB[2]);

                rankAList.Add(new RankStruc(lotToRankABQty[lotNo][0], rankA[0], rankA[1], qtyA));
                rankBList.Add(new RankStruc(lotToRankABQty[lotNo][1], rankB[0], rankB[1], qtyB));
            }

            return new LedLeftovers(rankAList, rankBList);
        }

        private void AddRecordsFromDb()
        {
            suspendCellVelueChangedEvent = true;
            //DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED
            DataTable sqlTable = SqlOperations.GetSmtRecordsFromDb(22, smtLine);
            List<string> lotsList = new List<string>();
            foreach (DataRow row in sqlTable.Rows)
            {
                lotsList.Add(row["NrZlecenia"].ToString().Trim());
            }

            Dictionary<string, string[]> lotToRankABQty = SqlOperations.lotToRankABQty(lotsList.ToArray());
            dataGridView1.SuspendLayout();
            foreach (DataRow row in sqlTable.Rows)
            {
                dataGridView1.Rows.Insert(0);
                string lotNo = row["NrZlecenia"].ToString().Trim();

                LedLeftovers ledLeft = CreateLedLeftovers(row["KoncowkiLED"].ToString(), lotNo, lotToRankABQty);

                //string leftoverRaw = row["KoncowkiLED"].ToString();
                //string[] leftoverReels = leftoverRaw.Split('#');
                //string lotNo = row["NrZlecenia"].ToString().Trim();

                //List<string[]> rankaAlist = new List<string[]>();
                //List<string[]> rankaBlist = new List<string[]>();

                //List<RankStruc> rankAList = new List<RankStruc>();
                //List<RankStruc> rankBList = new List<RankStruc>();

                //foreach (var item in leftoverReels)
                //{
                //    string[] rankA = item.Split('|')[0].Split(':');
                //    string[] rankB = item.Split('|')[1].Split(':');

                //    double qtyA = double.Parse(rankA[2]);
                //    double qtyB = double.Parse(rankB[2]);

                //    rankAList.Add(new RankStruc(lotToRankABQty[lotNo][0], rankA[0], rankA[1], qtyA));
                //    rankBList.Add(new RankStruc(lotToRankABQty[lotNo][1], rankB[0], rankB[1], qtyB));
                //}

                //LedLeftovers ledLeft = new LedLeftovers(rankAList, rankBList);
                DataGridViewCheckBoxCell chbCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[0].Cells[0];
                chbCell.Value = true;

                dataGridView1.Rows[0].Cells[1].Value = lotNo;
                string connQty = Tools.GetNumberOfConnectors(row["Model"].ToString().Trim());
                dataGridView1.Rows[0].Cells[3].Value = connQty;
                if (connQty == "4")
                {
                    dataGridView1.Rows[0].Cells[3].Style.ForeColor = Color.White;
                    dataGridView1.Rows[0].Cells[3].Style.BackColor = Color.DarkCyan;
                }
                dataGridView1.Rows[0].Cells[2].Value = row["Model"].ToString().Trim();
                //dataGridView1.Rows[0].Cells[4].Value = kontrola pierwszej
                dataGridView1.Rows[0].Cells[5].Value = lotToRankABQty[lotNo][2];
                dataGridView1.Rows[0].Cells[6].Value = ledLeft.RankA[0].Nc12;
                dataGridView1.Rows[0].Cells[7].Value = lotToRankABQty[lotNo][0];
                dataGridView1.Rows[0].Cells[8].Value = lotToRankABQty[lotNo][1];
                dataGridView1.Rows[0].Cells[9].Value = row["IloscWykonana"].ToString().Trim();
                dataGridView1.Rows[0].Cells[10].Value = row["NGIlosc"].ToString().Trim();
                dataGridView1.Rows[0].Cells[11].Value = row["ScrapIlosc"].ToString().Trim();
                dataGridView1.Rows[0].Cells[12].Tag = ledLeft;
                dataGridView1.Rows[0].Cells[12].Value = "OK";

                dataGridView1.Rows[0].Cells[13].Value = DateTime.Parse(row["DataCzasStart"].ToString().Trim()).ToString("HH:mm:ss dd-MM-yyyy");
                dataGridView1.Rows[0].Cells[14].Value = DateTime.Parse(row["DataCzasKoniec"].ToString().Trim()).ToString("HH:mm:ss dd-MM-yyyy");
                dataGridView1.Rows[0].Cells[15].Value = row["OperatorSMT"].ToString().Trim();
            }
            Tools.AutoSizeColumnsWidth(dataGridView1);
            dataGridView1.ResumeLayout();
            suspendCellVelueChangedEvent = false;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right & dataGridView1.Columns[e.ColumnIndex].Name == "goodQty")
            {
                dataGridView1.Columns["Scrap"].Visible = !dataGridView1.Columns["Scrap"].Visible;
            }
        }
        
        private void EfficiencyTick()
        { 
            int lotsThisShift = EfficiencyTools.HowManyLotsThisShift(dataGridView1);
            if (lotsThisShift > 0)
            {
                Tools.dateShiftNo shiftStart = Tools.whatDayShiftIsit(DateTime.Now);
                double minutesFromShiftStart = (DateTime.Now - shiftStart.date).TotalMinutes;
                double lotsPerShift = (480 * (double)lotsThisShift) / minutesFromShiftStart;
                double efficiency = Math.Round(lotsPerShift / normlotsPerShift * 100, 1);


                labelWasteLed.Text = "Odpad diody LED: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridView1, normPerModel)[0] + "%";
                labelModuleWaste.Text= "Odpad modułów: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridView1, normPerModel)[1] + "%";
                labelLotsThisShift.Text = "LOTy od początku zmiany: " + lotsThisShift;
                labelEfficiency.Text = "Wydajność: " + efficiency + "%";

                if (DateTime.Now.Minute == 0 || DateTime.Now.Minute == 30)
                {
                    Charting.DrawEfficiencyChart(pbChart, (float)efficiency);
                }

                //List<Charting.EfficiencyAtTime> list = (List<Charting.EfficiencyAtTime>)pbChart.Tag;

                //if (list==null)
                //{
                //    Charting.DrawEfficiencyChart(pbChart, (float)efficiency);
                //}
                //else
                //if (list.Count > 0)
                //{
                //    DateTime lastChartPoint = list[list.Count - 1].time;
                //    if ((DateTime.Now - lastChartPoint).TotalMinutes > 0)
                //    {
                //        Charting.DrawEfficiencyChart(pbChart, (float)efficiency);
                //    }
                //}


                
                

            }
            else
            {
                labelWasteLed.Text = "Odpad diody LED: -";
                labelModuleWaste.Text = "Odpad modułów: ";
                labelLotsThisShift.Text = "LOTy od początku zmiany: -";
                labelEfficiency.Text = "Wydajność: -";
            }
            EfficiencyTools.QuantityDictionaryToGrid(dataGridView3DaysInfo, EfficiencyTools.quantityPerDayPerShift(SqlOperations.GetSmtRecordsFromDbQuantityOnly(6, smtLine)));
            Charting.DrawDayByDayEfficiency(dataGridView3DaysInfo, pictureBoxShifts);
            
        }

        private void EfficiencyTimer_Tick(object sender, EventArgs e)
        {
            EfficiencyTick();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           if( dataGridView1.SelectedCells[0].ColumnIndex==0)

            {
                dataGridView1.Rows[0].Cells[1].Selected = true;
            }
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView3DaysInfo.Rows[e.RowIndex].Cells[e.ColumnIndex];

            ShowShiftInfo ShiftInfoDialog = new ShowShiftInfo((DataTable)cell.Tag, normlotsPerShift);
            ShiftInfoDialog.ShowDialog();
        }

        private void dataGridView3DaysInfo_SelectionChanged(object sender, EventArgs e)
        {
            bool release = true;
#if DEBUG
            release = false;
            #endif

            if (release)
                dataGridView3DaysInfo.ClearSelection();
        }
    }
}
