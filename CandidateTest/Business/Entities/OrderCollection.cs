using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business.Entities
{
    [XmlRoot("Order Collection")]
    public class OrderCollection
    {
        private List<Order> _orderList;

        public OrderCollection()
        {
            this._orderList = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            this._orderList.Add(order);
        }

        public Order GetOrder(string orderNumber)
        {
            return this._orderList.Find(o=>o.OrderNo == orderNumber);
        }

        [XmlArray("Orders")]
        [XmlArrayItem("Order")]
        public List<Order> OrderList { get { return new List<Order>(this._orderList); } }
    }
}
