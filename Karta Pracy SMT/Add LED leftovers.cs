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
        private readonly LedLeftovers ledsLeft;

        public Add_LED_leftovers(DataGridView grid,LedLeftovers ledsLeft)
        {
            InitializeComponent();
            this.grid = grid;
            this.ledsLeft = ledsLeft;

            foreach (var reel in ledsLeft.RankA)
            {
                dataGridViewRankA.Rows.Add(reel.Nc12, reel.ID, reel.Qty);
            }
            foreach (var reel in ledsLeft.RankB)
            {
                dataGridViewRankB.Rows.Add(reel.Nc12, reel.ID, reel.Qty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
