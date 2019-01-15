using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    public class ledReelData
    {
        private const bool @false = false;

        public ledReelData(string NC12, double Ilosc, string LPN_ID, string LPN_NC, string ZlecenieString, string ID, string rank, bool removedToTrash = false)
        {
            this.NC12 = NC12;
            this.Ilosc = Ilosc;
            this.LPN_ID = LPN_ID;
            this.LPN_NC = LPN_NC;
            this.ZlecenieString = ZlecenieString;
            this.ID = ID;
            Rank = rank;
            RemovedToTrash = removedToTrash;
        }

        public string NC12 { get; }
        public double Ilosc { get; }
        public string LPN_ID { get; }
        public string LPN_NC { get; }
        public string ZlecenieString { get; }
        public string ID { get; }
        public string Rank { get; }
        public bool RemovedToTrash { get; set; }
    }
}
