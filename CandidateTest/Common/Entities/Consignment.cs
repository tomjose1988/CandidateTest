using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.Entities
{
    public class Consignment
    {
        private string consignmentNo;
        private string consigneeName;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string countryCode;
        private List<Parcel> parcels;

        public Consignment()
        {
            parcels = new List<Parcel>();
        }

        [XmlAttribute("Consignment No")]
        public string ConsignmentNo { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("Consignee Name")]
        public string ConsigneeName { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("Address 1")]
        public string Address1 { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("Address 2")]
        public string Address2 { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("City")]
        public string City { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("State")]
        public string State { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("Country Code")]
        public string CountryCode { get { return consignmentNo; } set { consignmentNo = value; } }
        public Order Order { get; set; }

        [XmlArray("Parcels")]
        [XmlArrayItem("Parcel")]
        List<Parcel> Parcels { get { return new List<Parcel>(parcels); } }

        public Parcel GetParcel(string parcelCode)
        {
            return parcels.Find(p=>p.ParcelCode==parcelCode);
        }

        public void AddParcel(Parcel parcel)
        {
            this.parcels.Add(parcel);
        }

    }
}
