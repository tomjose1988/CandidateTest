using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business.Entities
{
    public class Item
    {
        public Item()
        {
            //Id=String.Empty;
            //Description=String.Empty;
            //Currency=String.Empty;
            //Quantity=String.Empty;
            //Value=String.Empty;
            //Weight=String.Empty;
        }

        public string Id { get; set; }
        [XmlAttribute("ItemDescription")]
        public string Description { get; set; }
        [XmlAttribute("ItemCurrency")]
        public string Currency { get; set; }
        [XmlAttribute("ItemQuantity")]
        public string Quantity { get; set; }
        [XmlAttribute("ItemValue")]
        public string Value { get; set; }
        [XmlAttribute("ItemWeight")]
        public string Weight { get; set; }

        [XmlIgnore]
        public Parcel Parcel { get; set; }
        
    }
}
