using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Extensions
{
    public static class DateTimeExtention
    {
        public static int CalcRemainingdays(this DateTime BorrowedUntil)
        {
            DateTime StartDate = DateTime.Today;

            return (int)(BorrowedUntil - StartDate).TotalDays;
        }
    }
}
