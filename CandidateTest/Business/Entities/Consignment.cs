using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Consignment
    {
        public int ConsignmentNo { get; set; }
        public string Consignee { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public Order Order { get; set; }
        List<Parcel> Parcels { get; set; }  

    }
}
