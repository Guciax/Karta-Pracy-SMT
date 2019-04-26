using MST.MES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT.Efficiency
{
    public class EfficiencyCalculation
    {
        public static List<MST.MES.Data_structures.DevToolsModelStructure> dtDb = MST.MES.Data_structures.DevTools.DevToolsLoader.LoadDevToolsModels();
        public static Dictionary<string, ModelInfo.ModelSpecification> mesModels = MST.MES.SqlDataReaderMethods.MesModels.allModels();
        public static string smtLine = "";

        public static double CalculateEfficiency(DateTime startDate, DateTime endDate, string modelId, double qty, bool subtructChangeover)
        {
            double duration = (endDate - startDate).TotalHours;
            double changeoverTime = (double)25 / 60;
            if (subtructChangeover & duration> changeoverTime)
            {
                duration -= changeoverTime;
            }
            var modelEffNorm = CalculateModelNormPerHour(modelId, smtLine);
            double result = qty / duration /  modelEffNorm.outputPerHour;
            return result;
        }

        public static int CalculateStandardChangeOverTime(string model1, string model2)
        {
            ModelInfo.ModelSpecification modelSpec1;
            ModelInfo.ModelSpecification modelSpec2;
            if (!mesModels.TryGetValue(model1, out modelSpec1))
            {
                return -1;
            }
            if (!mesModels.TryGetValue(model2, out modelSpec2))
            {
                return -1;
            }

            var mbDimensionsLWmm1 = GetLgModelMbDimension(model1);
            var mbDimensionsLWmm2 = GetLgModelMbDimension(model2);

            if (modelSpec1.model12Nc[2] == '-')
            {
                //LG
                if (modelSpec1.model12Nc.Substring(0, 5) == modelSpec1.model12Nc.Substring(0, 5)) return 5;
                if (mbDimensionsLWmm1.Item1 == 350 &&  mbDimensionsLWmm2.Item1 == 350) return 20;
                if (mbDimensionsLWmm1.Item1 != 350 && mbDimensionsLWmm2.Item1 != 350) return 20;
                return 90;
            }
            else
            {
                //MST
                if (modelSpec1.ledCountPerModel != modelSpec2.ledCountPerModel) return 25;
                if (mbDimensionsLWmm1.Item2 != mbDimensionsLWmm2.Item2) return 25;
                if (mbDimensionsLWmm1.Item1 != mbDimensionsLWmm2.Item1) return 25;
                return 10;
            }
        }

        private class SmtLineConfiguration
        {
            public string lineName;
            public int pcbReflowSpeed;
            public int carrierReflowSpeed;
            public int reflowBoardSpacingTime;
            public int siplaceCph;
            public int printerCt;
            public int connCph;
            
        }

        public class OrderDataForEfficiencyStructure
        {
            public string modelId { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public double qty { get; set; }
            public int rowIndex { get; set; }

            public DateTools.dateShiftNo dateShiftOwner
            {
                get
                {
                    var startShiftInfo = DateTools.whatDayShiftIsit(start);
                    var endShiftInfo = DateTools.whatDayShiftIsit(start);
                    if (startShiftInfo == endShiftInfo) return DateTools.whatDayShiftIsit(start);

                    if ((start - startShiftInfo.fixedDate).TotalMinutes >= (endShiftInfo.fixedDate - end).TotalMinutes) return startShiftInfo;
                    return endShiftInfo;
                }
            }
        }

        public class SmtEfficiencyNormStruct
        {
            public double siplaceCT { get; set; }
            public double connCT { get; set; }
            public double reflowCT { get; set; }
            public double printerCT { get; set; }
            public double outputPerHour { get; set; }
            public ModelInfo.ModelSpecification modelSpec { get; set; }
            public Tuple<double, double> mbDimensionsLWmm { get; set; }
            public double lineCT
            {
                get
                {
                    return Math.Max(Math.Max(siplaceCT, connCT), Math.Max(reflowCT, printerCT));
                }
            }
        }

        public static SmtEfficiencyNormStruct CalculateModelNormPerHour(string modelId, string smtLine, int siplaceHeads2or4 = 2)
        {
            ModelInfo.ModelSpecification modelSpec;
            Tuple<double, double> mbDimensionsLWmm = new Tuple<double, double>(0, 0);
            if (!mesModels.TryGetValue(modelId, out modelSpec))
            {
                return null;
            }

            Dictionary<string, SmtLineConfiguration> lineConfiguration = new Dictionary<string, SmtLineConfiguration>
            { 
                    {"SMT1",new SmtLineConfiguration(){lineName="SMT1", pcbReflowSpeed=90, carrierReflowSpeed=90, reflowBoardSpacingTime=15 , siplaceCph=23000, printerCt = 30, connCph =  10000} },
                    {"SMT2",new SmtLineConfiguration(){lineName="SMT2", pcbReflowSpeed=130, carrierReflowSpeed=110, reflowBoardSpacingTime=15, siplaceCph=25000, printerCt = 35, connCph =  10000 } },
                    {"SMT3",new SmtLineConfiguration(){lineName="SMT3", pcbReflowSpeed=95, carrierReflowSpeed=90, reflowBoardSpacingTime=15, siplaceCph=25000, printerCt = 35, connCph =  10000 } },
                    {"SMT4",new SmtLineConfiguration(){lineName="SMT4", pcbReflowSpeed=130, carrierReflowSpeed=0, reflowBoardSpacingTime=15, siplaceCph=30000, printerCt = 45, connCph =  10000 } },
                    {"SMT5",new SmtLineConfiguration(){lineName="SMT5", pcbReflowSpeed=140, carrierReflowSpeed=105, reflowBoardSpacingTime=25, siplaceCph=25000, printerCt = 30, connCph =  3000 } },
                    {"SMT6",new SmtLineConfiguration(){lineName="SMT6", pcbReflowSpeed=140, carrierReflowSpeed=110, reflowBoardSpacingTime=25, siplaceCph=25000, printerCt = 30, connCph =  3000 } },
                    {"SMT7",new SmtLineConfiguration(){lineName="SMT7", pcbReflowSpeed=90, carrierReflowSpeed=90, siplaceCph=15000, reflowBoardSpacingTime=15, printerCt = 25, connCph =  1800 } },
                    {"SMT8",new SmtLineConfiguration(){lineName="SMT8", pcbReflowSpeed=90, carrierReflowSpeed=90, reflowBoardSpacingTime=15, siplaceCph=15000, printerCt = 25, connCph =  1800 } },
            };

            var dtModels = dtDb.Where(rec => rec.nc12 == modelId + "00");
            double reflowCT = 0;
            double siplaceCph = 0;
            double headMultiplier = siplaceHeads2or4 == 2 ? 1 : 1.8;
            double connCT = 0;
            int pcbLoadingUnloading = 15;

            if (dtModels.Count() > 0)
            {//MST
                mbDimensionsLWmm = GetMbDimensions(dtModels.First()); //need smth better than first
                reflowCT = (double)(mbDimensionsLWmm.Item1 / 10) / (double)lineConfiguration[smtLine].pcbReflowSpeed * 60 + lineConfiguration[smtLine].reflowBoardSpacingTime;
                siplaceCph = (50 * modelSpec.ledCountPerModel * modelSpec.pcbCountPerMB + 10000) * headMultiplier;
                connCT = modelSpec.pcbCountPerMB * modelSpec.connectorCountPerModel * (3600 / (double)lineConfiguration[smtLine].connCph) + pcbLoadingUnloading;
            }
            else
            {//LG
                mbDimensionsLWmm = GetLgModelMbDimension(modelId);
                var reflowCTspeed = (mbDimensionsLWmm.Item1 == 600)? lineConfiguration[smtLine].carrierReflowSpeed : lineConfiguration[smtLine].pcbReflowSpeed;
                reflowCT = (double)(mbDimensionsLWmm.Item1 / 10) / (double)reflowCTspeed * 60 + lineConfiguration[smtLine].reflowBoardSpacingTime;
                siplaceCph = ((50 * modelSpec.ledCountPerModel * modelSpec.pcbCountPerMB + 10000) * headMultiplier);
                double connQty = GetLgModelConnQty(modelId);
                connCT = modelSpec.pcbCountPerMB * connQty * (3600 / (double)lineConfiguration[smtLine].connCph) + pcbLoadingUnloading;
            }

            if (siplaceCph < 15000) siplaceCph = 15000;
            if (siplaceCph > lineConfiguration[smtLine].siplaceCph*headMultiplier) siplaceCph = lineConfiguration[smtLine].siplaceCph * headMultiplier;

            double siplaceCT = modelSpec.ledCountPerModel * modelSpec.pcbCountPerMB / (siplaceCph / 3600) + pcbLoadingUnloading;
            double lineCT = Math.Max(
                                    Math.Max(lineConfiguration[smtLine].printerCt, connCT),
                                    Math.Max(siplaceCT, reflowCT));

            return new SmtEfficiencyNormStruct()
            {
                connCT = Math.Ceiling(connCT),
                outputPerHour = Math.Ceiling(3600 / lineCT * modelSpec.pcbCountPerMB),
                reflowCT = Math.Ceiling(reflowCT),
                siplaceCT = Math.Ceiling(siplaceCT),
                printerCT = lineConfiguration[smtLine].printerCt,
                modelSpec = modelSpec,
                mbDimensionsLWmm = mbDimensionsLWmm
            };
        }

        private static double GetLgModelConnQty(string modelId)
        {
            if (modelId.StartsWith("K2") || modelId.StartsWith("G2"))
            {
                int connMark = int.Parse(modelId[8].ToString());
                if (connMark % 2 == 0) return 4;
            }
            return 2;
        }

        private static Tuple<double, double> GetLgModelMbDimension(string model)
        {

            if (model.StartsWith("33")) return new Tuple<double, double>(270, 270);
            if (model.StartsWith("22")) return new Tuple<double, double>(250, 250);
            if (model.StartsWith("32")) return new Tuple<double, double>(272, 230);
            if (model.StartsWith("53")) return new Tuple<double, double>(560, 270);
            return new Tuple<double, double>(600, 350);
        }

        private static Tuple<double, double> GetMbDimensions(MST.MES.Data_structures.DevToolsModelStructure dtModel00)
        {
            MST.MES.Data_structures.DevToolsModelStructure model = new MST.MES.Data_structures.DevToolsModelStructure();
            bool success = false;
            foreach (var subComponent in dtModel00.children)
            {
                if (subComponent.nc12.StartsWith("6010616")) //SMD Assy
                {
                    foreach (var component in subComponent.children)
                    {
                        if (component.name.StartsWith("PCB") & component.children.Count == 0)
                        {
                            model = component;
                            success = true;
                            break;
                        }
                        if (component.nc12.StartsWith("4010440"))
                        {
                            foreach (var pcb in component.children)
                            {
                                if (pcb.name.StartsWith("MB"))
                                {
                                    model = pcb;
                                    success = true;
                                    break;
                                }
                            }
                        }
                        if (success) break;
                    }
                }
                if (success) break;
            }

            Tuple<double, double> result = new Tuple<double, double>(0, 0);
            if (success)
            {
                try
                {
                    string[] size = model.atributes["L x W"].ToUpper().Split('X');
                    string l = size[0];
                    string w = size[1];
                    result = new Tuple<double, double>(double.Parse(l, CultureInfo.InvariantCulture), double.Parse(w, CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {

                    return new Tuple<double, double>(-1, -1);
                }
            }

            return result;
        }

        public static double WeightedAverage(List<Tuple<double, double>> valueWeigth)
        {
            var sumOfWeigth = valueWeigth.Select(v => v.Item2).Sum();
            var sumWeightedValues = valueWeigth.Select(v => v.Item1 * v.Item2).Sum();
            return sumWeightedValues / sumOfWeigth;
        }
    }
}
