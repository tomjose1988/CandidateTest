using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business.Entities
{
    public class Order
    {

        private string orderNo;
        private List<Consignment> consignments;

        public Order()
        {
            consignments = new List<Consignment>();
        }
        
        [XmlAttribute("OrderNo")]
        public string OrderNo { get { return orderNo; } set { orderNo = value; } }

        [XmlAttribute("TotalValue")]
        public double TotalValue 
        { 
            get 
            {
                return GetTotalValue(); 
            }
            set
            {

            }
        }

        [XmlAttribute("TotalWeight")]
        public double TotalWeight
        {
            get
            {
                return GetTotalWeight();
            }
            set
            {

            }
        }

        [XmlArray("Consignments")]
        [XmlArrayItem("Consignment")]
        public List<Consignment> Consignments { get { return new List<Consignment>(consignments); } }

        public void AddConsignment(Consignment consignment)
        {
            consignments.Add(consignment);
        }

        public Consignment GetConsignment(string consignmentNo)
        {
           return this.consignments.Find(c => c.ConsignmentNo == consignmentNo);
        }

        private double GetTotalValue()
        {
            double total = 0;
            foreach (var consignment in this.consignments)
            {
                total += consignment.GetTotalValue();
            }
            return total;
        }

        private double GetTotalWeight()
        {
            double total = 0;
            foreach (var consignment in this.consignments)
            {
                total += consignment.GetTotalWeight();
            }
            return total;
        }

    }
}
