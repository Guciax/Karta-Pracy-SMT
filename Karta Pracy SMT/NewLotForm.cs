using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public partial class NewLotForm : Form
    {
        MainForm opener;
        ledReelData currentLedReel;
        LotData currentLotData = new LotData("", 0, "", "");
        private readonly DataGridView grid;
        private readonly string currentMiraeProgram;
        private readonly bool checkMirae;
        List<TextBox> txbList = new List<TextBox>();
        Tuple<double, double, double> ledRanksQty = new Tuple<double, double,double>(0,0,0);

        public NewLotForm(MainForm callingForm, DataGridView grid, string currentMiraeProgram, bool checkMirae)
        {
            InitializeComponent();
            opener = callingForm as MainForm;
            this.grid = grid;
            this.currentMiraeProgram = currentMiraeProgram;
            this.checkMirae = checkMirae;
            this.ActiveControl = textBoxLotNo;

            txbList.Add(textBoxLotNo);
            txbList.Add(textBoxRankAQr);
            txbList.Add(textBoxRankBQr);
            textBoxLotNo.BackColor = Color.LemonChiffon;
        }

        private void textBoxLotNo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void CountQtyProductToManufacture()
        {
            double sumRankA = 0;
            double sumRankB = 0;

            foreach (DataGridViewRow row in dataGridViewRankA.Rows)
            {
                double rankA = 0;
                string stringA = row.Cells["RankAIlosc"].Value.ToString().Trim();
                double.TryParse(stringA, out rankA);
                sumRankA += rankA;
            }

            foreach (DataGridViewRow row in dataGridViewRankB.Rows)
            {
                double rankB = 0;
                string stringB = row.Cells["rankBIlosc"].Value.ToString().Trim();
                double.TryParse(stringB, out rankB);
                sumRankB += rankB;
            }
            double smtCarrier = ledRanksQty.Item3;
            if (smtCarrier < 3) smtCarrier = 1;

            double maxProductQtyA = Math.Truncate(sumRankA / ledRanksQty.Item1 / smtCarrier) * smtCarrier;
            double maxProductQtyB = Math.Truncate(sumRankB / ledRanksQty.Item2 / smtCarrier) * smtCarrier;
            labelMaxProductQty.Text = "Maksymalna ilość zlecenia: " + Math.Min(maxProductQtyA, maxProductQtyB);
        }

        private void CheckIfCorrectLed()
        {
            bool error = false;
            string errorDescription = "";
            List<string> loadedIds = new List<string>();
            string expectedRankA = labelRankA.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];
            string expectedRankB = labelRankB.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];
            List<Tuple<int, int>> errorCellsA = new List<Tuple<int, int>>();
            List<Tuple<int, int>> errorCellsB = new List<Tuple<int, int>>();
            Dictionary<string, List<Tuple<DataGridView, int, int>>> idToCellAdress = new Dictionary<string, List<Tuple<DataGridView, int, int>>>();

            HashSet<int> errorRowsA = new HashSet<int>();
            HashSet<int> errorRowsB = new HashSet<int>();
            string lotNumber = textBoxLotNo.Text;

            foreach (DataGridViewRow row in dataGridViewRankA.Rows)
            {
                string rank = row.Cells["RankA"].Value.ToString();
                string id = row.Cells["rankAId"].Value.ToString();

                if (!idToCellAdress.ContainsKey(id))
                {
                    idToCellAdress.Add(id, new List<Tuple<DataGridView, int, int>>());
                }
                idToCellAdress[id].Add(new Tuple<DataGridView, int, int>(dataGridViewRankA, row.Index, row.Cells.IndexOf(row.Cells["rankAId"])));
                

                if (rank != expectedRankA)
                {
                    error = true;
                    errorCellsA.Add(new Tuple<int, int>(row.Index, row.Cells.IndexOf(row.Cells["RankA"])));
                    errorDescription += Environment.NewLine + "Rank BINA nie zgadza się. Powinno być: " + rank;
                    errorRowsA.Add(row.Index);
                }

                if (lotNumber!= row.Cells["RankAZlecenie"].Value.ToString())
                {
                    error = true;
                    errorDescription += Environment.NewLine + "Ta rolka LED jest przypisana do innego zlecenia -" + row.Cells["RankAZlecenie"].Value.ToString();
                    errorRowsA.Add(row.Index);
                    errorCellsA.Add(new Tuple<int, int>(row.Index, row.Cells.IndexOf(row.Cells["RankAZlecenie"])));
                }
            }

            foreach (DataGridViewRow row in dataGridViewRankB.Rows)
            {
                string rank = row.Cells["RankB"].Value.ToString();
                string id = row.Cells["rankBId"].Value.ToString();

                if (!idToCellAdress.ContainsKey(id))
                {
                    idToCellAdress.Add(id, new List<Tuple<DataGridView, int, int>>());
                }
                idToCellAdress[id].Add(new Tuple<DataGridView, int, int>(dataGridViewRankB, row.Index, row.Cells.IndexOf(row.Cells["rankBId"])));

                if (rank != expectedRankB)
                {
                    error = true;
                    errorDescription += Environment.NewLine + "Rank BINA nie zgadza się. Powinno być: " + rank;
                    errorRowsB.Add(row.Index);
                    errorCellsB.Add(new Tuple<int, int>(row.Index, row.Cells.IndexOf(row.Cells["RankB"])));
                }
                if (lotNumber != row.Cells["RankBZlecenie"].Value.ToString())
                {
                    error = true;
                    errorDescription += Environment.NewLine + "Ta rolka LED jest przypisana do innego zlecenia -" + row.Cells["RankBZlecenie"].Value.ToString();
                    errorRowsB.Add(row.Index);
                    errorCellsB.Add(new Tuple<int, int>(row.Index, row.Cells.IndexOf(row.Cells["RankBZlecenie"])));
                }
            }

            foreach (var idEntry in idToCellAdress) 
            {
                if (idEntry.Value.Count > 1) 
                {
                    foreach (var id in idEntry.Value)
                    {
                        id.Item1.Rows[id.Item2].Cells[id.Item3].Style.BackColor = Color.Red;
                    }
                    errorDescription += Environment.NewLine + "Rolka ID:" + idEntry.Key + " została już wcześniej dodana!";
                    error = true;
                }
            }

            if (error)
            {
                foreach (var aCoord in errorCellsA)
                {
                    dataGridViewRankA.Rows[aCoord.Item1].Cells[aCoord.Item2].Style.BackColor = Color.Red;
                }
                foreach (var bCoord in errorCellsB)
                {
                    dataGridViewRankB.Rows[bCoord.Item1].Cells[bCoord.Item2].Style.BackColor = Color.Red;
                }
                MessageBox.Show(errorDescription);
            }
        }

        private void CheckIfFormDataComplete()
        {
            bool correct = true;
            buttonOK.Text = "UZUPEŁNIJ DANE ";

            foreach (DataGridViewRow row  in dataGridViewRankA.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.BackColor==Color.Red)
                    {
                        correct = false;
                        buttonOK.Text += "-Błąd RankA";
                        break;
                    }
                }
            }


                foreach (DataGridViewRow row in dataGridViewRankB.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Style.BackColor == Color.Red)
                        {
                            correct = false;
                            buttonOK.Text += "-Błąd RankB";
                            break;
                        }
                    }
                }


            if (comboBoxOperator.Text.Length < 5)
            {
                correct = false;
                buttonOK.Text += "-Operator";
            }

            if (dataGridViewRankA.Rows.Count != dataGridViewRankB.Rows.Count)
            {
                correct = false;
                buttonOK.Text += "-Ilość A/B";
            }

            if (dataGridViewRankA.Rows.Count == 0 & dataGridViewRankB.Rows.Count==0)
            {
                buttonOK.Text = "UZUPEŁNIJ DANE ";
                correct = false;
            }


            if (correct)
            {
                buttonOK.Text = "OK";
            }
            else
            {
                //buttonOK.Text = "UZUPEŁNIJ DANE";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Return)
            {
                if (textBoxRankAQr.Text.Split('\t').Length > 5)
                {
                    currentLedReel = SqlOperations.GetLedDataFromSparing(textBoxRankAQr.Text);
                    if (currentLedReel.ID != "error")
                    {
                        dataGridViewRankA.Rows.Add(currentLedReel.NC12, currentLedReel.ID, currentLedReel.Rank, currentLedReel.Ilosc, currentLedReel.ZlecenieString);

                        foreach (DataGridViewColumn col in dataGridViewRankA.Columns)
                        {
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        textBoxRankAQr.Text = "";
                        CheckIfCorrectLed();
                        textBoxRankBQr.Focus();
                        labelRankALoaded.Text += Environment.NewLine + currentLedReel.Rank;

                        string ledFamily = currentLedReel.NC12;
                        if (!labelLed12NC.Text.Contains(ledFamily))
                        {
                            labelLed12NC.Text += " " + ledFamily;
                        }

                        CountQtyProductToManufacture();
                        CheckIfFormDataComplete();
                    }
                }
                else
                {
                    textBoxRankAQr.Text = "";
                    MessageBox.Show("Niewłaściwy kod QR!");
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (buttonOK.Text=="OK")
            {
                grid.SuspendLayout();
                LedLeftovers ledsLeft = new LedLeftovers(new List<RankStruc>(), new List<RankStruc>());
                
                foreach (DataGridViewRow rowA in dataGridViewRankA.Rows)
                {
                    string rankA = labelRankA.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)[1];
                    string rankAId = rowA.Cells["RankAId"].Value.ToString();
                    string rankA12NC= rowA.Cells["RankANc12"].Value.ToString();
                    string iloscAString = rowA.Cells["RankAIlosc"].Value.ToString();
                    double rankAIlosc = double.Parse(iloscAString); 
                    ledsLeft.RankA.Add(new RankStruc(rankA, rankAId, rankA12NC, -1));
                }

                foreach (DataGridViewRow rowB in dataGridViewRankB.Rows)
                {
                    string rankB = labelRankB.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)[1];
                    string rankBId = rowB.Cells["rankBId"].Value.ToString();
                    string rankB12NC = rowB.Cells["RankBNc12"].Value.ToString();
                    string iloscBString = rowB.Cells["rankBIlosc"].Value.ToString();
                    double rankBIlosc = double.Parse(iloscBString);
                    ledsLeft.RankB.Add(new RankStruc(rankB, rankBId, rankB12NC, -1));
                }

                grid.Rows.Insert(0, 1);
                int lastRow = 0;
                grid.Rows[lastRow].Cells["ColumnSaved"].Style.BackColor = Color.Red;
                grid.Rows[lastRow].Cells["ColumnLot"].Value = textBoxLotNo.Text;
                grid.Rows[lastRow].Cells["ColumnModel"].Value = currentLotData.Model;
                grid.Rows[lastRow].Cells["ColumnQty"].Value = currentLotData.OrderedQty;
                grid.Rows[lastRow].Cells["ColumnRankA"].Value = currentLotData.RankA;
                grid.Rows[lastRow].Cells["ColumnRankB"].Value = currentLotData.RankB;
                grid.Rows[lastRow].Cells["Rank12NC"].Value = ledsLeft.RankA[0].Nc12;
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Value = "BRAK";
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Style.BackColor = Color.Red;
                grid.Rows[lastRow].Cells["ColumnQualityCheck"].Value = "";
                grid.Rows[lastRow].Cells["StartDate"].Value = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Tag = ledsLeft;
                grid.Rows[lastRow].Cells["Operator"].Value = comboBoxOperator.Text;

                grid.Rows[lastRow].Cells["connQty"].Value = Tools.GetNumberOfConnectors(currentLotData.Model);
                if (grid.Rows[lastRow].Cells["connQty"].Value.ToString()=="4")
                {
                    //grid.Rows[lastRow].Cells["connQty"].Style.ForeColor = System.Drawing.Color.Red;
                    grid.Rows[lastRow].Cells["connQty"].Style.ForeColor = Color.White;
                    grid.Rows[lastRow].Cells["connQty"].Style.BackColor = Color.DarkCyan;
                }

                if (grid.Rows.Count == 1)
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                if (grid.Rows.Count==1)
                {
                    grid.Rows[lastRow].Cells["ColumnQualityCheck"].Style.BackColor = Color.Red;
                    grid.Rows[lastRow].Cells["ColumnQualityCheck"].Value = "BRAK";
                }
                else
                {
                    if (Tools.getCellValue(grid.Rows[lastRow + 1].Cells["ColumnModel"]) != "")
                        if (grid.Rows[lastRow].Cells["ColumnModel"].Value.ToString() != grid.Rows[lastRow + 1].Cells["ColumnModel"].Value.ToString())
                        {
                            grid.Rows[lastRow].Cells["ColumnQualityCheck"].Style.BackColor = Color.Red;
                            grid.Rows[lastRow].Cells["ColumnQualityCheck"].Value = "BRAK";
                            
                        }
                }

                if (radioButtonNewStencil.Checked)
                {
                    //Stencil QR reading
                    //ScanStencilQr fm = new ScanStencilQr(grid.Rows[lastRow].Cells["Stencil"]);
                    //fm.ShowDialog();
                }

                Tools.CleanUpDgv(grid);
                grid.FirstDisplayedScrollingRowIndex = 0;
                this.Close();
                grid.ResumeLayout();
            }
            else
            {
            #if DEBUG
                grid.Columns["Stencil"].Visible = true;
                grid.Rows.Insert(0, 1);
                grid.Rows[0].Cells["StartDate"].Value = System.DateTime.Now.ToLongTimeString();
                grid.Rows[0].Cells["ColumnSaved"].Style.BackColor = Color.Red;
                ScanStencilQr fm = new ScanStencilQr(grid.Rows[0].Cells["Stencil"]);
                fm.ShowDialog();

            #endif
            }

        }

        private void textBoxRankBQr_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                currentLedReel = SqlOperations.GetLedDataFromSparing(textBoxRankBQr.Text);
                if (currentLedReel.ID != "error")
                {
                    dataGridViewRankB.Rows.Add(currentLedReel.NC12, currentLedReel.ID, currentLedReel.Rank, currentLedReel.Ilosc, currentLedReel.ZlecenieString);
                    CheckIfCorrectLed();
                    foreach (DataGridViewColumn col in dataGridViewRankB.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    textBoxRankBQr.Text = "";
                    textBoxRankAQr.Focus();
                    labelRankBLoaded.Text += Environment.NewLine + currentLedReel.Rank;
                    CountQtyProductToManufacture();
                    CheckIfFormDataComplete();
                }
            }
        }

        private void textBoxLotNo_Leave(object sender, EventArgs e)
        {
            if (labelModel.Text == "Model:") 
            {
                this.ActiveControl = textBoxLotNo;
            }
        }

        private void textBoxEnter(object sender, EventArgs e)
        {
            foreach (TextBox txb in txbList)
            {
                if (txb.Focused) txb.BackColor = Color.LemonChiffon;
                else txb.BackColor = Color.White;
            }
        }

        private void NewLotForm_Load(object sender, EventArgs e)
        {
            // comboBoxOperator.Items.AddRange(SqlOperations.GetLastOperators());
            comboBoxOperator.Items.AddRange(SqlOperations.GetOperatorsArray());
            //Stencil QR reading
            //radioButtonCurrentStencil.Visible = true;
            //radioButtonNewStencil.Visible = true;
        }

        private void dataGridViewRankA_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewRankA.ClearSelection();
        }

        private void dataGridViewRankB_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewRankB.ClearSelection();
        }

        private void comboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIfFormDataComplete();
        }

        private void comboBoxOperator_TextChanged(object sender, EventArgs e)
        {
            CheckIfFormDataComplete();
        }

        private void comboBoxOperator_Leave(object sender, EventArgs e)
        {
            comboBoxOperator.Text = comboBoxOperator.Text.ToUpper().Trim();
        }
        
        private void textBoxLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Return)
            {
                currentLotData = SqlOperations.GetLotData(textBoxLotNo.Text);
                if (currentLotData.Model.Length > 0)
                {
                    labelModel.Text = "Model: " + currentLotData.Model;
                    labelOrderedQty.Text = "Ilość " + currentLotData.OrderedQty.ToString();
                    labelRankA.Text = "Rank A" + Environment.NewLine + currentLotData.RankA;
                    labelRankB.Text = "Rank B" + Environment.NewLine + currentLotData.RankB;

                    string expectedMiraeProgram = currentLotData.Model.Remove(6, 1).Insert(6, "X");

                    if (checkMirae)
                        labelMiraeProgram.Text = "Mirae program: " + currentMiraeProgram;
                    else
                        labelMiraeProgram.Text = "";

                    if (currentMiraeProgram == expectedMiraeProgram || !checkMirae)
                    {
                        labelMiraeProgram.Text = labelMiraeProgram.Text + " OK";
                        textBoxRankAQr.Visible = true;
                        textBoxRankBQr.Visible = true;
                        textBoxRankAQr.Focus();
                    }
                    else
                    {
                        labelMiraeProgram.Text = labelMiraeProgram.Text + Environment.NewLine + " ZŁY PROGRAM!";
                        labelMiraeProgram.ForeColor = Color.Red;
                        labelMiraeProgram.Font = new Font(labelMiraeProgram.Font, FontStyle.Bold);
                    }

                    ledRanksQty = SqlOperations.MaxRankQty(currentLotData.Model);
                    labelLedQty.Text = "RankA=" + ledRanksQty.Item1 + " RankB=" + ledRanksQty.Item2;
                    labelLotData.Text = "Dane zlecenia nr. " + textBoxLotNo.Text;
                    textBoxRankAQr.Focus();
                }
                else
                {
                    labelModel.Text = "Brak zlecenia w bazie danych";
                    labelOrderedQty.Text = "";
                    labelRankA.Text = "";
                    labelRankB.Text = "";
                    textBoxRankAQr.Visible = false;
                    textBoxRankBQr.Visible = false;
                }

                string prevModel = "";

                if (grid.Rows.Count > 0)
                {
                    prevModel = Tools.getCellValue(grid.Rows[0].Cells["ColumnModel"]); 
                    if (prevModel != "")
                        if (prevModel == currentLotData.Model)
                        {
                            string stencil = Tools.getCellValue(grid.Rows[0].Cells["Stencil"]);
                            if (stencil != "")
                            {
                                radioButtonCurrentStencil.Enabled = true;
                                radioButtonCurrentStencil.Checked = true;
                                radioButtonCurrentStencil.Text = "Aktualny: " + stencil;
                            }
                        }
                }
            }
        }

        private void textBoxRankAQr_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab) e.IsInputKey = true;
        }

        private void textBoxRankBQr_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           // if (e.KeyCode == Keys.Tab) e.IsInputKey = true;
        }
    }
}
