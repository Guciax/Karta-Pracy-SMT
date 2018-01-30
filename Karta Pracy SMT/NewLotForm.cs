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
        

        public NewLotForm(MainForm callingForm, DataGridView grid)
        {
            InitializeComponent();
            opener = callingForm as MainForm;
            this.grid = grid;
        }

        private void textBoxLotNo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLotNo.Text.Length > 5)
            {
                currentLotData = SqlOperations.GetLotData(textBoxLotNo.Text);
                if (currentLotData.Model.Length > 0)
                {
                    labelModel.Text = "Model: " + currentLotData.Model;
                    labelOrderedQty.Text = "Ilość " + currentLotData.OrderedQty.ToString();
                    labelRankA.Text = "Rank A" + Environment.NewLine + currentLotData.RankA;
                    labelRankB.Text = "Rank B" + Environment.NewLine + currentLotData.RankB;
                    textBoxRankAQr.Visible = true;
                    textBoxRankBQr.Visible = true;
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
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                currentLedReel = SqlOperations.GetLedDataFromSparing(textBoxRankAQr.Text);

                dataGridViewRankA.Rows.Add(currentLedReel.NC12, currentLedReel.ID, currentLedReel.Ilosc, currentLedReel.ZlecenieString);
                if (currentLedReel.ZlecenieString == textBoxRankAQr.Text)
                {
                    foreach (DataGridViewCell cell in dataGridViewRankA.Rows[dataGridViewRankA.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Green;
                        cell.Style.ForeColor = Color.Green;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridViewRankA.Rows[dataGridViewRankA.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Red;
                        cell.Style.ForeColor = Color.Red;
                    }
                }

                    foreach (DataGridViewColumn col in dataGridViewRankA.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                textBoxRankAQr.Text = "";
            }
        }

        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (dataGridViewRankA.Rows.Count == dataGridViewRankB.Rows.Count)
            {
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

                grid.Rows.Add();
                int lastRow = grid.Rows.Count - 1;
                grid.Rows[lastRow].Cells["ColumnLot"].Value = textBoxLotNo.Text;
                grid.Rows[lastRow].Cells["ColumnModel"].Value = currentLotData.Model;
                grid.Rows[lastRow].Cells["ColumnQty"].Value = currentLotData.OrderedQty;
                grid.Rows[lastRow].Cells["ColumnRankA"].Value = currentLotData.RankA;
                grid.Rows[lastRow].Cells["ColumnRankB"].Value = currentLotData.RankB;
                grid.Rows[lastRow].Cells["Rank12NC"].Value = ledsLeft.RankA[0].Nc12;
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Value = "BRAK";
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Style.BackColor = Color.Red;
                grid.Rows[lastRow].Cells["ColumnQualityCheck"].Value = "";
                grid.Rows[lastRow].Cells["StartDate"].Value = DateTime.Now.ToLongTimeString();
                grid.Rows[lastRow].Cells["BtnSave"].Value = "";
                grid.Rows[lastRow].Cells["ColumnButtonLed"].Tag = ledsLeft;

                //foreach (DataGridViewCell cell in grid.Rows[lastRow].Cells)
                //{
                //    if (cell.GetType().ToString() == "System.Windows.Forms.DataGridViewButtonCell")
                //    {
                //        ((DataGridViewButtonCell)cell).FlatStyle = FlatStyle.Flat;
                //    }
                //}

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
                    if (grid.Rows[lastRow].Cells["ColumnModel"].Value.ToString() != grid.Rows[lastRow-1].Cells["ColumnModel"].Value.ToString())
                    {
                        grid.Rows[lastRow].Cells["ColumnQualityCheck"].Style.BackColor = Color.Red;
                        grid.Rows[lastRow].Cells["ColumnQualityCheck"].Value = "BRAK";
                    }
                }

                this.Close();
            }
        }



        private void textBoxRankBQr_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                currentLedReel = SqlOperations.GetLedDataFromSparing(textBoxRankBQr.Text);

                dataGridViewRankB.Rows.Add(currentLedReel.NC12, currentLedReel.ID, currentLedReel.Ilosc, currentLedReel.ZlecenieString);
                if (currentLedReel.ZlecenieString == textBoxRankBQr.Text)
                {
                    foreach (DataGridViewCell cell in dataGridViewRankB.Rows[dataGridViewRankB.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Green;
                        cell.Style.ForeColor = Color.Green;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridViewRankB.Rows[dataGridViewRankB.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Red;
                        cell.Style.ForeColor = Color.Red;
                    }
                }

                foreach (DataGridViewColumn col in dataGridViewRankB.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                textBoxRankBQr.Text = "";
            }
        
        }
    }
}
