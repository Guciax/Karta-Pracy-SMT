using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT.Data_Structures
{
    class CurrentMstOrder
    {
        public CurrentMstOrder(string orderNumber, string oper, Int32 orderedQty, Int32 madeQty, DateTime dateStart, string stencil, string nc12, DateTime lastUpdateTime, int pcbOnMb, List<string> ledReels)
        {
            OrderNumber = orderNumber;
            Oper = oper;
            OrderedQty = orderedQty;
            MadeQty = madeQty;
            DateStart = dateStart;
            Stencil = stencil;
            Nc12 = nc12;
            LastUpdateTime = lastUpdateTime;
            PcbOnMb = pcbOnMb;
            LedReels = ledReels;
        }

        public string OrderNumber { get; set; }
        public string Oper { get; set; }
        public int OrderedQty { get; set; }
        public int MadeQty { get; set; }
        public DateTime DateStart { get; set; }
        public string Stencil { get; set; }
        public string Nc12 { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int PcbOnMb { get; set; }
        public List<string> LedReels { get; }
    }
}
