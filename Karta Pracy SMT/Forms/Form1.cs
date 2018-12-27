using Karta_Pracy_SMT.Data_Structures;
using Karta_Pracy_SMT.Forms;
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
using static Karta_Pracy_SMT.Tools;

namespace Karta_Pracy_SMT
{
    public partial class MainForm : Form
    {
        Dictionary<string, EfficiencyStructure> efficiencNormyPerModel = new Dictionary<string, EfficiencyStructure>();
        List<LedLeftovers> ledLeftSaveBuffer = new List<LedLeftovers>();
        Dictionary<string, EfficiencyNormsPerModel> normPerModel = new Dictionary<string, EfficiencyNormsPerModel>();
        string smtLine = ConfigurationManager.AppSettings["SMTLine"];
        double normLotsPerShift = 16;
        CurrentMstOrder currentMstOrder = new CurrentMstOrder("", "", 0, 0, DateTime.Now, "", "", DateTime.Now,0, new List<string>());

        public MainForm()
        {
            InitializeComponent();
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1) 
            {
                MessageBox.Show("Karta Pracy SMT jest już uruchomiona.");
                this.Close();
            }

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
                normLotsPerShift = 12;
            }
           
            Charting.DrawEfficiencyChart(pbChart, 0);

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(300, 30);

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
                        else
                        {
                            //ng ,0 will be 0
                            dataGridView1.Rows[e.RowIndex].Cells[ngIndex].Value = ngValue;
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

                            string stencil = Tools.getCellValue(row.Cells["Stencil"]);

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
                                leftovers,
                                stencil);
                           // Debug.WriteLine("Saved");
                            Tools.CleanUpDgv(dataGridView1);
                        }
                    }
                }
                dataGridView1.HorizontalScrollingOffset = 0;
            }
        }
        
        private void lotFinished(DataGridViewCheckBoxCell checkBoxCell, int rowIndex)
        {
            DateTime now = System.DateTime.Now;
            dataGridView1.Rows[rowIndex].Cells["EndDate"].Value = now.ToString("HH:mm:ss dd-MM-yyyy");
            Color cellColor = Tools.GetShiftColor(now);

            dataGridView1.Rows[rowIndex].Cells["ColumnSaved"].Style.BackColor = Color.White;
            dataGridView1.Rows[rowIndex].Cells[13].Style.BackColor = cellColor;
            dataGridView1.Rows[rowIndex].Cells[14].Style.BackColor = cellColor;
            dataGridView1.Rows[rowIndex].Cells[15].Style.BackColor = cellColor;

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
            dataGridView1.Rows.Insert(0, 1);
            dataGridView1.Rows[0].Cells["StartDate"].Value = System.DateTime.Now.ToLongTimeString();

            dataGridView1.Rows[0].Cells[1].Value = "fakeLot";
            string connQty = "4";
            dataGridView1.Rows[0].Cells[3].Value = connQty;
            if (connQty == "4")
            {
                dataGridView1.Rows[0].Cells[3].Style.ForeColor = Color.White;
                dataGridView1.Rows[0].Cells[3].Style.BackColor = Color.DarkCyan;
            }
            dataGridView1.Rows[0].Cells[2].Value = "Model";
            //dataGridView1.Rows[0].Cells[4].Value = kontrola pierwszej
            dataGridView1.Rows[0].Cells[5].Value = "156";
            dataGridView1.Rows[0].Cells[6].Value = "RankA";
            dataGridView1.Rows[0].Cells[7].Value = "RankB";
            dataGridView1.Rows[0].Cells[8].Value = "LED";
            //dataGridView1.Rows[0].Cells[9].Value = ;
            //dataGridView1.Rows[0].Cells[10].Value = row["NGIlosc"].ToString().Trim();
            //dataGridView1.Rows[0].Cells[11].Value = row["ScrapIlosc"].ToString().Trim();
            dataGridView1.Rows[0].Cells[12].Tag = dataGridView1.Rows[1].Cells[12].Tag;
            dataGridView1.Rows[0].Cells[12].Value = "OK";

            dataGridView1.Rows[0].Cells[13].Value = DateTime.Now.AddMinutes(-45).ToString("HH:mm:ss dd-MM-yyyy");
            dataGridView1.Rows[0].Cells[14].Value =DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");


            dataGridView1.Rows[0].Cells[15].Value = "Operator";


            //dataGridView1.Rows[0].Cells["ColumnSaved"].Style.BackColor = Color.Red;
            //Tools.CleanUpDgv(dataGridView1);
            // dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            // labelWasteLed.Text = EfficiencyTools.CalculateWasteLevel(dataGridView1, normPerModel) + "%";
            // labelLotsThisShift.Text = EfficiencyTools.HowManyLotsThisShift(dataGridView1).ToString();
            //EfficiencyTools.QuantityDictionaryToGrid(dataGridView2, EfficiencyTools.quantityPerDayPerShift(SqlOperations.GetSmtRecordsFromDbQuantityOnly(5, smtLine)));
            //Charting.DrawEfficiencyChart(pbChart, 80);
            //Charting.DrawDayByDayEfficiency(dataGridView3DaysInfo, pictureBoxShifts);
        }

        bool saveLedLeftWorking = false;
        private void timerLedLeftSave_Tick(object sender, EventArgs e)
        {
            if (ledLeftSaveBuffer.Count > 0 & !saveLedLeftWorking) 
            {
                saveLedLeftWorking = true;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    List<int> indexToDelete = new List<int>();

                    if (SqlOperations.UpdateLedLeftovers(ledLeftSaveBuffer[0])) 
                    {
                        ledLeftSaveBuffer.RemoveAt(0);
                    }

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

            return new LedLeftovers(rankAList, rankBList, lotNo);
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
                string model = row["Model"].ToString();
                if (model[2] == '-')
                {
                    if (lotToRankABQty.ContainsKey(row["NrZlecenia"].ToString()))
                    {
                        dataGridView1.Rows.Insert(0);

                        string lotNo = row["NrZlecenia"].ToString().Trim();
                        string stencil = row["StencilQR"].ToString();

                        LedLeftovers ledLeft = CreateLedLeftovers(row["KoncowkiLED"].ToString(), lotNo, lotToRankABQty);

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
                        Color cellColor = Tools.GetShiftColor(DateTime.Parse(row["DataCzasKoniec"].ToString()));
                        dataGridView1.Rows[0].Cells[13].Style.BackColor = cellColor;
                        dataGridView1.Rows[0].Cells[14].Style.BackColor = cellColor;
                        dataGridView1.Rows[0].Cells[15].Style.BackColor = cellColor;

                        dataGridView1.Rows[0].Cells[15].Value = row["OperatorSMT"].ToString().Trim();
                        dataGridView1.Rows[0].Cells[16].Value = stencil;
                    }
                }
                else
                {
                    //MST order
                    dataGridViewMstOrders.Rows.Insert(0, DateTime.Parse(row["DataCzasStart"].ToString().Trim()).ToString("HH:mm:ss dd-MM-yyyy"),
                        DateTime.Parse(row["DataCzasKoniec"].ToString().Trim()).ToString("HH:mm:ss dd-MM-yyyy"),
                        row["Model"].ToString().Trim(),
                        row["IloscWykonana"].ToString().Trim(),
                        row["OperatorSMT"].ToString().Trim());
                }
            }
            Tools.AutoSizeColumnsWidth(dataGridView1);
            Tools.AutoSizeColumnsWidth(dataGridViewMstOrders);
            dataGridView1.ResumeLayout();
            suspendCellVelueChangedEvent = false;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right & dataGridView1.Columns[e.ColumnIndex].Name == "goodQty")
            {
                dataGridView1.Columns["Scrap"].Visible = !dataGridView1.Columns["Scrap"].Visible;
            }
            if (e.Button == MouseButtons.Right & dataGridView1.Columns[e.ColumnIndex].Name == "Operator")
            {
                dataGridView1.Columns["Stencil"].Visible = !dataGridView1.Columns["Stencil"].Visible;
            }
        }

        private void EfficiencyTick()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int lotsThisShift = EfficiencyTools.HowManyLotsThisShift(dataGridView1);
                if (lotsThisShift > 0)
                {
                    Tools.dateShiftNo shiftStart = Tools.whatDayShiftIsit(DateTime.Now);
                    double minutesFromShiftStart = (DateTime.Now - shiftStart.date).TotalMinutes;
                    double lotsPerShift = (480 * (double)lotsThisShift) / minutesFromShiftStart;
                    double efficiency = Math.Round(lotsPerShift / normLotsPerShift * 100, 1);


                    labelWasteLed.Text = "Odpad diody LED: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridView1, normPerModel)[0] + "%";
                    labelModuleWaste.Text = "Odpad modułów: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridView1, normPerModel)[1] + "%";
                    labelLotsThisShift.Text = "LOTy od początku zmiany: " + lotsThisShift;
                    labelEfficiency.Text = "Wydajność: " + efficiency + "%";

                    bool debugOff = true;
#if DEBUG
                    debugOff = false;
#endif

                    if (debugOff)
                    {
                        if (DateTime.Now.Minute == 0 || DateTime.Now.Minute == 30)
                        {
                            Charting.DrawEfficiencyChart(pbChart, (float)efficiency);
                        }
                    }
                    else
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

                if (dataGridView1.Rows.Count > 2)
                {
                    EfficiencyTools.QuantityDictionaryToGrid(dataGridView3DaysInfo, EfficiencyTools.quantityPerDayPerShift(SqlOperations.GetSmtRecordsFromDbQuantityOnly(5, smtLine)));
                    Charting.DrawDayByDayEfficiency(dataGridView3DaysInfo, pictureBoxShifts);
                }
            }
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

            ShowShiftInfo ShiftInfoDialog = new ShowShiftInfo((DataTable)cell.Tag, normLotsPerShift);
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //Debug.WriteLine(cell.Value.ToString());
                
                if (cell.OwningColumn.Name == "Ng" & cell.Value!=null )
                {
                    
                    int check = 0;
                    if (!int.TryParse(e.FormattedValue.ToString(), out check))
                    {
                        e.Cancel = true;
                        dataGridView1.EndEdit();
                    }
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //// This event is called once for each tab button in your tab control
            //// First paint the background with a color based on the current tab
            //// e.Index is the index of the tab in the TabPages collection.
            //switch (e.Index)
            //{
            //    case 0:
            //        e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
            //        break;
            //    case 1:
            //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), e.Bounds);
            //        break;
            //    default:
            //        break;
            //}

            //// Then draw the current tab button text 
            //Rectangle paddedBounds = e.Bounds;
            //paddedBounds.Inflate(-2, -2);
            //float fontSize = 14;
            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;
            //e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, new Font(FontFamily.GenericSansSerif, fontSize), SystemBrushes.HighlightText, paddedBounds, sf);

        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPageLg")
            {
                int notSavedRowsQty = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ColumnSaved"].Style.BackColor == Color.Red)
                    {
                        notSavedRowsQty++;
                    }
                }
                if (notSavedRowsQty>0)
                {
                    MessageBox.Show("Zakończ trwające zlecenia");
                    e.Cancel = true;
                }
            }
        }

        public void UpdateMstLabels()
        {
            labelMstOrderStartDate.Text = currentMstOrder.DateStart.ToString("HH:mm:ss  dd-MM-yyyy");
            labelMstOrderLastUpdate.Text = currentMstOrder.LastUpdateTime.ToString("HH:mm:ss  dd-MM-yyyy");
            labelMstOrderQtyDone.Text = currentMstOrder.MadeQty.ToString();
            label12NC.Text = currentMstOrder.Nc12;
            labelMstOrderOrderedQty.Text = currentMstOrder.OrderedQty.ToString();
            labelMstOrderNo.Text = currentMstOrder.OrderNumber;
            labelMstOrderNo.Tag = currentMstOrder.OrderNumber;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MstOrder mstOrderForm = new MstOrder())
            {
                if (mstOrderForm.ShowDialog() == DialogResult.OK)
                {
                    currentMstOrder.DateStart = mstOrderForm.currentMstOrderData.DateStart;
                    currentMstOrder.LastUpdateTime = mstOrderForm.currentMstOrderData.LastUpdateTime;
                    currentMstOrder.MadeQty = mstOrderForm.currentMstOrderData.MadeQty;
                    currentMstOrder.Nc12 = mstOrderForm.currentMstOrderData.Nc12.Insert(5," ").Insert(10," ");
                    currentMstOrder.Oper = mstOrderForm.currentMstOrderData.Oper;
                    currentMstOrder.OrderedQty = mstOrderForm.currentMstOrderData.OrderedQty;
                    currentMstOrder.OrderNumber = mstOrderForm.currentMstOrderData.OrderNumber;
                    currentMstOrder.PcbOnMb = mstOrderForm.currentMstOrderData.PcbOnMb;
                    currentMstOrder.Stencil = mstOrderForm.currentMstOrderData.Stencil;
                    UpdateMstLabels();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (UpdateMstQty updateQtyForm = new UpdateMstQty(currentMstOrder.LastUpdateTime, currentMstOrder.MadeQty, currentMstOrder.PcbOnMb))
            {
                if (updateQtyForm.ShowDialog() == DialogResult.OK)
                {
                    currentMstOrder.MadeQty = updateQtyForm.newTotalQty;
                    currentMstOrder.LastUpdateTime = DateTime.Now;
                    UpdateMstLabels();
                }
            }

            SqlOperations.SaveRecordToDb(currentMstOrder.DateStart, DateTime.Now, smtLine, currentMstOrder.Oper, currentMstOrder.OrderNumber, currentMstOrder.Nc12, currentMstOrder.MadeQty.ToString(), "0", "0", "OK", "LEDS LEFT", currentMstOrder.Stencil);

            dataGridViewMstOrders.Rows.Insert(0,
                currentMstOrder.DateStart,
                DateTime.Now,
                currentMstOrder.Nc12,
                currentMstOrder.MadeQty,
                currentMstOrder.Oper);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (UpdateMstQty updateQtyForm = new UpdateMstQty(currentMstOrder.LastUpdateTime, currentMstOrder.MadeQty, currentMstOrder.PcbOnMb))
            {
                if (updateQtyForm.ShowDialog() == DialogResult.OK)
                {
                    currentMstOrder.MadeQty = updateQtyForm.newTotalQty;
                    currentMstOrder.LastUpdateTime = DateTime.Now;
                    UpdateMstLabels();
                }
            }
        }
    }
}
