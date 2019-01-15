using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    class DgvTools
    {
        public static void PrepareDgvForBins(DataGridView grid, int binQty)
        {
            grid.Rows.Clear();
            Char binId = 'A';
            for (int b = 0; b < binQty; b++)
            {
                grid.Rows.Add(binId.ToString(), "BIN " + binId.ToString());
                foreach (DataGridViewCell cell in grid.Rows[grid.Rows.Count - 1].Cells)
                {
                    if (cell.Value != null)
                    {
                        if (cell.Value.ToString() == binId.ToString())
                        {
                            cell.Style.ForeColor = Color.DimGray;
                        }
                        else
                        {
                            cell.Style.ForeColor = Color.White;
                        }
                    }
                    cell.Style.BackColor = Color.DimGray;

                }
                binId++;
                grid.Rows.Add();
            }
            if (grid.Rows.Count > 0) grid.Rows.RemoveAt(grid.Rows.Count - 1);
        }

        public static void AddReelToGrid(string nc12, string id, DataGridView grid,ref CurrentMstOrder currentOrder)
        {
            DataTable reelTable = MST.MES.SqlOperations.SparingLedInfo.GetInfoFor12NC_ID(nc12, id);
            string qty = reelTable.Rows[0]["Ilosc"].ToString();
            string binId = reelTable.Rows[0]["Tara"].ToString();
            string zlecenieString = reelTable.Rows[0]["ZlecenieString"].ToString();

            if (currentOrder.OrderNumber == zlecenieString)
            {
                int binRow = 0;
                for (int r = 0; r < grid.Rows.Count; r++)
                {
                    if (grid.Rows[r].Cells[0].Value == null) continue;
                    if (grid.Rows[r].Cells[0].Value.ToString() == binId)
                    {
                        binRow = r;
                        break;
                    }
                }

                grid.Rows.Insert(binRow + 1, nc12, id, qty);
                ledReelData newReel = new ledReelData(nc12, double.Parse(qty), "", "", zlecenieString, id, binId);
                currentOrder.LedReels.Add(newReel);

                DgvTools.SumUpLedsInBins(grid);

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else
            {
                MessageBox.Show("Ta rolka LED przypisana jest zlecenia: "+zlecenieString);
            }
        }

        public static void AddReelToTrash(string nc12, string id, DataGridView grid,ref CurrentMstOrder currentOrder)
        {
            ledReelData reel = null;
            foreach (var ledReel in currentOrder.LedReels)
            {
                if (ledReel.NC12 == nc12 & ledReel.ID == id)
                {
                    reel = ledReel;
                }
            }

            if (reel != null)
            {
                int binRow = 0;
                for (int r = 0; r < grid.Rows.Count; r++)
                {
                    if (grid.Rows[r].Cells[0].Value == null) continue;
                    if (grid.Rows[r].Cells[0].Value.ToString() == reel.Rank)
                    {
                        binRow = r;
                        break;
                    }
                }

                grid.Rows.Insert(binRow + 1, nc12, id, reel.Ilosc);
                reel.RemovedToTrash = true;

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else
            {
                MessageBox.Show("Nieznana rolka LED");
            }

        }

        public static void PaintRowBackgroumd(DataGridViewRow row, Color backColor, Color frColor)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style.BackColor = backColor;
                cell.Style.ForeColor = frColor;
            }
        }

        public static void SumUpLedsInBins(DataGridView grid)
        {
            int lastBin = 0;
            double binSum = 0;
            for (int r = 0; r < grid.Rows.Count; r++)
            {
                if (grid.Rows[r].Cells[0].Value == null) continue;
                if (grid.Rows[r].Cells[0].Value.ToString().Length == 1)
                {
                    grid.Rows[lastBin].Cells[2].Value = binSum;
                    binSum = 0;
                    lastBin = r;
                    continue;
                }

                if (grid.Rows[r].Cells[2].Value == null) continue;
                binSum += double.Parse(grid.Rows[r].Cells[2].Value.ToString());

                if (r == grid.Rows.Count - 1)
                {
                    grid.Rows[lastBin].Cells[2].Value = binSum;
                }
            }
        }

        public static void AutoSizeColumns(DataGridView grid, DataGridViewAutoSizeColumnMode mode)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.AutoSizeMode = mode;
            }
        }

        internal static string TryGetCellValue(DataGridViewCell cell)
        {
            string result = null;
            if (cell.Value != null)
            {
                result = cell.Value.ToString();
            }
            return result;
        }

        internal static void MarkRemovedRow(string nc12, string id, DataGridView grid, Color backColor, Color foreColor)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (TryGetCellValue(row.Cells[0]) == nc12)
                {
                    if (TryGetCellValue(row.Cells[1]) == id)
                    {
                        PaintRowBackgroumd(row, backColor, foreColor);
                    }
                }
            }
        }

        public static void CleanUpLgiDgv(DataGridView dgv)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (!dgv.Rows[i].Displayed)
                {
                    bool saved = Convert.ToBoolean(dgv.Rows[i].Cells[0].Value);
                    if (saved)
                    {
                        rowsToRemove.Add(dgv.Rows[i]);
                    }
                }

                DataGridViewCheckBoxCell chbCell = (DataGridViewCheckBoxCell)dgv.Rows[i].Cells[0];
                if (Convert.ToBoolean(chbCell.Value))
                {
                    dgv.Rows[i].Cells[9].ReadOnly = true;
                    dgv.Rows[i].Cells[10].ReadOnly = true;
                }
            }

            foreach (var row in rowsToRemove)
            {
                dgv.Rows.Remove(row);
            }
        }

        public static void CleanUpMstDgv(DataGridView dgv)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (!dgv.Rows[i].Displayed)
                {
                    rowsToRemove.Add(dgv.Rows[i]);
                }
            }

            foreach (var row in rowsToRemove)
            {
                dgv.Rows.Remove(row);
            }
        }
    }
}
