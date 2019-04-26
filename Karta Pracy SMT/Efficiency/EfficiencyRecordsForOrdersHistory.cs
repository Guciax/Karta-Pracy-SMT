using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT.Efficiency
{
    public class EfficiencyRecordsForOrdersHistory
    {
        public static void FillOutListView(ListView lV, List<EfficiencyCalculation.OrderDataForEfficiencyStructure> ordersEff)
        {
            Dictionary<string, List<EfficiencyCalculation.OrderDataForEfficiencyStructure>> grouppedByDateShift = new Dictionary<string, List<EfficiencyCalculation.OrderDataForEfficiencyStructure>>();
            foreach (var orderEff in ordersEff.OrderByDescending(o=>o.dateShiftOwner.fixedDate))
            {
                string dateShiftKey = $"{orderEff.dateShiftOwner.fixedDate.ToString("dd-MMM")} {orderEff.dateShiftOwner.shift}zm.";
                if (!grouppedByDateShift.ContainsKey(dateShiftKey))
                {
                    grouppedByDateShift.Add(dateShiftKey, new List<EfficiencyCalculation.OrderDataForEfficiencyStructure>());
                }
                grouppedByDateShift[dateShiftKey].Add(orderEff);
            }

            foreach (var shiftEntry in grouppedByDateShift)
            {
                List<Tuple<double, double>> effDurationList = new List<Tuple<double, double>>();
                foreach (var order in shiftEntry.Value)
                {
                    var eff = EfficiencyCalculation.CalculateEfficiency(order.start, order.end, order.modelId, order.qty, true);

                    effDurationList.Add(new Tuple<double, double>(eff, (order.end - order.start).TotalHours));
                }

                var weightedAvergae = Math.Round(EfficiencyCalculation.WeightedAverage(effDurationList)*100,0)+"%";

                var item = new ListViewItem(new[] { shiftEntry.Key, shiftEntry.Value.Select(o => o.qty).Sum().ToString(), weightedAvergae.ToString() });
                lV.Items.Add(item);
            }

            foreach (ColumnHeader column in lV.Columns)
            {
                column.Width = -2; //=autosize
            }
        }
    }
}
