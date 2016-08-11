using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debottlenecking
{
    public class Acummulator
    {
        public static FlowTable DefferedProduction()
        {
            
            var flowTable = GetFlowTable.GetTable();

            var factorisedTable = GetFactor.GettingFactor();

            double capacity = 100;

            //factorising new flow table
            foreach (var item in factorisedTable)
            {
                if (item.Factor < 1)
                {
                    for (int i = 0; i < item.WellRates.Count; i++)
                    {
                        item.WellRates[i].Rate *= item.Factor;
                    }
                }
            }
            //accumulating the potential production 
            var accumulator = new double();

            //adding new rates to the factorised table
            foreach (var time in factorisedTable)
            {
                accumulator = flowTable.Find(x => x.MyDateTime == time.MyDateTime).TotalDeferred + accumulator;

                if (accumulator > 0 && time.Factor == 1)
                {
                    var addedProduction = capacity - time.TotalRate;

                    if (addedProduction > accumulator)
                    {
                        addedProduction = accumulator;
                    }

                    for (int i = 0; i < time.WellRates.Count; i++)
                    {
                        time.WellRates[i].Rate = time.WellRates[i].Rate + (time.WellRates[i].Rate / (flowTable.Find(x => x.MyDateTime == time.MyDateTime).TotalRate)) * addedProduction;
                    }
                    accumulator = accumulator - addedProduction;
                }
            }
            return factorisedTable;
        }
    }
}
        