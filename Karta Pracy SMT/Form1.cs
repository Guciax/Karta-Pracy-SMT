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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLotForm fmNewLot = new NewLotForm();
            fmNewLot.Show();
        }

        public DataGridView grid
        {
            get { return dataGridView1; }
            set { dataGridView1 = grid; }
        }
    }
}
