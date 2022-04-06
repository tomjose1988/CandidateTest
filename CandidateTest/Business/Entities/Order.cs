using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Order
    {
        public int OrderNo { get; set; }
        public List<Consignment> Consignments { get; set; }
    }
}
