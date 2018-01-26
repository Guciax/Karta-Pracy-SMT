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
        Form1 opener;
        ledReelData currentLedReel;
        LotData currentLotData;

        public NewLotForm(Form1 callingForm)
        {
            InitializeComponent();
            opener = callingForm as Form1;
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
                currentLedReel = SqlOperations.GetLedDataFromSparing(textBox1.Text);

                dataGridView1.Rows.Add(currentLedReel.NC12, currentLedReel.Ilosc, currentLedReel.LPN_ID, currentLedReel.LPN_NC, currentLedReel.ZlecenieString);
                if (currentLedReel.ZlecenieString == textBox1.Text)
                {
                    foreach (DataGridViewCell cell in dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Green;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells)
                    {
                        cell.Style.BackColor = Color.Red;
                    }
                }

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                textBox1.Text = "";
            }
        }

        
        private void buttonOK_Click(object sender, EventArgs e)
        {

            opener.grid.Rows.Add(false, textBoxLotNo.Text, currentLotData.Model, "", currentLotData.OrderedQty, currentLotData.RankA, new DataGridViewButtonCell(), currentLotData.RankB, new DataGridViewButtonCell(), new DataGridViewButtonCell(), System.DateTime.Now.ToShortDateString());
            opener.grid.Rows[opener.grid.Rows.Count - 1].Cells["RankAInfo"].Value = "INFO";
            opener.grid.Rows[opener.grid.Rows.Count - 1].Cells["RankBInfo"].Value = "INFO";
            opener.grid.Rows[opener.grid.Rows.Count - 1].Cells["ColumnButtonLed"] .Value = "Brak";
            opener.grid.Rows[opener.grid.Rows.Count - 1].Cells["ColumnButtonLed"].Style.BackColor = Color.Red;
            foreach (DataGridViewColumn col in opener.grid.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            this.Close();

        }
    }
}
