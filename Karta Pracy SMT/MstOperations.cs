using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    public class MstOperations
    {
        public static List<string> LedInfoTableToList(DataTable ledTable)
        {
            List<string> result = new List<string>();

            foreach (DataRow row in ledTable.Rows)//NC12,ID,Ilosc,ZlecenieString,Data_Czas,Tara
            {
                string line = $"BIN {row["Tara"].ToString()}: {row["NC12"].ToString().Insert(4, " ").Insert(8, " ")}";
                if (result.Contains(line)) continue;
                result.Add(line);
            }
            return result.OrderBy(x=>x).ToList();
        }
    }
}
