using Karta_Pracy_SMT.Data_Structures;
using Karta_Pracy_SMT.Efficiency;
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
        public CurrentMstOrder currentMstOrderData = new CurrentMstOrder("", "", 0, 0, DateTime.Now, "", "", DateTime.Now, 0, 0, 0, 0, new List<ledReelData>(),"",0, 0);
        private readonly string smtLine;

        public MstOrder(string smtLine)
        {
            InitializeComponent();
            this.smtLine = smtLine;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void MstOrder_Load(object sender, EventArgs e)
        {
            comboBoxOperator.Items.AddRange(SqlOperations.GetOperatorsArray(30));
            this.ActiveControl = textBoxOrderNumber;
        }

        private void CheckInputData()
        {
            bool result = true;

            if (comboBoxOperator.Text.Trim() == "") result = false;
            if (currentMstOrderData.OrderNumber == "") result = false;
            if (currentMstOrderData.Stencil == "") result = false;
            if (currentMstOrderData.Nc10 == "") result = false;

            if (result)
                buttonOK.Text = "OK";
            else
                buttonOK.Text = "Uzupełnij dane";
        }

        private void textBoxOrderNumber_TextChanged(object sender, EventArgs e)
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

        }
        private void textBoxPcbOnMb_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CheckInputData();
            if (buttonOK.Text== "OK")
            {
                currentMstOrderData.Oper = comboBoxOperator.Text;
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
            labelModelInfo.Text = "";
            //DataTable lotTable = MST.MES.SqlOperations.Kitting.GetKittingTableForLots(new string[] { textBoxOrderNumber.Text });
            var kittingInfo = MST.MES.SqlDataReaderMethods.Kitting.GetKittingDataForOrders(new string[] { textBoxOrderNumber.Text });
            if (kittingInfo.Count > 0) 
            {
                var orderInfo = kittingInfo[textBoxOrderNumber.Text];
                currentMstOrderData.OrderNumber = textBoxOrderNumber.Text;
                string modelId = orderInfo.modelId;
                currentMstOrderData.Nc10 = modelId;
                currentMstOrderData.ModelName = orderInfo.ModelName; ;
                DataTable modelInfoTable = MST.MES.SqlOperations.MesModels.GetMstModelInfo(modelId);

                var effPerHour = EfficiencyCalculation.CalculateModelNormPerHour(modelId, smtLine);

                currentMstOrderData.OrderedQty = (int)orderInfo.orderedQty;
                currentMstOrderData.OrderNumber = textBoxOrderNumber.Text;
                currentMstOrderData.PcbOnMb = TryParseNullableCell(modelInfoTable.Rows[0]["SMT_Carrier_QTY"].ToString());
                currentMstOrderData.ResQty = TryParseNullableCell(modelInfoTable.Rows[0]["Resistor_Qty"].ToString());
                currentMstOrderData.ConnQty = TryParseNullableCell(modelInfoTable.Rows[0]["Conn_Qty"].ToString());
                currentMstOrderData.LedQty = TryParseNullableCell(modelInfoTable.Rows[0]["PKG_SUM_QTY"].ToString());
                currentMstOrderData.BinQty = (int)orderInfo.numberOfBins;
                currentMstOrderData.NormPerHour = Math.Round(effPerHour.outputPerHour, 0);
                textBoxOrderNumber.BackColor = Color.Lime;
                labelModelInfo.Text += currentMstOrderData.Nc10.Insert(4, " ").Insert(8, " ") + Environment.NewLine +
                    currentMstOrderData.ModelName + Environment.NewLine +
                    "Ilość zlecona: " + currentMstOrderData.OrderedQty;

                var previousSmtRecords = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder(textBoxOrderNumber.Text);
                if (previousSmtRecords.totalManufacturedQty > 0)
                {
                    labelPreviousSmtInfo.Text = "Kontynuacja zlecenia." + Environment.NewLine +
                        $"Do tej pory wykonano: {previousSmtRecords.totalManufacturedQty} szt." + Environment.NewLine +
                        $"Pozostało do wykonania: {currentMstOrderData.OrderedQty - previousSmtRecords.totalManufacturedQty} szt.";
                    currentMstOrderData.PreviouslyManufacturedQty = previousSmtRecords.totalManufacturedQty;
                }
                else
                {
                    labelPreviousSmtInfo.Text = "";
                }

                if (currentMstOrderData.Nc10.Contains("-"))
                {
                    labelPreviousSmtInfo.Text = "To jest zlecenie LG, zmień zakładkę na LGIT!";
                }
            }
            else
            {
                labelModelInfo.Text = "Nieprawidłowy numer zlecenia - Brak zlecenia w bazie Kitting!";
            }


            //if (MST.MES.SqlOperations.SMT.SmtLots(new string[] { textBoxOrderNumber.Text }).Rows.Count > 0) 
            //{
            //    labelModelInfo.Text = "Ten numer zlecenia nie istnieje w bazie SMT";
            //}
            //if (lotTable.Rows.Count > 0)
            //{
            //    currentMstOrderData.OrderNumber = textBoxOrderNumber.Text;
            //    string modelId = lotTable.Rows[0]["NC12_wyrobu"].ToString();
            //    currentMstOrderData.Nc10 = modelId;
            //    currentMstOrderData.ModelName = MST.MES.SqlOperations.ConnectDB.NC12ToModelName(modelId + "00");
            //    DataTable modelInfoTable = MST.MES.SqlOperations.MesModels.GetMstModelInfo(modelId);

            //    var effPerHour = EfficiencyCalculation.CalculateModelNormPerHour(modelId, smtLine);

            //    currentMstOrderData.OrderedQty = TryParseNullableCell(lotTable.Rows[0]["Ilosc_wyrobu_zlecona"].ToString());
            //    currentMstOrderData.OrderNumber = textBoxOrderNumber.Text;
            //    currentMstOrderData.PcbOnMb = TryParseNullableCell(modelInfoTable.Rows[0]["SMT_Carrier_QTY"].ToString());
            //    currentMstOrderData.ResQty = TryParseNullableCell(modelInfoTable.Rows[0]["Resistor_Qty"].ToString());
            //    currentMstOrderData.ConnQty = TryParseNullableCell(modelInfoTable.Rows[0]["Conn_Qty"].ToString());
            //    currentMstOrderData.LedQty = TryParseNullableCell(modelInfoTable.Rows[0]["PKG_SUM_QTY"].ToString());
            //    currentMstOrderData.BinQty = TryParseNullableCell(lotTable.Rows[0]["IloscKIT"].ToString());
            //    currentMstOrderData.NormPerHour = Math.Round(effPerHour.outputPerHour, 0);
            //    textBoxOrderNumber.BackColor = Color.Lime;
            //    labelModelInfo.Text += currentMstOrderData.Nc10.Insert(4, " ").Insert(8, " ") + Environment.NewLine +
            //        currentMstOrderData.ModelName + Environment.NewLine +
            //        "Ilość zlecona: " + currentMstOrderData.OrderedQty;

            //    var previousSmtRecords = MST.MES.SqlDataReaderMethods.SMT.GetOneOrder(textBoxOrderNumber.Text);
            //    if (previousSmtRecords.totalManufacturedQty > 0)
            //    {
            //        labelPreviousSmtInfo.Text = "Kontynuacja zlecenia." + Environment.NewLine +
            //            $"Do tej pory wykonano: {previousSmtRecords.totalManufacturedQty} szt." + Environment.NewLine +
            //            $"Pozostało do wykonania: {currentMstOrderData.OrderedQty - previousSmtRecords.totalManufacturedQty} szt.";
            //        currentMstOrderData.PreviouslyManufacturedQty = previousSmtRecords.totalManufacturedQty;
            //    }
            //    else
            //    {
            //        labelPreviousSmtInfo.Text = "";
            //    }

            //    if (currentMstOrderData.Nc10.Contains("-"))
            //    {
            //        labelPreviousSmtInfo.Text = "To jest zlecenie LG, zmień zakładkę na LGIT!";
            //    }
            //}
            //else
            //{
            //    labelModelInfo.Text = "Nieprawidłowy numer zlecenia - Brak zlecenia w bazie Kitting!";
            //}
        }

        private int TryParseNullableCell(object dataCell)
        {
            int result = 0;
            if (int.TryParse(dataCell.ToString(), out  result)) return result;
            return 0;

        }

        private void comboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CheckInputData();
        }

        private void textBoxStencil_TextChanged(object sender, EventArgs e)
        {
            currentMstOrderData.Stencil = textBoxStencil.Text;
            CheckInputData();
            textBoxStencil.BackColor = Color.Lime;
        }




    }
}
