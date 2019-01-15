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
    public partial class ReadQrForm : Form
    {
        public string id = "";
        public string nc12 = "";
        
        public ReadQrForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string[] split = textBox1.Text.Split('\t');
                if (split.Length > 4)
                {
                    id = split[5];
                    nc12 = split[0];
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    textBox1.Text = "";
                }
            }
        }

        private void ReadQrForm_Load(object sender, EventArgs e)
        {

        }
    }
}
