using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business.Entities
{
    public class Parcel
    {
        private string parcelCode;
        private List<Item> items;

        public Parcel()
        {
            items=new List<Item>();
        }

        [XmlAttribute("Parcel Code")]
        public string ParcelCode { get { return parcelCode; } set { parcelCode = value; } }
        public Consignment Consignment { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<Item> Items { get { return new List<Item>(items); } }

        public Item GetItem(string itemDescription)
        {
            return items.FirstOrDefault(x => x.Description == itemDescription);
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
    }
}
