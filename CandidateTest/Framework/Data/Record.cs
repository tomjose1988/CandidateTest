using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class Record
    {
        private int index;
        private List<Property> properties;

        public Record(int index)
        {
            this.index = index;
            properties = new List<Property>();  
        }

        public void AddProperty(Property property)
        {
            properties.Add(property);   
        }

        public Property GetProperty(string propertyName)
        {
            return properties.Find(p=>p.Name == propertyName);
        }
    }
}
