using Karta_Pracy_SMT.Data_Structures;
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
    public partial class MstOrder : Form
    {
        internal CurrentMstOrder currentMstOrderData;

        public MstOrder()
        {
            InitializeComponent();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void MstOrder_Load(object sender, EventArgs e)
        {
            comboBoxOperator.Items.AddRange(SqlOperations.GetOperatorsArray(30));
        }

        private void CheckInputData()
        {
            bool result = true;

            if (comboBoxOperator.Text.Trim() == "") result = false;
            if (textBoxOrderNumber.Text.Trim()== "") result = false;
            if (textBox12NC.Text.Trim() =="") result = false;
            if (textBoxQuantity.Text.Trim() == "") result = false;

            if (result)
                buttonOK.Text = "OK";
            else
                buttonOK.Text = "Uzupełnij dane";
        }

        private void textBoxOrderNumber_TextChanged(object sender, EventArgs e)
        {
            CheckInputData();
        }

        private void comboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckInputData();
        }

        private void textBoxStencil_TextChanged(object sender, EventArgs e)
        {
            CheckInputData();
        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            CheckInputData();
        }

        private void dataGridViewRankA_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CheckInputData();
        }

        private void dataGridViewRankB_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CheckInputData();
        }
        private void textBoxQuantity_TextChanged_1(object sender, EventArgs e)
        {
            CheckInputData();
        }
        private void textBoxPcbOnMb_TextChanged(object sender, EventArgs e)
        {
            CheckInputData();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CheckInputData();
            if (buttonOK.Text== "OK")
            {
                int pcbOnMb = int.Parse(textBoxPcbOnMb.Text);
                currentMstOrderData = new CurrentMstOrder(textBoxOrderNumber.Text, comboBoxOperator.Text, int.Parse(textBoxQuantity.Text), 0, DateTime.Now, textBox12NC.Text.Trim().Replace(" ",""), textBox12NC.Text, DateTime.Now, pcbOnMb, new List<string>());

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                
            }
        }

        private void comboBoxOperator_Leave_1(object sender, EventArgs e)
        {
            if (comboBoxOperator.Text.Trim() != "")
            {
                comboBoxOperator.BackColor = Color.Lime;
            }
            else
            {
                comboBoxOperator.BackColor = Color.Red;
            }
        }

        private void textBoxOrderNumber_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text.Trim() != "")
            {
                tb.BackColor = Color.Lime;
            }
            else
            {
                tb.BackColor = Color.Red;
            }
        }

        private void textBoxQuantity_Leave(object sender, EventArgs e)
        {
            int value = 0;
            TextBox tbx = (TextBox)sender;
            if (Int32.TryParse(tbx.Text, out value))
            {
                if (value > 0)
                {
                    tbx.BackColor = Color.Lime;
                }
                else
                {
                    tbx.BackColor = Color.Red;
                    tbx.Text = "";
                }
            }
            else
            {
                tbx.BackColor = Color.Red;
                tbx.Text = "";
            }
        }


    }
}
