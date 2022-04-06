using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Entities
{
    [XmlRoot("Order Collection")]
    public class OrderCollection
    {
        private List<Order> orderList;

        public OrderCollection()
        {
            orderList = new List<Order>();
        }

        public void Add(Order order)
        {
            orderList.Add(order);
        }

        public Order GetOrder(string orderNumber)
        {
            return orderList.Find(o=>o.OrderNo == orderNumber);
        }

        [XmlArray("Orders")]
        [XmlArrayItem("Order")]
        public List<Order> OrderList { get; set; }
    }
}
