using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    class LotData
    {
        public LotData(string model, int orderedQty, string rankA, string rankB)
        {
            Model = model;
            OrderedQty = orderedQty;
            RankA = rankA;
            RankB = rankB;
        }

        public string Model { get; }
        public int OrderedQty { get; }
        public string RankA { get; }
        public string RankB { get; }
    }
}
