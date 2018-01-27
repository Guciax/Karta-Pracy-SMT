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
        LedLeftovers ledsLeft;

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

                labelModel.Text = "Model: " + currentLotData.Model;
                labelOrderedQty.Text = "Ilość " + currentLotData.OrderedQty.ToString();
                labelRankA.Text = "Rank A" + Environment.NewLine + currentLotData.RankA;
                labelRankB.Text = "Rank B" + Environment.NewLine + currentLotData.RankB;
                
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                currentLedReel = SqlOperations.GetLedDataFromSparing(textBoxRankAQr.Text);

                dataGridViewRankA.Rows.Add(currentLedReel.NC12, currentLedReel.Ilosc, currentLedReel.LPN_ID, currentLedReel.LPN_NC, currentLedReel.ZlecenieString);
                if (currentLedReel.ZlecenieString == textBoxRankAQr.Text)
                {
                    foreach (DataGridViewCell cell in dataGridViewRankA.Rows[dataGridViewRankA.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Green;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridViewRankA.Rows[dataGridViewRankA.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Red;
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
                grid.Rows.Add(false, textBoxLotNo.Text, currentLotData.Model, "", currentLotData.OrderedQty, currentLotData.RankA, new DataGridViewButtonCell(), currentLotData.RankB, new DataGridViewButtonCell(), new DataGridViewButtonCell(), System.DateTime.Now.ToShortDateString());
                grid.Rows[grid.Rows.Count - 1].Cells["RankAInfo"].Value = "INFO";
                grid.Rows[grid.Rows.Count - 1].Cells["RankBInfo"].Value = "INFO";
                grid.Rows[grid.Rows.Count - 1].Cells["ColumnButtonLed"].Value = "Brak";
                grid.Rows[grid.Rows.Count - 1].Cells["ColumnButtonLed"].Style.BackColor = Color.Red;

                foreach (DataGridViewRow row in dataGridViewRankA.Rows)
                {
                    ledsLeft.RankA.Add(new RankStruc(labelRankA.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)[1], row.Cells["RankAId"].ToString(), row.Cells["RankANc12"].ToString(), double.Parse(row.Cells["RankAIlosc"].ToString())));
                    ledsLeft.RankB.Add(new RankStruc(labelRankB.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None)[1], row.Cells["RankBId"].ToString(), row.Cells["RankBNc12"].ToString(), double.Parse(row.Cells["RankBIlosc"].ToString())));
                }

                grid.Rows[grid.Rows.Count - 1].Cells["ColumnButtonLed"].Tag = ledsLeft;

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                this.Close();
            }
        }
    }
}
