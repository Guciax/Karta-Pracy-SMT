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
    public partial class Add_LED_leftovers : Form
    {
        private readonly DataGridView grid;
        private readonly DataGridViewCell cell;
        private List<LedLeftovers> ledLeftSaveBuffer;
        LedLeftovers ledsLeft;

        public Add_LED_leftovers(DataGridView grid,DataGridViewCell cell, List<LedLeftovers> ledLeftSaveBuffer)
        {
            InitializeComponent();
            this.grid = grid;
            this.cell = cell;
            this.ledLeftSaveBuffer = ledLeftSaveBuffer;
            ledsLeft = (LedLeftovers)cell.Tag;

            foreach (var reel in ledsLeft.RankA)
            {
                string qty = "";
                if (reel.Qty < 0)
                    qty = "";
                else
                    qty = reel.Qty.ToString();
                dataGridViewRankA.Rows.Add(reel.Nc12,  reel.Rank, reel.ID, qty);
            }
            foreach (var reel in ledsLeft.RankB)
            {
                string qty = "";
                if (reel.Qty < 0)
                    qty = "";
                else
                    qty = reel.Qty.ToString();
                dataGridViewRankB.Rows.Add(reel.Nc12, reel.Rank, reel.ID, qty);
            }
            
            Tools.AutoSizeColumnsWidth(dataGridViewRankA);
            Tools.AutoSizeColumnsWidth(dataGridViewRankB);

            this.ActiveControl = textBoxQr;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool everyQtyFilled = true;
            for (int i = 0; i < dataGridViewRankA.Rows.Count; i++) 
            {
                double qtyA = 0;
                double qtyB = 0;

                if (dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString() != "")
                {
                    if (!double.TryParse(dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString(), out qtyA) || dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString() == "")
                    {
                        everyQtyFilled = false;

                    } else ledsLeft.RankA[i].Qty = qtyA;
                }
                else everyQtyFilled = false;

                if (dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString() != "")
                {
                    if (!double.TryParse(dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString(), out qtyB) || dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString() == "")
                    {
                        everyQtyFilled = false;
                    }
                    ledsLeft.RankB[i].Qty = qtyB;
                } else everyQtyFilled = false;


                
                
            }

            if (everyQtyFilled)
            {
                cell.Value = "OK";
                cell.Style.BackColor = Color.Green;

                //SqlOperations.UpdateLedLeftovers(ledsLeft);
                ledLeftSaveBuffer.Add(ledsLeft);
            }

            this.Close();
        }

        private void textBoxQr_Leave(object sender, EventArgs e)
        {
            textBoxQr.Focus();
        }

        private void textBoxQr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return & textBoxQr.Text.Split('\t').Length>4) 
            {
                bool foundLedReel = false;
                string ledID = textBoxQr.Text.Split('\t')[5];
                string nc12 = textBoxQr.Text.Split('\t')[0];

                for (int a = 0; a < dataGridViewRankA.Rows.Count; a++) 
                {
                    if (dataGridViewRankA.Rows[a].Cells[0].Value.ToString()==nc12 & dataGridViewRankA.Rows[a].Cells[2].Value.ToString() ==ledID)
                    {
                        VirtualKeyboard kbForm = new VirtualKeyboard(dataGridViewRankA.Rows[a].Cells[3]);
                        kbForm.ShowInTaskbar = false;
                        kbForm.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                        kbForm.ShowDialog();
                        textBoxQr.Text = "";
                        foundLedReel = true;
                    }
                }

                if (!foundLedReel)
                    for (int b = 0; b < dataGridViewRankB.Rows.Count; b++)
                    {
                        if (dataGridViewRankB.Rows[b].Cells[0].Value.ToString() == nc12 & dataGridViewRankB.Rows[b].Cells[2].Value.ToString() == ledID)
                        {
                            VirtualKeyboard kbForm = new VirtualKeyboard(dataGridViewRankB.Rows[b].Cells[3]);
                            kbForm.ShowInTaskbar = false;
                            kbForm.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                            kbForm.ShowDialog();
                            textBoxQr.Text = "";
                            foundLedReel = true;
                        }
                    }

                if (!foundLedReel)
                {
                    MessageBox.Show("Podana rolka LED nie należy do tego zlecenia");
                }
            }

        }

        private void Add_LED_leftovers_Load(object sender, EventArgs e)
        {
            textBoxQr.Focus();
        }
    }
}
