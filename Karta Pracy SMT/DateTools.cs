using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT
{
    public class DateTools
    {
        ///<summary>
        ///<para>for orders carried out on 2 shifts, returns longer time shift</para>
        ///</summary>
        public static dateShiftNo GetOrderOwningShift(DateTime start, DateTime end)
        {
            //if (start.Hour == 5)
            //    ;
            var startShiftInfo = whatDayShiftIsit(start);
            var endShiftInfo = whatDayShiftIsit(end);
            if (startShiftInfo.fixedDate == endShiftInfo.fixedDate) return startShiftInfo;

            if ((endShiftInfo.fixedDate - start).TotalMinutes >= (end - endShiftInfo.fixedDate).TotalMinutes) return startShiftInfo;
            return endShiftInfo;
        }

        public class dateShiftNo
        {
            public DateTime fixedDate { get; set; }
            public int shift { get; set; }

            public override bool Equals(object obj)
            {
                dateShiftNo dateItem = obj as dateShiftNo;

                return dateItem.fixedDate == this.fixedDate;

            }

            public override int GetHashCode()
            {
                // Which is preferred?

                //return base.GetHashCode();

                return this.fixedDate.GetHashCode();
            }
        }

        ///<summary>
        ///<para>returns shift number and shift start date and time</para>
        ///</summary>
        public static dateShiftNo whatDayShiftIsit(DateTime inputDate)
        {
            int hourNow = inputDate.Hour;
            DateTime resultDate = new DateTime();
            int resultShift = 0;

            if (hourNow < 6)
            {
                resultDate = new DateTime(inputDate.Date.AddDays(-1).Year, inputDate.Date.AddDays(-1).Month, inputDate.Date.AddDays(-1).Day, 22, 0, 0);
                resultShift = 3;
            }

            else if (hourNow < 14)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 6, 0, 0);
                resultShift = 1;
            }

            else if (hourNow < 22)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 14, 0, 0);
                resultShift = 2;
            }

            else
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 22, 0, 0);
                resultShift = 3;
            }

            dateShiftNo result = new dateShiftNo();
            result.fixedDate = resultDate;
            result.shift = resultShift;

            return result;
        }




    }
}
