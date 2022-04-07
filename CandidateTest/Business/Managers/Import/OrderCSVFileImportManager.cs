using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Framework.Data;
using Framework.ImportExport.File.Import.CSV;

namespace Business.Managers.Import
{
    public class OrderCSVFileImportManager : CSVFileImportManager, IOrderCSVFileImportManager
    {
        public OrderCSVFileImportManager()
        {

        }

        public OrderCollection FormatData(ImportData data)
        {
            var collection=new OrderCollection();
            if (data != null)
            {
                for (int i = 0; i < data.RowCount; i++)
                {
                    var record = data.GetRecord(i);
                    object orderNumber = record.GetProperty("Order No")?.Value;
                    var orderNo = (string)orderNumber;
                    var order = collection.GetOrder(orderNo);
                    if (order == null)
                    {
                        order = new Order();
                        order.OrderNo = orderNo;
                        collection.AddOrder(order);
                    }
                    object consignmentNo = record.GetProperty("Consignment No")?.Value;
                    var conNumber = (string)consignmentNo;
                    var consignment = order.GetConsignment(conNumber);
                    if (consignment == null)
                    {
                        consignment = new Consignment();
                        consignment.ConsignmentNo = conNumber;
                        consignment.ConsigneeName= (string)record.GetProperty("Consignee Name")?.Value; 
                        consignment.Address1 = (string)record.GetProperty("Address 1")?.Value;
                        consignment.Address2 = (string)record.GetProperty("Address 2")?.Value;
                        consignment.City = (string)record.GetProperty("City")?.Value;
                        consignment.State = (string)record.GetProperty("State")?.Value;
                        consignment.CountryCode = (string)record.GetProperty("Country Code")?.Value;
                        consignment.Order = order;
                        order.AddConsignment(consignment);
                    }
                    var parcelCode = (string)record.GetProperty("Parcel Code")?.Value;
                    var parcel = consignment.GetParcel(parcelCode);
                    if (parcel == null)
                    {
                        parcel = new Parcel();
                        parcel.ParcelCode = parcelCode;
                        parcel.Consignment = consignment;
                        consignment.AddParcel(parcel);
                    }
                    var itemDescription = (string)record.GetProperty("Item Description")?.Value;
                    var item = parcel.GetItem(itemDescription);
                    if (item == null)
                    {
                        item = new Item();
                        item.Description = itemDescription;
                        item.Quantity = (string)record.GetProperty("Item Quantity")?.Value;
                        item.Value = (string)record.GetProperty("Item Value")?.Value;
                        item.Weight = (string)record.GetProperty("Item Weight")?.Value;
                        item.Currency = (string)record.GetProperty("Item Currency")?.Value;
                        item.Parcel = parcel;
                        parcel.AddItem(item);
                    }
                }
            }
            return collection;
        }

        /// <summary>
        /// Use this method to read in a format template file using which we can format the data in
        /// a specific way.
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, string> GetMappingTemplate()
        {
            Dictionary<string, string> template = new Dictionary<string, string>();
            template.Add("OrderNo", "Order No");
            template.Add("ConsignmentNo", "Consignment No");
            template.Add("ParcelCode", "Parcel Code");
            template.Add("Address1", "Address 1");
            template.Add("Address2", "Address 2");
            template.Add("City", "City");
            template.Add("State", "State");
            template.Add("CountryCode", "Country Code");
            template.Add("Quantity", "Item Quantity");
            template.Add("Value", "Item Value");
            template.Add("Weight", "Item Weight");
            template.Add("Description", "Item Description");
            template.Add("Currency", "Item Currency");
            return template;
        }
    }
}
