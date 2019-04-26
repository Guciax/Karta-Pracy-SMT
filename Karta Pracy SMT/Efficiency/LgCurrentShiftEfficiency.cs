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
    public class LgCurrentShiftEfficiency
    {
        private static List<OrderDataForEfficiencyStructure> GetCurrentShiftLgOrders(DataGridView lgGrid)
        {
            List<OrderDataForEfficiencyStructure> result = new List<OrderDataForEfficiencyStructure>();
            foreach (DataGridViewRow row in lgGrid.Rows)
            {
                if (row.Cells["EndDate"].Value == null) continue;
                DateTime start = DateTime.Parse(row.Cells["StartDate"].Value.ToString());
                DateTime end = DateTime.Parse(row.Cells["EndDate"].Value.ToString());
                var owningShift = DateTools.GetOrderOwningShift(start, end);
                var currentShift = DateTools.whatDayShiftIsit(DateTime.Now);
                if (currentShift.fixedDate != owningShift.fixedDate) continue;

                int qty = int.Parse(row.Cells["ColumnQty"].Value.ToString());
                string modelId = row.Cells["ColumnModel"].Value.ToString().Replace(" ", "");

                result.Add(new OrderDataForEfficiencyStructure()
                {
                    start = start,
                    end = end,
                    modelId = modelId,
                    qty = qty,
                    rowIndex=row.Index
                });
            }
            return result;
        }

        public static double CalculateCurrentShiftEff(DataGridView lgGrid)
        {
            List<OrderDataForEfficiencyStructure> currentShiftOrders = GetCurrentShiftLgOrders(lgGrid).OrderBy(o=>o.rowIndex).ToList();
            if (currentShiftOrders.Count < 0) return -1;
            List<Tuple<double, double>> ordersEffDurationList = new List<Tuple<double, double>>();
            

            foreach (var order in currentShiftOrders)
            {
                int changeOverTime = 5;
                if (order.rowIndex <= lgGrid.Rows.Count - 1)
                {
                    if(order.modelId != lgGrid.Rows[order.rowIndex + 1].Cells["ColumnModel"].Value.ToString())
                    {
                        changeOverTime = 25;
                    }
                }

                ordersEffDurationList.Add(new Tuple<double, double>(
                                               EfficiencyCalculation.CalculateEfficiency(order.start, order.end.AddMinutes(-changeOverTime), order.modelId, order.qty, false),
                                               (order.end - order.start).TotalHours));
            }

            return EfficiencyCalculation.WeightedAverage(ordersEffDurationList) * 100;
        }
    }
}
