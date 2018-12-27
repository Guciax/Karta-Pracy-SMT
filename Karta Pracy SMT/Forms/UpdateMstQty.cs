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
    public partial class UpdateMstQty : Form
    {
        private readonly DateTime lastUpdate;
        private readonly int previousQty;
        private readonly int pcbOnMb;

        private Int32 currentQty = 0;
        public Int32 newTotalQty = 0;

        public UpdateMstQty(DateTime lastUpdate, Int32 previousQty, int pcbOnMb)
        {
            InitializeComponent();
            this.lastUpdate = lastUpdate;
            this.previousQty = previousQty;
            this.pcbOnMb = pcbOnMb;
            
        }

        private void UpdateMstQty_Load(object sender, EventArgs e)
        {
            label1.Text += previousQty.ToString() + " PCB / " + (previousQty / pcbOnMb).ToString() + "MB"; ;
            labelMbCalsTotal.Text = "x" + pcbOnMb + "= ";
            labelMin.Text = "(min. " + previousQty / pcbOnMb + ")";
            numericUpDownMbQty.Minimum = previousQty / pcbOnMb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text=="OK")
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void numericUpDownMbQty_ValueChanged(object sender, EventArgs e)
        {
            newTotalQty = (int)numericUpDownMbQty.Value * pcbOnMb;
            labelMbCalsTotal.Text = "x" + pcbOnMb + " = " + (newTotalQty).ToString() + "szt. PCB";
            
        }
    }
}
