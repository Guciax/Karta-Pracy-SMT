using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT.Data_Structures
{
    public class EfficiencyStructure
    {
        public EfficiencyStructure(DateTime startDate, DateTime ednDate, double elapsedMinutes, string model, string lotNo, double allQty, double ngQty)
        {
            StartDate = startDate;
            EdnDate = ednDate;
            ElapsedMinutes = elapsedMinutes;
            Model = model;
            LotNo = lotNo;
            AllQty = allQty;
            NgQty = ngQty;
        }

        public DateTime StartDate { get; }
        public DateTime EdnDate { get; }
        public double ElapsedMinutes { get; }
        public string Model { get; }
        public string LotNo { get; }
        public double AllQty { get; }
        public double NgQty { get; }
    }
}
