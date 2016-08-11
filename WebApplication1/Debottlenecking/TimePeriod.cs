using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debottlenecking
{
    public class TimePeriod
    {
        public TimePeriod()
        {
            WellRates = new List<Rates>();
        }

        public DateTime MyDateTime { get; set; }

        public int RowId { get; set; }

        public List<Rates> WellRates { get; set; }

        public double TotalRate
        {
            get
            {
                return WellRates.Sum(x => x.Rate);
            }

        }

        public double Factor { get; set; }

        public double TotalDeferred
        {
            get
            {
                double capacity = 100;
                double TotalDeferred = (TotalRate - capacity)>0 ? (TotalRate - capacity) : 0;
                return TotalDeferred;

                //  wwhen dealing with well capacity we may need to change the way we are calculating deferred production
                //return WellRates.Sum(x => x.DeferredProduction);
            }
        }
    }
}
