using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Parcel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        Consignment Consignment { get; set; }
        List<Item> items { get; set; }
    }
}
