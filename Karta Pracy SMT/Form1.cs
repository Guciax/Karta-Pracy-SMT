using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public MainForm()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewLotForm fmNewLot = new NewLotForm(this, dataGridView1);
            fmNewLot.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 & e.RowIndex > 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].CellType.ToString() == "System.Windows.Forms.DataGridViewButtonCell")
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnButtonLed")
                    {
                        DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        LedLeftovers clickedLeftovers = (LedLeftovers)cell.Tag;

                        Add_LED_leftovers editLeftovers = new Add_LED_leftovers(dataGridView1, cell);
                        editLeftovers.Show();
                    }
                    
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnQualityCheck")
                    {
                        DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        ChangeOverConfirmationCard changeOverForm = new ChangeOverConfirmationCard(cell);
                        changeOverForm.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //int val = int.Parse(btn.Text);

            SendKeys.Send(btn.Text);
        }
    }
}
