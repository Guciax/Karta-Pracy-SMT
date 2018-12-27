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
    public partial class VirtualKeyboard : Form
    {
        private readonly DataGridViewCell cell;

        public VirtualKeyboard(DataGridViewCell cell)
        {
            InitializeComponent();
            this.cell = cell;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxDisplay.Text.Length>0)
                textBoxDisplay.Text = textBoxDisplay.Text.Substring(0,textBoxDisplay.Text.Length - 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "0";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cell.Value = textBoxDisplay.Text;
            this.Close();
        }

        private void NumericKeyPressed(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            textBoxDisplay.Text += btn.Text;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
