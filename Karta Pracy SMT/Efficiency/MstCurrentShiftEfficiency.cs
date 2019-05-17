using Karta_Pracy_SMT.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Karta_Pracy_SMT.Efficiency.EfficiencyCalculation;

namespace Karta_Pracy_SMT.Efficiency
{
    

    public class MstCurrentShiftEfficiency
    {
        private static List<OrderDataForEfficiencyStructure> GetCurrentShiftMstOrders(DataGridView mstGrid)
        {
            List<OrderDataForEfficiencyStructure> result = new List<OrderDataForEfficiencyStructure>();
            foreach (DataGridViewRow row in mstGrid.Rows)
            {
                int qty = int.Parse(row.Cells["ColumnMstQty"].Value.ToString());
                //if (qty == 0) continue;
                DateTime start = (DateTime)row.Cells["MstOrdersStart"].Value;
                DateTime end = (DateTime)row.Cells["MstOrdersEnd"].Value;
                if (start == end)
                {
                    end = end.AddMinutes(1);
                }
                var owningShift = DateTools.GetOrderOwningShift(start, end);
                var currentShift = DateTools.whatDayShiftIsit(DateTime.Now);
                if (currentShift.fixedDate != owningShift.fixedDate) continue;

                
                string modelId = row.Cells["Column12NC"].Value.ToString().Replace(" ","");

                result.Add(new OrderDataForEfficiencyStructure()
                {
                    start = start,
                    end = end,
                    modelId = modelId,
                    qty = qty
                });
            }
            return result;
        }

        public static double CalculateCurrentShiftEff(CurrentMstOrder currentMstOrder,  DataGridView mstGrid)
        {
            List<OrderDataForEfficiencyStructure> currentShiftOrders = GetCurrentShiftMstOrders( mstGrid);

            List<Tuple<double, double>> ordersEffDurationList = new List<Tuple<double, double>>();
            if ((currentMstOrder.LastUpdateTime - currentMstOrder.DateStart).TotalHours > 0 & currentMstOrder.Nc10 != "") 
            {
                ordersEffDurationList.Add(new Tuple<double, double>(EfficiencyCalculation.CalculateEfficiency(currentMstOrder.DateStart, currentMstOrder.LastUpdateTime, currentMstOrder.Nc10, currentMstOrder.MadeQty, true),
                                                (currentMstOrder.LastUpdateTime - currentMstOrder.DateStart).TotalHours));
            }
            
            foreach (var order in currentShiftOrders)
            {
                try
                {
                    ordersEffDurationList.Add(new Tuple<double, double>(
                                                   EfficiencyCalculation.CalculateEfficiency(order.start, order.end, order.modelId, order.qty, true),
                                                   (order.end - order.start).TotalHours));
                }
                catch { continue; }
            }
            if (ordersEffDurationList.Count > 0)
            {
                return EfficiencyCalculation.WeightedAverage(ordersEffDurationList) * 100;
            }
            return -1;
        }
    }
}
