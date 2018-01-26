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
        Form opener;
        public NewLotForm()
        {
            InitializeComponent();
            opener = ParentForm as Form1;
        }

        private void textBoxLotNo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLotNo.Text.Length > 5)
            {
                LotData currentLotData = SqlOperations.GetLotData(textBoxLotNo.Text);

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
                ledReelData leedReel = SqlOperations.GetLedDataFromSparing(textBox1.Text);

                dataGridView1.Rows.Add(leedReel.NC12, leedReel.Ilosc, leedReel.LPN_ID, leedReel.LPN_NC, leedReel.ZlecenieString);
                if (leedReel.ZlecenieString == textBox1.Text)
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
            
        }
    }
}
