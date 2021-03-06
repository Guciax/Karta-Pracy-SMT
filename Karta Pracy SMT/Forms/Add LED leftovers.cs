﻿using System;
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
                if (reel.QtyLeft < 0)
                    qty = "";
                else
                    qty = reel.QtyLeft.ToString();
                dataGridViewRankA.Rows.Add(reel.Nc12,  reel.Rank, reel.ID, qty);
            }
            foreach (var reel in ledsLeft.RankB)
            {
                string qty = "";
                if (reel.QtyLeft < 0)
                    qty = "";
                else
                    qty = reel.QtyLeft.ToString();
                dataGridViewRankB.Rows.Add(reel.Nc12, reel.Rank, reel.ID, qty);
            }
            
            Tools.AutoSizeColumnsWidth(dataGridViewRankA);
            Tools.AutoSizeColumnsWidth(dataGridViewRankB);

            this.ActiveControl = textBoxQr;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool everyQtyFilled = true;
            bool ledWasteAllert = false;

            for (int i = 0; i < dataGridViewRankA.Rows.Count; i++) 
            {
                double qtyA = 0;
                double qtyB = 0;

                if (dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString() != "")
                {
                    if (!double.TryParse(dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString(), out qtyA) || dataGridViewRankA.Rows[i].Cells["RankAIlosc"].Value.ToString() == "")
                    {
                        everyQtyFilled = false;

                    }
                    else
                    {
                        ledsLeft.RankA[i].QtyLeft = qtyA;
                        if (qtyA == 0)
                        {
                            ledWasteAllert = true;
                        }
                    }
                }
                else everyQtyFilled = false;

                if (dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString() != "")
                {
                    if (!double.TryParse(dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString(), out qtyB) || dataGridViewRankB.Rows[i].Cells["RankBIlosc"].Value.ToString() == "")
                    {
                        everyQtyFilled = false;
                    }
                    ledsLeft.RankB[i].QtyLeft = qtyB;
                }
                else
                {
                    everyQtyFilled = false;
                    if (qtyB == 0)
                    {
                        ledWasteAllert = true;
                    }
                }
            }

            if (everyQtyFilled)
            {
                cell.Value = "OK";
                cell.Style.BackColor = Color.Green;
                //SqlOperations.UpdateLedLeftovers(ledsLeft);
                ledLeftSaveBuffer.Add(ledsLeft);
            }

            this.Close();
            if (ledWasteAllert) 
            {
                MessageBox.Show("UWAGA!" + Environment.NewLine + "Duży odpad LED!" + Environment.NewLine + "Powiadom technika.");
            }
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
                string ledID = textBoxQr.Text.ToUpper().Split('\t')[5];
                string nc12 = textBoxQr.Text.ToUpper().Split('\t')[0];

                for (int a = 0; a < dataGridViewRankA.Rows.Count; a++) 
                {
                    if (dataGridViewRankA.Rows[a].Cells[0].Value.ToString().ToUpper() ==nc12 & dataGridViewRankA.Rows[a].Cells[2].Value.ToString().ToUpper() ==ledID)
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
                        if (dataGridViewRankB.Rows[b].Cells[0].Value.ToString().ToUpper() == nc12 & dataGridViewRankB.Rows[b].Cells[2].Value.ToString().ToUpper() == ledID)
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
                    MessageBox.Show("Ta rolka LED nie należy do tego zlecenia");
                }
            }
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void Add_LED_leftovers_Load(object sender, EventArgs e)
        {

            textBoxQr.Focus();
        }

        private void Add_LED_leftovers_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
