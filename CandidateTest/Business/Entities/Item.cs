using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }    
        public int Quantity { get; set; }
        public double Value { get; set; }
        public double Weight { get; set; }

        public Parcel Parcel { get; set; }
        
    }
}
