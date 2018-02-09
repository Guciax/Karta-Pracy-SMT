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
    }
}
