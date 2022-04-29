using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Framework.Data;
using Framework.ImportExport.File.Import.CSV;
using Framework.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Business.Managers.Import
{
    public class OrderFileImportManager: IOrderImportManager
    {

        private object _lockObj = new object();
        private IFileImportManager _fileImportManager;
        private string _inputDirectory;
        private string _inputArchiveDirectory;
        private int _archiveThreshold;
        private List<string> _processedInputFiles;
        private List<string> _currentlyQueuedFiles;

        public OrderFileImportManager(IFileImportManager fileImportManager, IConfiguration configuration)
        {
            this._inputDirectory = configuration.GetSection("inputDirectory").Value;
            this._inputArchiveDirectory = configuration.GetSection("inputArchiveDirectory").Value;
            var threshold = configuration.GetSection("inputArchiveThreshold").Value;
            int.TryParse(threshold, out this._archiveThreshold);
            this._fileImportManager= fileImportManager;
            //this._watcher = new FileSystemWatcher(this._inputDirectory);
            //this._watcher.Created += this.OnCreated;
            this._currentlyQueuedFiles = new List<string>();
            this._processedInputFiles = new List<string>();
        }

        public OrderCollection FormatOrderData(ImportData data)
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

                        var itemQty = (string)record.GetProperty("Item Quantity")?.Value;
                        int itemQuantity = 0;
                        int.TryParse(itemQty, out itemQuantity);
                        item.Quantity = itemQuantity;


                        var itemVal= (string)record.GetProperty("Item Value")?.Value;
                        double itemValue = 0;
                        double.TryParse(itemVal, out itemValue);
                        item.Value = itemValue;

                        var itemWgt = (string)record.GetProperty("Item Weight")?.Value;
                        double itemWeight = 0;
                        double.TryParse(itemWgt, out itemWeight);
                        item.Weight = itemWeight;

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

        public ImportData ImportOrderData(ItemKey key)
        {
            var filePath = key.Value;
            var data=this._fileImportManager.ImportFile(filePath);
            return data;
        }

        public void AddProcessed(ItemKey key)
        {
            AddProcessedInputFile(key.Value);
            CheckAndArchive();
        }

        public List<ItemKey> GetImportItemKeys()
        {
            var items = new List<ItemKey>();
            FillFileQueue();
            foreach (var file in this._currentlyQueuedFiles)
            {
                var item = new ItemKey { Value = file };
                items.Add(item);
            }
            return items;
        }


        private void FillFileQueue()
        {
            var files = this._fileImportManager.EnumerateFiles(this._inputDirectory);
            foreach (var file in files)
            {
                AddFile(file);
            }
        }


        private void AddFile(string file)
        {
            if (!this._currentlyQueuedFiles.Contains(file) && !this._processedInputFiles.Contains(file))
            {
                this._currentlyQueuedFiles.Add(file);
            }
        }

        private void CheckAndArchive()
        {
            if (this._processedInputFiles.Count == this._archiveThreshold)
            {
                if (!this._fileImportManager.IsDirectoryExists(this._inputArchiveDirectory))
                {
                    this._fileImportManager.CreateDirectory(this._inputArchiveDirectory);
                }
                var movedFiles = this._fileImportManager.MoveFiles(this._processedInputFiles, this._inputArchiveDirectory);
                foreach (var file in movedFiles)
                {
                    this._processedInputFiles.Remove(file);
                }
            }
        }

        private void AddProcessedInputFile(string file)
        {
            this._currentlyQueuedFiles.Remove(file);
            this._processedInputFiles.Add(file);
        }
    }
}
