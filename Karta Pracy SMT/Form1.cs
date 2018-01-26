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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLotForm fmNewLot = new NewLotForm(this);
            fmNewLot.Show();
        }

        public DataGridView grid
        {
            get { return dataGridView1; }
            set { dataGridView1 = grid; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (dataGridView1.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
            {
                MessageBox.Show(cell.Value.ToString());
            }

        }
    }
}
