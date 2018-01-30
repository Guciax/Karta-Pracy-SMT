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
        LedLeftovers ledsLeft;
        public Add_LED_leftovers(DataGridView grid,DataGridViewCell cell)
        {
            InitializeComponent();
            this.grid = grid;
            this.cell = cell;
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
            }

            this.Close();
        }
    }
}
