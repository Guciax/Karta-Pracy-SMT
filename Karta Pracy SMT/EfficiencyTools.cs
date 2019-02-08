using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public class EfficiencyTools
    {
        public static Dictionary<string, EfficiencyNormsPerModel> CreateEfficiencyNorm()
        {
            Dictionary<string, EfficiencyNormsPerModel> result = new Dictionary<string, EfficiencyNormsPerModel>();
            DataTable mesModels = SqlOperations.GetMesModels();
            //MODEL_ID,A_PKG_QTY,B_PKG_QTY,CCT_CODE,SMT_Carrier_QTY
            foreach (DataRow row in mesModels.Rows)
            {
                string model = row["MODEL_ID"].ToString().Replace("LLFML", "");
                if (model== "G2-08K201A")
                {
                    ;
                }
                string modelType = model.Split('-')[0]; //K2, K1, G2, G1....
                string modelFamily = model.Substring(0, 6) + "XXX" + model.Substring(9, 1);

                //if (result.ContainsKey(modelFamily)) continue;
                if(row["A_PKG_QTY"].ToString().Trim()=="" )continue;
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

                if (modelType == "K2" || modelType == "G2")
                {
                    if (!IsOdd(CCT))
                    {
                        connQty = 4;
                    }
                }

                int length = 600;
                switch (modelType)
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

                EfficiencyNormsPerModel item = new EfficiencyNormsPerModel(0, rankAQty, rankBQty);

               if (!result.ContainsKey(model))
                {
                    result.Add(model, item);
                }
               if (!result.ContainsKey(modelFamily))
                {
                    result.Add(modelFamily, item);
                }
            }
            return result;
        }

        public static Dictionary<string, EfficiencyStructure> CreateEfficiency()
        {
            Dictionary<string, EfficiencyStructure> result = new Dictionary<string, EfficiencyStructure>();
            DataTable mesModels = SqlOperations.GetMesModels();
            //MODEL_ID,A_PKG_QTY,B_PKG_QTY,CCT_CODE,SMT_Carrier_QTY

            foreach (DataRow row in mesModels.Rows)
            {
                string model = row["MODEL_ID"].ToString().Replace("LLFML", "");
                string modelType = model.Split('-')[0]; //K2, K1, G2, G1....
                string modelFamily = model.Substring(0, 6) + "XXX" + model.Substring(9, 1);

                if (result.ContainsKey(modelFamily)) continue;

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
                if (modelType=="K2" || modelType=="G2")
                {
                    if (!IsOdd(CCT))
                    {
                        connQty = 4;
                    }
                }

                int length = 600;
                switch (modelType)
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

                //EfficiencyStructure newItem = new EfficiencyStructure()
                   // result.Add(modelFamily, )
                
            }
            return result;
        }

        private  static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        public static Dictionary<string, double> EfficiencyPerLot(DataTable lotTable)
        {
            // DataCzasStart,DataCzasKoniec,LiniaSMT,OperatorSMT,NrZlecenia,Model,IloscWykonana,NGIlosc,ScrapIlosc,Kontrola1szt,KoncowkiLED
            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (DataRow row in lotTable.Rows)
            {
                DateTime startDate = DateTime.Parse(row["DataCzasStart"].ToString());
                DateTime endDate = DateTime.Parse(row["DataCzasKoniec"].ToString());
                Int32 lotDurationMinutes = (startDate - endDate).Minutes;

                string model = row["Model"].ToString();
                string modelFamily = model.Substring(0, 6) + "XXX" + model.Substring(9, 1);

                Int32 lotQty = Int32.Parse(row["IloscWykonana"].ToString());

                
            }
            return result;
            
        }



        ///<summary>
        ///<para>double[diodeWaste, moduleWaste]</para>
        ///</summary>
        public static double[] CalculateLedDiodeWasteLevel(DataGridView grid, Dictionary<string, EfficiencyNormsPerModel> modelNorm)
        {
            List<double> percentageLotWaste = new List<double>();
            List<string> lotsList = new List<string>();
            double moduleWasteSum = 0;
            double goodModuleSum = 0;


            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["ColumnLot"].Value == null) continue;
                lotsList.Add(row.Cells["ColumnLot"].Value.ToString().Trim());
            }
            //Dictionary<string, string[]> lotToRankABQty = SqlOperations.lotToRankABQty(lotsList.ToArray());
            Dictionary<string, string[]> lotToRankABQty = new Dictionary<string, string[]>();

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["Ng"].Value == null) continue;
                string lotNo = row.Cells["ColumnLot"].Value.ToString().Trim();
                //string ledLeftRaw = row.Cells["ColumnButtonLed"].Value.ToString().Trim();
                string model = row.Cells["ColumnModel"].Value.ToString().Trim();
                if (!modelNorm.ContainsKey(model))
                {
                    continue;
                }

                LedLeftovers ledLeft = (LedLeftovers)row.Cells["ColumnButtonLed"].Tag;


                double goodQty = Int32.Parse(row.Cells["GoodQty"].Value.ToString().Trim());
                double ngQty = Int32.Parse(row.Cells["Ng"].Value.ToString().Trim());
                double allQty = goodQty + ngQty;

                moduleWasteSum += ngQty;
                goodModuleSum += goodQty;

                //if (row.Cells["ColumnLot"].Value.ToString() == "1327848")
                //    Debug.WriteLine("tada");

                model = model.Substring(0, 6) + "XXX" + model.Substring(9, 1);
                double rankAQty = modelNorm[model].QtyRankA;
                double rankBQty = modelNorm[model].QtyRankB;
                double ledQty = rankAQty + rankBQty;
                double ledPerLotNorm = allQty * ledQty;

                double ledPerReel = 3000;
                if (row.Cells["Rank12NC"].Value.ToString().Length < 11) ledPerReel = 3500;
                double ledPerLot = ledLeft.RankA.Count * 2 * ledPerReel;
                double expectedLedLeft = ledPerLot - ledPerLotNorm;
                if (expectedLedLeft < 0) continue;

                double realLedLeft = 0;
                foreach (var led in ledLeft.RankA)
                {
                    realLedLeft += led.QtyLeft;
                }
                foreach (var led in ledLeft.RankB)
                {
                    realLedLeft += led.QtyLeft;
                }
                double wasteDifference = expectedLedLeft - realLedLeft;

                percentageLotWaste.Add(((expectedLedLeft-realLedLeft)/ledPerLot)*100);
            }

            if (goodModuleSum > 0)
            {
                return new double[] { Math.Round(percentageLotWaste.Average(), 2), Math.Round(moduleWasteSum / goodModuleSum, 4) * 100 };
            }
            else
            {
                return new double[] { 0, 0 };
            }
        }

        public struct lotPiecesQuantity
        {
            public Int32 lots;
            public Int32 modules;
            public DataTable tagAllLots;
        }

        ///<summary>
        ///<para>dictionary[day][shiftNo][lotsQty,modulesQty]</para>
        ///</summary>
        public static Dictionary<DateTime, Dictionary<int,lotPiecesQuantity>> quantityPerDayPerShift(DataTable sqlTable)
        {
            //DataCzasKoniec,LiniaSMT,Model,IloscWykonana
            Dictionary<DateTime, Dictionary<int, lotPiecesQuantity>> result = new Dictionary<DateTime, Dictionary<int, lotPiecesQuantity>>();
            //Dictionary<DateTime, Dictionary<int, DataTable>> tagTables = new Dictionary<DateTime, Dictionary<int, DataTable>>();


            //Tools.dateShiftNo startingDateShift = Tools.whatDayShiftIsit(DateTime.Parse(sqlTable.Rows[0]["DataCzasKoniec"].ToString()));

            foreach (DataRow row in sqlTable.Rows)
            {
                Tools.dateShiftNo dateShift = Tools.whatDayShiftIsit(DateTime.Parse(row["DataCzasKoniec"].ToString()));
                //Debug.WriteLine(row["DataCzasKoniec"].ToString() + " - " + dateShift.date.ToShortTimeString() + " - " + dateShift.shift);
                //if (dateShift.date == startingDateShift.date & dateShift.shift == startingDateShift.shift) continue;

                Int32 modulesQty = 0;
                if (!Int32.TryParse(row["IloscWykonana"].ToString(), out modulesQty)) continue;

                if (!result.ContainsKey(dateShift.date.Date))
                {
                    result.Add(dateShift.date.Date, new Dictionary<int, lotPiecesQuantity>());
                    //tagTables.Add(dateShift.date.Date, new Dictionary<int, DataTable>());
                }

                if (!result[dateShift.date.Date].ContainsKey(dateShift.shift))
                {
                    result[dateShift.date.Date].Add(dateShift.shift, new lotPiecesQuantity());
                    //tagTables[dateShift.date.Date].Add(dateShift.shift, new DataTable());
                }

                lotPiecesQuantity qty = new lotPiecesQuantity();
                qty.lots = result[dateShift.date.Date][dateShift.shift].lots + 1;
                qty.modules = result[dateShift.date.Date][dateShift.shift].modules + modulesQty;

                if (result[dateShift.date.Date][dateShift.shift].tagAllLots == null)
                {
                    qty.tagAllLots = sqlTable.Clone();
                    
                }
                else
                {
                    qty.tagAllLots = result[dateShift.date.Date][dateShift.shift].tagAllLots.Clone();
                    foreach (DataRow r in result[dateShift.date.Date][dateShift.shift].tagAllLots.Rows)
                    {
                        qty.tagAllLots.Rows.Add(r.ItemArray);
                    }
                }
                //if (qty.tagAllLots == null)
                

                qty.tagAllLots.Rows.Add(row.ItemArray);

                result[dateShift.date.Date][dateShift.shift] = qty;

            }

            return result;
        }

        public static void QuantityDictionaryToGrid(DataGridView grid, Dictionary<DateTime, Dictionary<int, lotPiecesQuantity>> inputDic)
        {
            if (inputDic.Count > 0)
            {
                grid.Rows.Clear();
                grid.Columns.Clear();

                foreach (var dateKey in inputDic)
                {
                    grid.Columns.Add(dateKey.Key.ToString("dd-MM"), dateKey.Key.ToString("dd-MM"));
                }

                grid.Rows.Add(3);
                grid.Rows[0].HeaderCell.Value = "1";
                grid.Rows[1].HeaderCell.Value = "2";
                grid.Rows[2].HeaderCell.Value = "3";

                Color firstShiftColor = Tools.GetShiftColor(new DateTime(2018, 05, 29, 08, 00, 00));
                Color secondShiftColor = Tools.GetShiftColor(new DateTime(2018, 05, 29, 15, 00, 00));
                Color thirdShiftColor = Tools.GetShiftColor(new DateTime(2018, 05, 29, 23, 00, 00));

                grid.Rows[0].DefaultCellStyle.BackColor = firstShiftColor;
                grid.Rows[1].DefaultCellStyle.BackColor = secondShiftColor;
                grid.Rows[2].DefaultCellStyle.BackColor = thirdShiftColor;

                foreach (var dateKey in inputDic)
                {
                    foreach (var shiftKey in dateKey.Value)
                    {
                        grid.Rows[shiftKey.Key - 1].Cells[dateKey.Key.ToString("dd-MM")].Value = shiftKey.Value.lots.ToString() + " / " + shiftKey.Value.modules;
                        grid.Rows[shiftKey.Key - 1].Cells[dateKey.Key.ToString("dd-MM")].Tag = inputDic[dateKey.Key.Date][shiftKey.Key].tagAllLots;
                    }

                }
            }
        }

        public static DateTime ParseExact(string stringDate)
        {
            return DateTime.ParseExact(stringDate, "HH:mm:ss dd-MM-yyyy", CultureInfo.InvariantCulture);

        }



        public static int HowManyLotsThisShift(DataGridView grid)
        {
            int lotsCounter = 0;
            DateTime shiftStart = Tools.shiftStartTime();

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["EndDate"].Value == null) continue;

                    DateTime lotEndTime = ParseExact(row.Cells["EndDate"].Value.ToString());
                    if (lotEndTime > shiftStart)
                    {
                        lotsCounter++;
                    }
                
            }

            return lotsCounter;
        }
    }
}
