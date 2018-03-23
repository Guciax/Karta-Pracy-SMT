using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT.Data_Structures
{
    public class EfficiencyNormsPerModel

    {
        public EfficiencyNormsPerModel(Int32 quantityPerHour,int  qtyRankA, int qtyRankB)
        {
            QuantityPerHour = quantityPerHour;
            QtyRankA = qtyRankA;
            QtyRankB = qtyRankB;
        }

        public int QuantityPerHour { get; }
        public int QtyRankA { get; }
        public int QtyRankB { get; }
    }
}
