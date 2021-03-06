﻿using Karta_Pracy_SMT.Data_Structures;
using Karta_Pracy_SMT.Efficiency;
using Karta_Pracy_SMT.Forms;
using LiveCharts.Wpf;
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
        CurrentMstOrder currentMstOrder = new CurrentMstOrder("", "", 0, 0, DateTime.Now, "", "", DateTime.Now, 0, 0, 0, 0, new List<ledReelData>(), "", 0, 0);

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
            #if DEBUG
            button2.Visible = true;
            EfficiencyTimer.Interval = 1000;
            dataGridView3DaysInfo.ReadOnly = false;
            dataGridView3DaysInfo.EditMode = DataGridViewEditMode.EditOnKeystroke;
            smtLine = "SMT2";
            #endif
            angularGauge1.LabelsStep = 10;
            angularGauge1.TickStep = 5;
            angularGauge1.Sections.Add(new AngularSection
            {
                FromValue = 0,
                ToValue = 70,
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 20, 60))
            });
            angularGauge1.Sections.Add(new AngularSection
            {
                FromValue = 70,
                ToValue = 90,
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 215, 0))
            });
            angularGauge1.Sections.Add(new AngularSection
            {
                FromValue = 90,
                ToValue = 100,
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(127, 255, 0))
            });

            EfficiencyCalculation.smtLine = smtLine;

            string chkMirae = ConfigurationManager.AppSettings["CheckMirae"];
            //checkMirae = Convert.ToBoolean(chkMirae);
            LoadLgRecordsFromDb();
            LoadMstOrdersFromDb(20);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "Karta pracy SMT " + version;
            //panelRight.Width = (int)Math.Round(this.Width * 0.6, 0);
            //pbChart.Width = this.Width-(panelRight.Location.X + 300);
            //dataGridView3DaysInfo.Width = 250;// panelLeft.Width - button1.Width - pictureBoxShifts.Width - 10;
            //dataGridView3DaysInfo.ScrollBars = ScrollBars.None;

            normPerModel = EfficiencyTools.CreateEfficiencyNorm();
            EfficiencyTick();
            if (smtLine == "SMT7" || smtLine == "SMT8" || smtLine == "SMT1")
            {
                normLotsPerShift = 12;
            }
           
            Charting.DrawEfficiencyChart(pbChart, 0);
            foreach (DataGridViewColumn column in dataGridViewLg.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(300, 30);

            tableLayoutPanel1.ColumnStyles[1].Width = panel5.Width + buttonMstSaveOrder.Width + panel5.Padding.Left*2+ buttonMstSaveOrder.Padding.Right;
            
            panelClock.Parent = this;
            panelClock.BringToFront();
            panelClock.Width = pbChart.Width;
            panelClock.Location = new Point(this.Width - panelClock.Width-23, 0);

            //efficiency
            
            //EfficiencyCalculation.dtDb = MST.MES.Data_structures.DevTools.DevToolsLoader.LoadDevToolsModels();
            //EfficiencyCalculation.mesModels = MST.MES.SqlDataReaderMethods.MesModels.allModels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int notSavedQty = 0;
            foreach (DataGridViewRow row in dataGridViewLg.Rows)
            {
                if (row.Cells["ColumnSaved"].Style.BackColor == Color.Red)
                {
                    notSavedQty++;
                }
            }

            if (notSavedQty <= 1)
            {
                NewLotForm fmNewLot = new NewLotForm(this, dataGridViewLg, miraeCurrentProgram, checkMirae);
                fmNewLot.ShowInTaskbar = false;
                fmNewLot.ShowDialog();
                UpdateCurrentModelEffNorm();
            }
            else
            {
                MessageBox.Show("Zakończ najpierw aktualny LOT.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 & e.RowIndex >= 0) 
            {
                DataGridViewCell cell = dataGridViewLg.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewLg.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
                {
                    if (dataGridViewLg.Columns[e.ColumnIndex].Name == "ColumnButtonLed")
                    {
                        LedLeftovers clickedLeftovers = (LedLeftovers)cell.Tag;
                        Add_LED_leftovers editLeftovers = new Add_LED_leftovers(dataGridViewLg, cell, ledLeftSaveBuffer);
                        editLeftovers.ShowInTaskbar = false;
                        editLeftovers.ShowDialog();
                    }
                    
                    if (dataGridViewLg.Columns[e.ColumnIndex].Name == "ColumnQualityCheck")
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
                int ngIndex = dataGridViewLg.Columns.IndexOf(dataGridViewLg.Columns["Ng"]);
                int scrapIndex = dataGridViewLg.Columns.IndexOf(dataGridViewLg.Columns["Scrap"]);
               // Debug.WriteLine("Cell value changed: " + e.RowIndex + " " + e.ColumnIndex);
                //calculate good qty
                if (dataGridViewLg.Rows.Count > 0 & (e.ColumnIndex == ngIndex || e.ColumnIndex == scrapIndex))
                {
                    int rowIndex = e.RowIndex;

                    DataGridViewCell ngCell = dataGridViewLg.Rows[rowIndex].Cells["Ng"];
                    DataGridViewCell scrapCell = dataGridViewLg.Rows[rowIndex].Cells["Scrap"];
                    
                    double ngValue = -9999;
                    double scrapValue = -9999;

                    if (ngCell.Value != null)
                        if (!(double.TryParse(ngCell.Value.ToString(), out ngValue)))
                        {
                            dataGridViewLg.Rows[e.RowIndex].Cells[ngIndex].Value = null;
                            ngValue = -9999;
                        }
                        else
                        {
                            //ng ,0 will be 0
                            dataGridViewLg.Rows[e.RowIndex].Cells[ngIndex].Value = ngValue;
                        }

                    if (dataGridViewLg.Columns["Scrap"].Visible)
                    {
                        if (scrapCell.Value != null)
                            if (!(double.TryParse(scrapCell.Value.ToString(), out scrapValue)))
                            {
                                dataGridViewLg.Rows[e.RowIndex].Cells[scrapIndex].Value = null;
                                scrapValue = -9999;
                            }
                    }
                    else
                    {
                        scrapValue = 0;
                    }

                    if (ngValue > -9999 & scrapValue >-9999 & dataGridViewLg.Rows[e.RowIndex].Cells["ColumnQty"].Value!=null)
                    {
                        double orderedQty = double.Parse(dataGridViewLg.Rows[e.RowIndex].Cells["ColumnQty"].Value.ToString());
                        double goodQty = orderedQty - ngValue - scrapValue;
                        dataGridViewLg.Rows[e.RowIndex].Cells["goodQty"].Value = goodQty;
                    }
                    else
                    {
                        dataGridViewLg.Rows[e.RowIndex].Cells["goodQty"].Value = "";
                    }
                }

                //saving finished LOT
                foreach (DataGridViewRow row in dataGridViewLg.Rows)
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

                            DateTime startDate = (DateTime)row.Cells["StartDate"].Value;
                            DateTime endDate = (DateTime)row.Cells["EndDate"].Value;
                            string firstPieceCheck = "";

                            string stencil = Tools.getCellValue(row.Cells["Stencil"]);

                            LedLeftovers ledLeftovers = (LedLeftovers)row.Cells["ColumnButtonLed"].Tag;
                            double totalLedsUsed = 0;
                            string leftovers = ledLeftovers.RankA[0].ID + ":" + ledLeftovers.RankA[0].Nc12 + ":" + ledLeftovers.RankA[0].QtyLeft + "|" + ledLeftovers.RankB[0].ID + ":" + ledLeftovers.RankB[0].Nc12 + ":" + ledLeftovers.RankB[0].QtyLeft;
                            for (int i = 0; i < ledLeftovers.RankA.Count; i++)
                            {
                                totalLedsUsed += ledLeftovers.RankA[i].StartQty - ledLeftovers.RankA[i].QtyLeft;
                                totalLedsUsed += ledLeftovers.RankB[i].StartQty - ledLeftovers.RankB[i].QtyLeft;
                                if (i > 0)
                                {
                                    leftovers += "#" + ledLeftovers.RankA[i].ID + ":" + ledLeftovers.RankA[i].Nc12 + ":" + ledLeftovers.RankA[i].QtyLeft + "|" + ledLeftovers.RankB[i].ID + ":" + ledLeftovers.RankB[i].Nc12 + ":" + ledLeftovers.RankB[i].QtyLeft;
                                    
                                }
                            }

                            if (row.Cells["ColumnQualityCheck"].Tag != null)
                            {
                                firstPieceCheck = row.Cells["ColumnQualityCheck"].Tag.ToString();
                            }

                            string scrap = "0";
                            if (dataGridViewLg.Columns["Scrap"].Visible) scrap = row.Cells["Scrap"].Value.ToString();

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
                                stencil,
                                "LG",
                                totalLedsUsed);
                            // Debug.WriteLine("Saved");
                            DgvTools.CleanUpLgiDgv(dataGridViewLg);
                        }
                    }
                }
                dataGridViewLg.HorizontalScrollingOffset = 0;
            }
        }
        
        private void lotFinished(DataGridViewCheckBoxCell checkBoxCell, int rowIndex)
        {
            DateTime now = System.DateTime.Now;
            dataGridViewLg.Rows[rowIndex].Cells["EndDate"].Value = now;
            Color cellColor = Tools.GetShiftColor(now);
            

            dataGridViewLg.Rows[rowIndex].Cells["ColumnSaved"].Style.BackColor = Color.White;
            dataGridViewLg.Rows[rowIndex].Cells[13].Style.BackColor = cellColor;
            dataGridViewLg.Rows[rowIndex].Cells[14].Style.BackColor = cellColor;
            dataGridViewLg.Rows[rowIndex].Cells[15].Style.BackColor = cellColor;

            checkBoxCell.Value = true;

            AdjsutRunningLotTime();
        }

        

        private void AdjsutRunningLotTime()
        {
            foreach (DataGridViewRow row in dataGridViewLg.Rows)
            {
                if (!Convert.ToBoolean(row.Cells["ColumnSaved"].Value))
                {
                    row.Cells["StartDate"].Value = DateTime.Now;
                    break;
                }
            }
        }

        string currentStencilQR = "";
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!suspendCellVelueChangedEvent)
            {
                if(dataGridViewLg.Rows[e.RowIndex].Cells["ColumnQualityCheck"].Value != null)
                {
                    currentStencilQR = dataGridViewLg.Rows[e.RowIndex].Cells["Stencil"].Value.ToString();
                }
                else
                {
                    dataGridViewLg.Rows[e.RowIndex].Cells["Stencil"].Value = currentStencilQR;
                }
            }
            // Debug.WriteLine(dataGridView1.Rows.Count);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridViewLg.Rows.Insert(0, 1);
            dataGridViewLg.Rows[0].Cells["StartDate"].Value = System.DateTime.Now.ToLongTimeString();

            dataGridViewLg.Rows[0].Cells[1].Value = "fakeLot";
            string connQty = "4";
            dataGridViewLg.Rows[0].Cells[3].Value = connQty;
            if (connQty == "4")
            {
                dataGridViewLg.Rows[0].Cells[3].Style.ForeColor = Color.White;
                dataGridViewLg.Rows[0].Cells[3].Style.BackColor = Color.DarkCyan;
            }
            dataGridViewLg.Rows[0].Cells[2].Value = "Model";
            //dataGridView1.Rows[0].Cells[4].Value = kontrola pierwszej
            dataGridViewLg.Rows[0].Cells[5].Value = "156";
            dataGridViewLg.Rows[0].Cells[6].Value = "RankA";
            dataGridViewLg.Rows[0].Cells[7].Value = "RankB";
            dataGridViewLg.Rows[0].Cells[8].Value = "LED";
            //dataGridView1.Rows[0].Cells[9].Value = ;
            //dataGridView1.Rows[0].Cells[10].Value = row["NGIlosc"].ToString().Trim();
            //dataGridView1.Rows[0].Cells[11].Value = row["ScrapIlosc"].ToString().Trim();
            dataGridViewLg.Rows[0].Cells[12].Tag = dataGridViewLg.Rows[1].Cells[12].Tag;
            dataGridViewLg.Rows[0].Cells[12].Value = "OK";

            dataGridViewLg.Rows[0].Cells[13].Value = DateTime.Now.AddMinutes(-45).ToString("HH:mm:ss dd-MM-yyyy");
            dataGridViewLg.Rows[0].Cells[14].Value =DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");


            dataGridViewLg.Rows[0].Cells[15].Value = "Operator";


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

                rankAList.Add(new RankStruc(lotToRankABQty[lotNo][0], rankA[0], rankA[1], qtyA, 3000));
                rankBList.Add(new RankStruc(lotToRankABQty[lotNo][1], rankB[0], rankB[1], qtyB, 3000));
            }

            return new LedLeftovers(rankAList, rankBList, lotNo);
        }

        private void LoadLgRecordsFromDb()
        {
            suspendCellVelueChangedEvent = true;
            //DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED
            DataTable sqlTable = SqlOperations.GetLgSmtRecordsFromDb(22, smtLine);
            List<string> lotsList = new List<string>();
            foreach (DataRow row in sqlTable.Rows)
            {
                lotsList.Add(row["NrZlecenia"].ToString().Trim());
            }

            Dictionary<string, string[]> lotToRankABQty = SqlOperations.lotToRankABQty(lotsList.ToArray());

            //dataGridView1.SuspendLayout();
            foreach (DataRow row in sqlTable.Rows)
            {
                string model = row["Model"].ToString();
                if (model[2] == '-')
                {
                    if (lotToRankABQty.ContainsKey(row["NrZlecenia"].ToString()))
                    {
                        dataGridViewLg.Rows.Insert(0);

                        string lotNo = row["NrZlecenia"].ToString().Trim();
                        string stencil = row["StencilQR"].ToString();

                        LedLeftovers ledLeft = CreateLedLeftovers(row["KoncowkiLED"].ToString(), lotNo, lotToRankABQty);

                        DataGridViewCheckBoxCell chbCell = (DataGridViewCheckBoxCell)dataGridViewLg.Rows[0].Cells[0];
                        chbCell.Value = true;

                        dataGridViewLg.Rows[0].Cells[1].Value = lotNo;
                        string connQty = Tools.GetNumberOfConnectors(row["Model"].ToString().Trim());
                        dataGridViewLg.Rows[0].Cells[3].Value = connQty;
                        if (connQty == "4")
                        {
                            dataGridViewLg.Rows[0].Cells[3].Style.ForeColor = Color.White;
                            dataGridViewLg.Rows[0].Cells[3].Style.BackColor = Color.DarkCyan;
                        }
                        dataGridViewLg.Rows[0].Cells[2].Value = row["Model"].ToString().Trim();
                        //dataGridView1.Rows[0].Cells[4].Value = kontrola pierwszej
                        dataGridViewLg.Rows[0].Cells[5].Value = lotToRankABQty[lotNo][2];
                        dataGridViewLg.Rows[0].Cells[6].Value = ledLeft.RankA[0].Nc12;
                        dataGridViewLg.Rows[0].Cells[7].Value = lotToRankABQty[lotNo][0];
                        dataGridViewLg.Rows[0].Cells[8].Value = lotToRankABQty[lotNo][1];
                        dataGridViewLg.Rows[0].Cells[9].Value = row["IloscWykonana"].ToString().Trim();
                        dataGridViewLg.Rows[0].Cells[10].Value = row["NGIlosc"].ToString().Trim();
                        dataGridViewLg.Rows[0].Cells[11].Value = row["ScrapIlosc"].ToString().Trim();
                        dataGridViewLg.Rows[0].Cells[12].Tag = ledLeft;
                        dataGridViewLg.Rows[0].Cells[12].Value = "OK";

                        dataGridViewLg.Rows[0].Cells[13].Value = DateTime.Parse(row["DataCzasStart"].ToString().Trim());
                        dataGridViewLg.Rows[0].Cells[14].Value = DateTime.Parse(row["DataCzasKoniec"].ToString().Trim());
                        Color cellColor = Tools.GetShiftColor((DateTime)row["DataCzasKoniec"]);
                        dataGridViewLg.Rows[0].Cells[13].Style.BackColor = cellColor;
                        dataGridViewLg.Rows[0].Cells[14].Style.BackColor = cellColor;
                        dataGridViewLg.Rows[0].Cells[15].Style.BackColor = cellColor;

                        dataGridViewLg.Rows[0].Cells[15].Value = row["OperatorSMT"].ToString().Trim();
                        dataGridViewLg.Rows[0].Cells[16].Value = stencil;
                    }
                }
            }
            Tools.AutoSizeColumnsWidth(dataGridViewLg);
            //dataGridView1.ResumeLayout();
            if (dataGridViewLg.Rows.Count>0)
            dataGridViewLg.CurrentCell =  dataGridViewLg.Rows[0].Cells[0];


            suspendCellVelueChangedEvent = false;
        }

        private void LoadMstOrdersFromDb(int recordsQuantity)
        {
            var nc = MST.MES.SqlOperations.ConnectDB.Nc12ToModelFullDict();
            DataTable sqlTable = SqlOperations.GetMstSmtRecordsFromDb(recordsQuantity, smtLine);
            var smto = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder("");
            dataGridViewMstOrders.Columns["MstOrdersStart"].DefaultCellStyle.Format = "HH:mm dd-MMM";
            dataGridViewMstOrders.Columns["MstOrdersEnd"].DefaultCellStyle.Format = "HH:mm dd-MMM";

            List<EfficiencyCalculation.OrderDataForEfficiencyStructure> ordersEff = new List<EfficiencyCalculation.OrderDataForEfficiencyStructure>();

            if (sqlTable.Rows.Count > 0)
            {
                HashSet<string> nc12ToModelList = new HashSet<string>();
                foreach (DataRow row in sqlTable.Rows)
                {
                    DateTime startDate = DateTime.Parse(row["DataCzasStart"].ToString().Trim());
                    DateTime endDate = DateTime.Parse(row["DataCzasKoniec"].ToString().Trim());

                    string nc10 = row["Model"].ToString() ;
                    double qty = 0;
                    string orderNo = row["NrZlecenia"].ToString();
                    double efficiency = 0;

                    if (double.TryParse(row["IloscWykonana"].ToString(), out qty)) 
                    {
                        efficiency = Math.Round(EfficiencyCalculation.CalculateEfficiency(startDate, endDate, nc10, qty, true) * 100, 0);
                    }
                    string name = nc.ContainsKey(nc10 + "00") ? nc[nc10 + "00"] : nc10;
                    dataGridViewMstOrders.Rows.Insert(0, startDate, endDate, orderNo, nc10.Insert(4, " ").Insert(8, " "), qty, efficiency+"%", name);
                    nc12ToModelList.Add(nc10 + "00");

                    ordersEff.Add(new EfficiencyCalculation.OrderDataForEfficiencyStructure() { start = startDate, end = endDate, qty = qty, modelId=nc10 });

                    var owningShift = DateTools.GetOrderOwningShift(startDate, endDate);
                    var currentShift = DateTools.whatDayShiftIsit(DateTime.Now);
                    //1878856
                    if (orderNo == "1878856")
                        ;
                    if (owningShift.fixedDate != currentShift.fixedDate)
                    {
                        if (owningShift.fixedDate.Date == currentShift.fixedDate.Date)
                        {
                            DgvTools.ColorDgvRow(dataGridViewMstOrders.Rows[0], Color.LightGray);
                        }
                        else
                        {
                            DgvTools.ColorDgvRow(dataGridViewMstOrders.Rows[0], Color.Silver);
                        }
                    }
                }
                //Dictionary<string, string> nc12toName = SqlOperations.nc12ToModelDict(nc12ToModelList.ToArray());

                //foreach (DataGridViewRow row in dataGridViewMstOrders.Rows)
                //{
                //    string nc12 = row.Cells["Column12NC"].Value.ToString().Replace(" ","") + "00";
                //    row.Cells["ColumnName"].Value = nc12toName[nc12];
                //}

                DgvTools.AutoSizeColumns(dataGridViewMstOrders, DataGridViewAutoSizeColumnMode.AllCells);

                EfficiencyRecordsForOrdersHistory.FillOutListView(lVMstEfficiencyRecord, ordersEff);
            }

            var eff = Math.Round(MstCurrentShiftEfficiency.CalculateCurrentShiftEff(currentMstOrder, dataGridViewMstOrders),0);
            angularGauge1.Value = Math.Min(100,eff);
            labelCurrentShiftEfficiency.Text = $"Wydajność aktualnej zmiany: {eff}%";
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right & dataGridViewLg.Columns[e.ColumnIndex].Name == "goodQty")
            {
                dataGridViewLg.Columns["Scrap"].Visible = !dataGridViewLg.Columns["Scrap"].Visible;
            }
            if (e.Button == MouseButtons.Right & dataGridViewLg.Columns[e.ColumnIndex].Name == "Operator")
            {
                dataGridViewLg.Columns["Stencil"].Visible = !dataGridViewLg.Columns["Stencil"].Visible;
            }
        }

        private void EfficiencyTick()
        {
            if (dataGridViewLg.Rows.Count > 0)
            {
                var eff = Math.Round(LgCurrentShiftEfficiency.CalculateCurrentShiftEff(dataGridViewLg), 0);
                int lotsThisShift = EfficiencyTools.HowManyLotsThisShift(dataGridViewLg);

                if (eff > 0)
                {
                    labelWasteLed.Text = "Odpad diody LED: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridViewLg, normPerModel)[0] + "%";
                    labelModuleWaste.Text = "Odpad wyrobów: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridViewLg, normPerModel)[1] + "%";
                    labelLotsThisShift.Text = "LOTy od początku zmiany: " + lotsThisShift;
                    labelEfficiency.Text = "Wydajność: " + eff + "%";

                    bool debugOff = true;
#if DEBUG
                    debugOff = false;
#endif

                    if (debugOff)
                    {
                        if (DateTime.Now.Minute == 0 || DateTime.Now.Minute == 30)
                        {
                            Charting.DrawEfficiencyChart(pbChart, (float)eff);
                        }
                    }
                    else
                    {
                        Charting.DrawEfficiencyChart(pbChart, (float)eff);
                    }

                }
                else
                {
                    labelWasteLed.Text = "Odpad diody LED: -";
                    labelModuleWaste.Text = "Odpad modułów: ";
                    labelLotsThisShift.Text = "LOTy od początku zmiany: -";
                    labelEfficiency.Text = "Wydajność: -";
                }

                if (dataGridViewLg.Rows.Count > 2)
                {
                    EfficiencyTools.QuantityDictionaryToGrid(dataGridView3DaysInfo, EfficiencyTools.quantityPerDayPerShift(SqlOperations.GetSmtRecordsFromDbQuantityOnly(2, smtLine)));
                    Charting.DrawDayByDayEfficiency(dataGridView3DaysInfo, pictureBoxShifts);
                }
                UpdateCurrentModelEffNorm();//remove
            }
        }

        private void UpdateCurrentModelEffNorm()
        {
            if (dataGridViewLg.Rows.Count > 0)
            {
                LedLeftovers ledLeftovers = (LedLeftovers)dataGridViewLg.Rows[0].Cells["ColumnButtonLed"].Tag;
                int headsUsed = 2;
                if (ledLeftovers.RankA.Count() > 0)
                {
                    headsUsed = ledLeftovers.RankA.Count * 2;
                }

                string model = dataGridViewLg.Rows[0].Cells["ColumnModel"].Value.ToString();
                double orderedQty = double.Parse(dataGridViewLg.Rows[0].Cells["ColumnQty"].Value.ToString());
                var norm = EfficiencyCalculation.CalculateModelNormPerHour(model, smtLine, headsUsed);

                labelLgModelName.Text = $"Czas cyklu: {norm.lineCT}sek";
                labelLgNormPerH.Text = $"Norma szt/h: {norm.outputPerHour}";
                labelLgLotNorm.Text = $"Norma na LOT: {Math.Round(orderedQty * 60 / norm.outputPerHour,0)}min";
                labelLgHeadCount.Text = $"Używane głowice: {headsUsed}";
                labelLgNormModelName.Text =$"Norma modelu: {norm.modelSpec.model12Nc}";
            }
        }

        private void EfficiencyTick2()
        {
            if (dataGridViewLg.Rows.Count > 0)
            {

                int lotsThisShift = EfficiencyTools.HowManyLotsThisShift(dataGridViewLg);
                if (lotsThisShift > 0)
                {
                    Tools.dateShiftNo shiftStart = Tools.whatDayShiftIsit(DateTime.Now);
                    double minutesFromShiftStart = (DateTime.Now - shiftStart.date).TotalMinutes;
                    double lotsPerShift = (480 * (double)lotsThisShift) / minutesFromShiftStart;
                    double efficiency = Math.Round(lotsPerShift / normLotsPerShift * 100, 1);

                    labelWasteLed.Text = "Odpad diody LED: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridViewLg, normPerModel)[0] + "%";
                    labelModuleWaste.Text = "Odpad modułów: " + EfficiencyTools.CalculateLedDiodeWasteLevel(dataGridViewLg, normPerModel)[1] + "%";
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

                }
                else
                {
                    labelWasteLed.Text = "Odpad diody LED: -";
                    labelModuleWaste.Text = "Odpad modułów: ";
                    labelLotsThisShift.Text = "LOTy od początku zmiany: -";
                    labelEfficiency.Text = "Wydajność: -";
                }

                if (dataGridViewLg.Rows.Count > 2)
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
           if( dataGridViewLg.SelectedCells[0].ColumnIndex==0)

            {
                dataGridViewLg.Rows[0].Cells[1].Selected = true;
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
                DataGridViewCell cell = dataGridViewLg.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //Debug.WriteLine(cell.Value.ToString());
                
                if (cell.OwningColumn.Name == "Ng" & cell.Value!=null )
                {
                    
                    int check = 0;
                    if (!int.TryParse(e.FormattedValue.ToString(), out check))
                    {
                        e.Cancel = true;
                        dataGridViewLg.EndEdit();
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
                foreach (DataGridViewRow row in dataGridViewLg.Rows)
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
            if (currentMstOrder.PreviouslyManufacturedQty>0)
            {
                labelMstOrderQtyDone.Text += $" / {currentMstOrder.PreviouslyManufacturedQty + currentMstOrder.MadeQty}";
            }
            labelPreviousProduction.Text = currentMstOrder.PreviouslyManufacturedQty.ToString();
            label12NC.Text = currentMstOrder.Nc10.Insert(4," ").Insert(8," ");
            labelMstOrderOrderedQty.Text = currentMstOrder.OrderedQty.ToString();
            labelMstOrderNo.Text = currentMstOrder.OrderNumber;
            labelMstOrderNo.Tag = currentMstOrder.OrderNumber;
            labelName.Text = currentMstOrder.ModelName;
            labelPcbPerMb.Text = currentMstOrder.PcbOnMb.ToString();
            labelLedQty.Text = currentMstOrder.LedQty.ToString();
            labelConnQty.Text = currentMstOrder.ConnQty.ToString();
            labelResQty.Text = currentMstOrder.ResQty.ToString();
            labelModelNorm.Text = $"{currentMstOrder.NormPerHour} szt/h";
            labelMstCycleTime.Text = $"{Math.Ceiling(3600/currentMstOrder.NormPerHour * currentMstOrder.PcbOnMb)}sek /1MB";

            labelBinInfo.Text = string.Join(Environment.NewLine, MstOperations.LedInfoTableToList(MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(currentMstOrder.OrderNumber)).ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MstOrder mstOrderForm = new MstOrder(smtLine))
            {
                if (mstOrderForm.ShowDialog() == DialogResult.OK)
                {
                    currentMstOrder = mstOrderForm.currentMstOrderData;

                    DataTable ledInfoTable =MST.MES.SqlOperations.SparingLedInfo.GetReelsForLot(currentMstOrder.OrderNumber);

                    UpdateMstLabels();
                    DgvTools.PrepareDgvForBins(dataGridViewMstLedReels, currentMstOrder.BinQty);
                    DgvTools.PrepareDgvForBins(dataGridViewLedTrash, currentMstOrder.BinQty);
                    timerMstUpdate.Enabled = true;
                }
            }
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

        private void dataGridViewMstLedReels_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewMstLedReels.ClearSelection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (ReadQrForm qrForm = new ReadQrForm())
            {
                bool alreadyExist = false;
                if (qrForm.ShowDialog() ==  DialogResult.OK)
                {
                    foreach (var reel in currentMstOrder.LedReels)
                    {
                        if (reel.NC12 == qrForm.nc12 & reel.ID==qrForm.id)
                        {
                            MessageBox.Show("Ta rolka została już dodana!");
                            alreadyExist = true;
                            break;
                        }
                    }
                    if (!alreadyExist)
                    {
                        DgvTools.AddReelToGrid(qrForm.nc12, qrForm.id, dataGridViewMstLedReels, ref currentMstOrder);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (ReadQrForm qrForm = new ReadQrForm())
            {
                if (qrForm.ShowDialog() == DialogResult.OK)
                {
                    DgvTools.AddReelToTrash(qrForm.nc12, qrForm.id, dataGridViewLedTrash,ref currentMstOrder);
                    DgvTools.MarkRemovedRow(qrForm.nc12, qrForm.id, dataGridViewMstLedReels, Color.FromArgb(255, 44, 62, 80), Color.FromArgb(255, 189, 195, 199));
                }
            }
        }

        private void dataGridViewLedTrash_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewLedTrash.ClearSelection();
        }

        private void dataGridViewMstOrders_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewMstOrders.ClearSelection();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            labelClockTime.Text = DateTime.Now.ToString("HH:mm:ss");
            labelClockDate.Text = DateTime.Now.ToString("dddd dd MMMM yyyy        ")+ " Linia "+smtLine ;
        }

        bool updateFormDisplayed = false;
        private void timerMstUpdate_Tick(object sender, EventArgs e)
        {
            if (!updateFormDisplayed)
            {
                if (currentMstOrder.OrderNumber != "")
                {
                    if (Math.Abs((DateTime.Now-currentMstOrder.LastUpdateTime ).TotalMinutes) > 30)
                    {
                        UpdateMstOrder();
                        
                    }
                }
            }
        }

        private void UpdateMstOrder()
        {
            using (UpdateMstQty updForm = new UpdateMstQty(currentMstOrder.LastUpdateTime, currentMstOrder.MadeQty, currentMstOrder.PcbOnMb))
            {
                updateFormDisplayed = true;
                if (updForm.ShowDialog() == DialogResult.OK)
                {
                    currentMstOrder.LastUpdateTime = (DateTime.Now - currentMstOrder.DateStart).TotalMinutes >= 1 ? DateTime.Now : DateTime.Now.AddMinutes(1);
                    currentMstOrder.MadeQty = updForm.newTotalQty;
                    UpdateMstLabels();
                    updateFormDisplayed = false;
                    if (currentMstOrder.RecordId > 0) 
                    {
                        //update
                        SqlOperations.UpdateCurrentMstOrderQuantity( currentMstOrder.MadeQty, currentMstOrder.RecordId);
                    }
                    else
                    {
                        //insert
                        SqlOperations.SaveRecordToDb(currentMstOrder.DateStart, DateTime.Now, smtLine, currentMstOrder.Oper, currentMstOrder.OrderNumber, currentMstOrder.Nc10, currentMstOrder.MadeQty.ToString(), "0", "0", "check", "", currentMstOrder.Stencil, "MST", 0);
                        int lastRecordId = 0;
                        lastRecordId = SqlOperations.GetLastRecordIdForLine(smtLine);
                        if (lastRecordId > 0)
                        {
                            currentMstOrder.RecordId = lastRecordId;
                        }
                    }
                    var eff = Math.Round(MstCurrentShiftEfficiency.CalculateCurrentShiftEff(currentMstOrder, dataGridViewMstOrders), 0);
                    angularGauge1.Value = Math.Min(100, eff);
                    labelCurrentShiftEfficiency.Text = $"Wydajność aktualnej zmiany: {eff}%";
                }
            }
        }

        private void labelMstOrderLastUpdate_DoubleClick(object sender, EventArgs e)
        {
            UpdateMstOrder();
        }

        private void MstSaveOrder_Click(object sender, EventArgs e)
        {
            if (currentMstOrder.OrderNumber.Trim() != "")
            {
                timerMstUpdate.Enabled = false;
                using (FinishMstOrder finishForm = new FinishMstOrder(ref currentMstOrder, smtLine))
                {
                    if (finishForm.ShowDialog() == DialogResult.OK)
                    {
                        if (currentMstOrder.RecordId > 0) 
                        {
                            SqlOperations.UpdateCurrentMstOrderQuantity(currentMstOrder.MadeQty, currentMstOrder.RecordId);
                        }
                        else
                        {
                            SqlOperations.SaveRecordToDb(currentMstOrder.DateStart, DateTime.Now, smtLine, currentMstOrder.Oper, currentMstOrder.OrderNumber, currentMstOrder.Nc10, currentMstOrder.MadeQty.ToString(), "0", "0", "check", "", currentMstOrder.Stencil, "MST", 0);
                        }

                        double efficiency = Math.Round(EfficiencyCalculation.CalculateEfficiency(currentMstOrder.DateStart, DateTime.Now, currentMstOrder.Nc10, currentMstOrder.MadeQty, true) * 100, 0);

                        dataGridViewMstOrders.Rows.Insert(0, currentMstOrder.DateStart, DateTime.Now, currentMstOrder.OrderNumber, currentMstOrder.Nc10.Insert(4, " ").Insert(8, " "), currentMstOrder.MadeQty, efficiency+"%",currentMstOrder.ModelName);
                        currentMstOrder = new CurrentMstOrder("Brak", "Brak", 0, 0, DateTime.Now, "Brak", "0000000000", DateTime.Now, 0, 0, 0, 0, new List<ledReelData>(), "Brak", 0, 0);
                        dataGridViewMstLedReels.Rows.Clear();
                        dataGridViewLedTrash.Rows.Clear();
                        UpdateMstLabels();
                        DgvTools.CleanUpMstDgv(dataGridViewMstOrders);
                        var eff = Math.Round(MstCurrentShiftEfficiency.CalculateCurrentShiftEff(currentMstOrder, dataGridViewMstOrders), 0);
                        angularGauge1.Value = Math.Min(100, eff);
                        labelCurrentShiftEfficiency.Text = $"Wydajność aktualnej zmiany: {eff}%";
                    }
                    else
                    {
                        timerMstUpdate.Enabled = true;
                    }
                }
            }
        }
    }
}
