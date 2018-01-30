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
    public partial class ChangeOverConfirmationCard : Form
    {
        public DataGridViewCell Cell { get; }

        public ChangeOverConfirmationCard(DataGridViewCell cell)
        {
            InitializeComponent();
            Cell = cell;
        }

        private void checkBoxTechncian_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTechncian.Checked) checkBoxTechncian.Text = "OK"; else checkBoxTechncian.Text = "";
        }

        private void checkBoxInspector_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInspector.Checked) checkBoxInspector.Text = "OK"; else checkBoxInspector.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxInspector.Checked & checkBoxTechncian.Checked)
            {
                Cell.Value = "OK";
                Cell.Style.BackColor = Color.Green;
                this.Close();
            }
        }
    }
}
