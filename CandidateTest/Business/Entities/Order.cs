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

        private string _orderNo;
        private List<Consignment> _consignments;

        public Order()
        {
            this._consignments = new List<Consignment>();
        }
        
        [XmlAttribute("OrderNo")]
        public string OrderNo { get { return this._orderNo; } set { this._orderNo = value; } }

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
        public List<Consignment> Consignments { get { return new List<Consignment>(this._consignments); } }

        public void AddConsignment(Consignment consignment)
        {
            this._consignments.Add(consignment);
        }

        public Consignment GetConsignment(string consignmentNo)
        {
           return this._consignments.Find(c => c.ConsignmentNo == consignmentNo);
        }

        private double GetTotalValue()
        {
            double total = 0;
            foreach (var consignment in this._consignments)
            {
                total += consignment.GetTotalValue();
            }
            return total;
        }

        private double GetTotalWeight()
        {
            double total = 0;
            foreach (var consignment in this._consignments)
            {
                total += consignment.GetTotalWeight();
            }
            return total;
        }

    }
}
