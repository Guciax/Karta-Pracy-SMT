using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT.Forms
{
    public partial class AddMstLedReel : Form
    {
        public string ScannedQr = "";
        public AddMstLedReel()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Return)
            {
                if (textBox1.Text.Trim()!="")
                {
                    ScannedQr = textBox1.Text;
                    this.Close();
                }
            }
        }
    }
}
