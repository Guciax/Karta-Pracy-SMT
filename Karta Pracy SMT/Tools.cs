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
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (!dgv.Rows[i].Displayed)
                {
                    bool saved = Convert.ToBoolean(dgv.Rows[i].Cells[0].Value);
                    if (saved)
                    {
                        dgv.Rows.RemoveAt(i);
                        break;
                    }
                }
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
    }
}
