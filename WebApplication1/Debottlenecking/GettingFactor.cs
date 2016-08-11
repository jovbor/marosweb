using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Debottlenecking
{
    public class GetFactor
    {
        public static FlowTable GettingFactor()
        {
            var factorisedTable = new FlowTable();
            var flowTable = GetFlowTable.GetTable();

            factorisedTable = flowTable;

            double capacity = 100;

            //Calculating the factor
            foreach (var time in factorisedTable)
            {
                time.Factor = time.TotalRate > 0 && time.TotalRate > capacity ? capacity / time.TotalRate : 1;
            }

            return factorisedTable;

        }
    }

}
