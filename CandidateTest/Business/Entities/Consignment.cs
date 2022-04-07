using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Business.Entities
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

        [XmlAttribute("ConsignmentNo")]
        public string ConsignmentNo { get { return consignmentNo; } set { consignmentNo = value; } }
        [XmlAttribute("ConsigneeName")]
        public string ConsigneeName { get { return consigneeName; } set { consigneeName = value; } }
        [XmlAttribute("Address1")]
        public string Address1 { get { return address1; } set { address1 = value; } }
        [XmlAttribute("Address2")]
        public string Address2 { get { return address2; } set { address2 = value; } }
        [XmlAttribute("City")]
        public string City { get { return city; } set { city = value; } }
        [XmlAttribute("State")]
        public string State { get { return state; } set { state = value; } }
        [XmlAttribute("CountryCode")]
        public string CountryCode { get { return countryCode; } set { countryCode = value; } }

        [XmlIgnore]
        public Order Order { get; set; }

        [XmlArray("Parcels")]
        [XmlArrayItem("Parcel")]
        public List<Parcel> Parcels { get { return new List<Parcel>(parcels); } }

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
