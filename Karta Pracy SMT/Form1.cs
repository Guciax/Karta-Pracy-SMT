using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLotForm fmNewLot = new NewLotForm(this, dataGridView1);
            fmNewLot.Show();
        }

        //public DataGridView grid
        //{
        //    get { return dataGridView1; }
        //    set { dataGridView1 = grid; }
        //}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                LedLeftovers clickedLeftovers = (LedLeftovers)cell.Tag;

                NewLotForm fmNewLot = new Add_LED_leftovers(dataGridView1)
                fmNewLot.Show();
            }

        }
    }
}
