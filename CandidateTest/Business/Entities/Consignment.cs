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
        private string _consignmentNo;
        private string _consigneeName;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _countryCode;
        private List<Parcel> _parcels;

        public Consignment()
        {
            this._parcels = new List<Parcel>();
        }

        [XmlAttribute("ConsignmentNo")]
        public string ConsignmentNo { get { return this._consignmentNo; } set { this._consignmentNo = value; } }
        [XmlAttribute("ConsigneeName")]
        public string ConsigneeName { get { return this._consigneeName; } set { this._consigneeName = value; } }
        [XmlAttribute("Address1")]
        public string Address1 { get { return this._address1; } set { this._address1 = value; } }
        [XmlAttribute("Address2")]
        public string Address2 { get { return this._address2; } set { this._address2 = value; } }
        [XmlAttribute("City")]
        public string City { get { return this._city; } set { this._city = value; } }
        [XmlAttribute("State")]
        public string State { get { return this._state; } set { this._state = value; } }
        [XmlAttribute("CountryCode")]
        public string CountryCode { get { return this._countryCode; } set { this._countryCode = value; } }

        [XmlIgnore]
        public Order Order { get; set; }

        [XmlArray("Parcels")]
        [XmlArrayItem("Parcel")]
        public List<Parcel> Parcels { get { return new List<Parcel>(this._parcels); } }

        public Parcel GetParcel(string parcelCode)
        {
            return this._parcels.Find(p=>p.ParcelCode==parcelCode);
        }

        public void AddParcel(Parcel parcel)
        {
            this._parcels.Add(parcel);
        }

        public double GetTotalValue()
        {
            double total = 0;
            foreach (Parcel parcel in this._parcels)
            {
                total+=parcel.GetTotalValue();
            }
            return total;
        }

        public double GetTotalWeight()
        {
            double total = 0;
            foreach (Parcel parcel in this._parcels)
            {
                total += parcel.GetTotalWeight();
            }
            return total;
        }

    }
}
