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
            if (button1.Text == "OK")
            {
                if (checkBoxInspector.Checked & checkBoxTechncian.Checked)
                {
                    Cell.Value = "OK";
                    Cell.Style.BackColor = Color.Green;
                    Cell.Tag = comboBoxInspect.Text.ToUpper() + ";" + comboBoxTechn.Text.ToUpper();
                    this.Close();
                }
            }
        }

        private void ChangeOverConfirmationCard_Load(object sender, EventArgs e)
        {
            string[][] technInsp = SqlOperations.GetTechnicianInspector(30);
            comboBoxInspect.Items.AddRange(technInsp[0]);
            comboBoxTechn.Items.AddRange(technInsp[1]);
        }

        private void checkIfOk()
        {
            if (comboBoxInspect.Text != "" & comboBoxTechn.Text != "" & checkBoxInspector.Checked & checkBoxTechncian.Checked)
            {
                button1.Text = "OK";
            }
            else
            {
                button1.Text = "UZUPEŁNIJ DANE";
            }
        }

        private void comboBoxTechn_TextChanged(object sender, EventArgs e)
        {
            checkIfOk();
        }

        private void comboBoxInspect_TextChanged(object sender, EventArgs e)
        {
            checkIfOk();
        }

        private void checkBoxTechncian_CheckedChanged_1(object sender, EventArgs e)
        {
            checkIfOk();
        }
    }
}
