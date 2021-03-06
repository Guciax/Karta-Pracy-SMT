﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT.Data_Structures
{
    public class CurrentMstOrder
    {
        public CurrentMstOrder(string orderNumber, string oper, int orderedQty, int madeQty, DateTime dateStart, string stencil, string nc10, DateTime lastUpdateTime, int pcbOnMb, int ledQty, int connQty, int resQty, List<ledReelData> ledReels, string modelName, int binQty, double normPerHour, int previouslyManufacturedQty=0,  int recordId=0)
        {
            OrderNumber = orderNumber;
            Oper = oper;
            OrderedQty = orderedQty;
            MadeQty = madeQty;
            DateStart = dateStart;
            Stencil = stencil;
            Nc10 = nc10;
            LastUpdateTime = lastUpdateTime;
            PcbOnMb = pcbOnMb;
            ConnQty = connQty;
            ResQty = resQty;
            LedReels = ledReels;
            ModelName = modelName;
            BinQty = binQty;
            NormPerHour = normPerHour;
            PreviouslyManufacturedQty = previouslyManufacturedQty;
            RecordId = recordId;
            LedQty = ledQty;
        }

        public string OrderNumber { get; set; }
        public string Oper { get; set; }
        public int OrderedQty { get; set; }
        public int MadeQty { get; set; }
        public DateTime DateStart { get; set; }
        public string Stencil { get; set; }
        public string Nc10 { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int PcbOnMb { get; set; }
        public int ConnQty { get; set; }
        public int ResQty { get; set; }
        public List<ledReelData> LedReels { get; set; }
        public string ModelName { get; set; }
        public int BinQty { get; set; }
        public double NormPerHour { get; set; }
        public int PreviouslyManufacturedQty { get; set; }
        public int RecordId { get; set; }
        public int LedQty { get; set; }
    }
}
