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
    public partial class ShowShiftInfo : Form
    {
        private readonly DataTable source;
        private readonly double lotsNorm;

        public ShowShiftInfo(DataTable source, double lotsNorm)
        {
            InitializeComponent();
            this.source = source;
            this.lotsNorm = lotsNorm;
        }

        private void ShowShiftInfo_Load(object sender, EventArgs e)
        {
            dataGridViewDetails.DataSource = source;
            int rows = dataGridViewDetails.Rows.Count;

            label1.Text = "Wydajność: "+Math.Round( ((double)rows / lotsNorm), 3)  * 100 + " % ";


            foreach (DataGridViewColumn col in dataGridViewDetails.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; ;
            }

            Dictionary<string, double> qtyPerModel = new Dictionary<string, double>();
            
            double qtyTotal = 0;
            foreach (DataRow row in source.Rows)
            {
                string model = row["Model"].ToString();

                double qty = double.Parse(row["IloscWykonana"].ToString());

                if (!qtyPerModel.ContainsKey(model))
                {
                    qtyPerModel.Add(model, 0);
                }

                qtyPerModel[model] += qty;
                 qtyTotal+= qty;
            }
            qtyPerModel.Add("Razem", qtyTotal);
            dataGridViewSummary.Columns.Add("Model","Model");
            dataGridViewSummary.Columns.Add("IloscWykonana", "IloscWykonana");

            foreach (var keyEntry in qtyPerModel)
            {
                dataGridViewSummary.Rows.Add(keyEntry.Key, keyEntry.Value);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
