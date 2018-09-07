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
    public partial class ScanStencilQr : Form
    {
        private readonly string model;
        public string stencil { get; set; }

        public ScanStencilQr(string model)
        {
            InitializeComponent();
            this.model = model;
        }

        private void ScanStencilQr_Load(object sender, EventArgs e)
        {
            labelTitle.Text = "Zeskanuj kod stencila" + Environment.NewLine + "Model: " + model;
            this.ActiveControl = textBox1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return & textBox1.Text.Trim()!="")
            {
                this.stencil = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
