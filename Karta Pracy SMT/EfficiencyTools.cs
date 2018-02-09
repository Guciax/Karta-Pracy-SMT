using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    public class EfficiencyTools
    {
        public static Dictionary<string, EfficiencyStructure> CreateEfficiencyNorm()
        {
            Dictionary<string, EfficiencyStructure> result = new Dictionary<string, EfficiencyStructure>();
            DataTable mesModels = SqlOperations.GetMesModels();
            //MODEL_ID,A_PKG_QTY,B_PKG_QTY,CCT_CODE,SMT_Carrier_QTY
            foreach (DataRow row in mesModels.Rows)
            {
                string model = row["MODEL_ID"].ToString().Replace("LLFML", "");
                string modelFamily = model.Split('-')[0];
                int rankAQty = int.Parse(row["A_PKG_QTY"].ToString());
                int rankBQty = int.Parse(row["B_PKG_QTY"].ToString());
                int CCT = int.Parse(row["CCT_CODE"].ToString());
                int pcbPerCarrier = int.Parse(row["SMT_Carrier_QTY"].ToString());
                if (pcbPerCarrier < 5)  //less than 5 is square (given carrier for testing purposes)
                {
                    pcbPerCarrier = 1;
                }

                int ledQty = Math.Max(rankAQty, rankBQty) * 2;
                int connQty = 2;
                if (modelFamily=="K2" || modelFamily=="G2")
                {
                    if (!IsOdd(CCT))
                    {
                        connQty = 4;
                    }
                }

                int length = 600;
                switch (modelFamily)
                {
                    case "22":
                        length = 250;
                        break;
                    case "33":
                        length = 270;
                        break;
                    case "32":
                        length = 272;
                        break;
                    case "53":
                        length = 540;
                        break;
                }
            }
            return result;
        }

        private  static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
