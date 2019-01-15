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
    public partial class FinishMstOrder : Form
    {
        private readonly CurrentMstOrder currentOrder;
        private  string smtLine;
        int finalQty = 0;

        public FinishMstOrder(ref CurrentMstOrder currentOrder, string smtLine)
        {
            InitializeComponent();
            this.currentOrder = currentOrder;
            this.smtLine = smtLine;
        }

        private void FinishMstOrder_Load(object sender, EventArgs e)
        {
            labelOrderNo.Text += currentOrder.OrderNumber;
            label10Nc.Text += currentOrder.Nc10;
            labelName.Text += currentOrder.ModelName;
            labelOrderedQty.Text += currentOrder.OrderedQty.ToString();
            numericUpDown1.Value = currentOrder.MadeQty / currentOrder.PcbOnMb;
            labelStartDate.Text += currentOrder.DateStart.ToString();

            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = new string[] { "Zużyta", "Do liczenia" };
            combo.HeaderText = "Stan rolki";

            dataGridView1.Columns.Add(combo);

            AddReelsToGrid();
            DgvTools.AutoSizeColumns(dataGridView1, DataGridViewAutoSizeColumnMode.AllCells);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            labelPcbQty.Text = "=PCB: " + (numericUpDown1.Value * currentOrder.PcbOnMb).ToString();
            currentOrder.MadeQty = (int)numericUpDown1.Value * currentOrder.PcbOnMb;
        }

        private void AddReelsToGrid()
        {
            foreach (var reel in currentOrder.LedReels)
            {
                dataGridView1.Rows.Add(reel.Rank, reel.NC12, reel.ID, reel.Ilosc, reel.RemovedToTrash ? "Zużyta" : "Do liczenia");
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
