using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debottlenecking
{
    public class Well
    {
        public string Name { get; set; }

        public int NodeID { get; set; }

        public int FlowID { get; set; }

        public double Capacity { get; set; }

        public string Units { get; set; }

           
    }
}
