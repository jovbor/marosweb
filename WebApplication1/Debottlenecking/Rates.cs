using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debottlenecking
{
    public class Rates
    {
        public string NodeName { get; set; }

        public double Rate { get; set; } 

        public int NodeID { get; set; }

        //we need to account for the well capacity - a well can be producing above its capacity 
        //e.g. Production is potentially 110 and capacity is 100

        //public double DeferredProduction 
        //{
        //    get
        //    {
        //        double capacity = 100;
        //        if (Rate > capacity)
        //            return (Rate - capacity);
        //    }
        //}
    }
}
