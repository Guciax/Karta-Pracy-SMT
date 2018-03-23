using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Karta_Pracy_SMT
{
    class Tools
    {
        public static void AutoSizeColumnsWidth(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public static string GetCurrenMiraeProgram()
        {
            string filePath =  ConfigurationManager.AppSettings["MiraePath"];
            string[] fileLibes = System.IO.File.ReadAllLines(filePath);
            string latestProgramPath = fileLibes[0];

            if(latestProgramPath.Contains("\\"))
            {
                return latestProgramPath.Split('\\')[latestProgramPath.Split('\\').Length - 1].Split('.')[0].Replace("LLFML","");
            }
            else
            {
               return latestProgramPath.Split('.')[0].Replace("LLFML", "");
            }

        }

        public static string GetNumberOfConnectors(string model)
        {
            //exceptions....
            if (model.Replace("LLFML", "") == "G2-08L404B")
                return "0";

            string family = model.Split('-')[0].Replace("LLFML", "").Substring(0, 1).ToUpper();
            string connCode = model.Split('-')[1].Substring(5, 1);
            if ((family == "K" || family == "G") & (connCode == "2" || connCode == "4"))
            {
                return "4";
            }
            else
            {
                return "2";
            }
        }

        public static void CleanUpDgv(DataGridView dgv)
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

        public static string getCellValue(DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return cell.Value.ToString();
            }
            return "";
        }

        public struct dateShiftNo
        {
            public DateTime date;
            public int shift;
        }

        ///<summary>
        ///<para>returns shift number and shift start date and time</para>
        ///</summary>
        public static dateShiftNo whatDayShiftIsit(DateTime inputDate)
        {
            int hourNow = inputDate.Hour;
            DateTime resultDate = new DateTime();
            int resultShift = 0;

            if (hourNow < 6)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day-1, 22, 0, 0);
                resultShift = 3;
            }

            else if (hourNow < 14)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 6, 0, 0);
                resultShift = 1;
            }

            else if (hourNow < 22)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 14, 0, 0);
                resultShift = 2;
            }

            else
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 22, 0, 0);
                resultShift = 3;
            }

            dateShiftNo result = new dateShiftNo();
            result.date = resultDate;
            result.shift = resultShift;
            return result;
        }

        public static DateTime shiftStartTime()
        {
            DateTime shiftStart = new DateTime();
            int hourNow = DateTime.Now.Hour; 

            if (hourNow < 6)
            {
                shiftStart = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day - 1, 22, 0, 0);
            }
            else if (hourNow < 14)
            {
                shiftStart = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 6, 0, 0);
            }
            else if (hourNow < 22)
            {
                shiftStart = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 14, 0, 0);
            }
            else
            {
                shiftStart = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 22, 0, 0);
            }

            return shiftStart;
        }
    }
}
