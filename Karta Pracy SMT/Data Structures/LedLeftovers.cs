using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    public class LedLeftovers
    {
        public LedLeftovers(List<RankStruc> rankA, List<RankStruc> rankB, string lotNo)
        {
            RankA = rankA;
            RankB = rankB;
            LotNo = lotNo;
        }

        public List<RankStruc> RankA { get; }
        public List<RankStruc> RankB { get; }
        public string LotNo { get; }
    }

    public class RankStruc
    {
        public RankStruc(string rank, string ID, string nc12, double qty)
        {
            Rank = rank;
            this.ID = ID;
            Nc12 = nc12;
            Qty = qty;
        }

        public string Rank { get; }
        public string ID { get; }
        public string Nc12 { get; }
        public double Qty { get; set; }
    }
    
}
