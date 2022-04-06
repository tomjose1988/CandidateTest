using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Entities
{
    public class Order
    {

        private string orderNo;
        private List<Consignment> consignments;

        public Order()
        {
            consignments = new List<Consignment>();
        }
        
        [XmlAttribute("Order No")]
        public string OrderNo { get { return orderNo; } set { orderNo = value; } }

        [XmlArray("Consignments")]
        [XmlArrayItem("Consignment")]
        public List<Consignment> Consignments { get { return new List<Consignment>(consignments); } }

        public void Add(Consignment consignment)
        {
            consignments.Add(consignment);
        }

        public Consignment GetConsignment(string consignmentNo)
        {
           return this.consignments.Find(c => c.ConsignmentNo == consignmentNo);
        }

    }
}
