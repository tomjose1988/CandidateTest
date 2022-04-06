using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Entities
{
    public class Item
    {
        public string Id { get; set; }
        [XmlAttribute("Item Description")]
        public string Description { get; set; }
        [XmlAttribute("Item Currency")]
        public string Currency { get; set; }
        [XmlAttribute("Item Quantity")]
        public string Quantity { get; set; }
        [XmlAttribute("Item Value")]
        public string Value { get; set; }
        [XmlAttribute("Item Weight")]
        public string Weight { get; set; }

        public Parcel Parcel { get; set; }
        
    }
}
