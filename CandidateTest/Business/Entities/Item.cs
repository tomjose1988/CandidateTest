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
        private string currency;
        public Item()
        {
            this.currency = "GBP";
        }

        [XmlAttribute("ItemDescription")]
        public string Description { get; set; }
        [XmlAttribute("ItemCurrency")]
        public string Currency 
        {
            get
            {
                return this.currency;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.currency = value;
                }
            }
        }
        [XmlAttribute("ItemQuantity")]
        public string Quantity { get; set; }
        [XmlAttribute("ItemValue")]
        public double Value { get; set; }
        [XmlAttribute("ItemWeight")]
        public double Weight { get; set; }

        [XmlIgnore]
        public Parcel Parcel { get; set; }

        
    }
}
