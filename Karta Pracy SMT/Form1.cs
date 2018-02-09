using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public partial class MainForm : Form
    {
        Dictionary<string, EfficiencyStructure> efficiencNormyPerModel = new Dictionary<string, EfficiencyStructure>(); 

        public MainForm()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLotForm fmNewLot = new NewLotForm(this, dataGridView1, miraeCurrentProgram);
            fmNewLot.ShowInTaskbar = false;
            fmNewLot.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 & e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridView1.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnButtonLed")
                    {
                        
                        LedLeftovers clickedLeftovers = (LedLeftovers)cell.Tag;

                        Add_LED_leftovers editLeftovers = new Add_LED_leftovers(dataGridView1, cell);
                        editLeftovers.ShowInTaskbar = false;
                        editLeftovers.ShowDialog();
                    }
                    
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnQualityCheck")
                    {
                        //DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        ChangeOverConfirmationCard changeOverForm = new ChangeOverConfirmationCard(cell);
                        changeOverForm.ShowDialog();
                    }
                }
            }
        }


        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            //Pen p = new Pen(Color.Red, 3);
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (cell.GetType().ToString() == "System.Windows.Forms.DataGridViewButtonCell")
            //        {
            //            if (cell.Value.ToString() == "BRAK")
            //            {
            //                Rectangle rec = dataGridView1.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true);
            //                e.Graphics.DrawRectangle(p, rec);
            //            }
            //        }
            //    }
            //}
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.GetType().ToString() == "System.Windows.Forms.DataGridViewButtonCell") 
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All
                    & ~(DataGridViewPaintParts.ContentForeground));
                var r = e.CellBounds;
                r.Inflate(-4, -4);
                SolidBrush brush = new SolidBrush(cell.Style.BackColor);

                e.Graphics.FillRectangle(brush, r);
                e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }
        DateTime miraeFileLatModificationDate;
        public string miraeCurrentProgram = ""; 
        private void timerMiraeStalker_Tick(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["MiraePath"];
            if (System.IO.File.Exists(filePath))
            {
                DateTime lastModifiedCurrentRading = System.IO.File.GetLastWriteTime(filePath);
                if (lastModifiedCurrentRading!= miraeFileLatModificationDate)
                {
                    miraeCurrentProgram = Tools.GetCurrenMiraeProgram();
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int rowIndex = e.RowIndex;
                DataGridViewCell ngCell = dataGridView1.Rows[rowIndex].Cells["Ng"];
                DataGridViewCell scrapCell = dataGridView1.Rows[rowIndex].Cells["Scrap"];

                if (ngCell.Value != null & scrapCell.Value != null)
                {

                    double ngValue = -1;
                    double scrapValue = -1;


                    if (double.TryParse(ngCell.Value.ToString(), out ngValue) & double.TryParse(scrapCell.Value.ToString(), out scrapValue))
                    {

                        if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "NG" || dataGridView1.Columns[e.ColumnIndex].HeaderText == "Scrap")
                        {
                            double orderedQty = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["ColumnQty"].Value.ToString());
                            double goodQty = orderedQty - ngValue - scrapValue;
                            dataGridView1.Rows[e.RowIndex].Cells["goodQty"].Value = goodQty;
                        }
                    }
                }
            }
        }
    }
}
