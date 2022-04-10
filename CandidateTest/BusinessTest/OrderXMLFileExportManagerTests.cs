using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Managers.Export;
using NUnit.Framework;

namespace BusinessTest
{
    [TestFixture]
    public class OrderXMLFileExportManagerTests
    {
        OrderCollection orders;

        [SetUp]
        public void SetUp()
        {
            orders=new OrderCollection();
            var order=new Order();
            orders.AddOrder(order);
            var consignment=new Consignment();
            order.AddConsignment(consignment);
            consignment.ConsignmentNo = "CON001";
            consignment.ConsigneeName = "stephen";
            consignment.Address1 = "7,Egmond Road";
            consignment.Address2 = "Stockton";
            consignment.City = "Stockton";
            consignment.State = "North East";
            consignment.CountryCode = "GB";
            var parcel=new Parcel();
            parcel.ParcelCode = "Parcel1";
            consignment.AddParcel(parcel);
            var item=new Item();
            parcel.AddItem(item);
            item.Quantity = 5;
            item.Description = "Shoe";
            item.Weight = 5;
            item.Value = 10;
        }

        [Test]
        public void ExportData_WithNullInput_ReturnsEmptyFilePath()
        {
            var orderXMLFileManager = new OrderXMLFileExportManager();
            var directoryPath = @"..\..\..\TestFolder\";
            var fileName = "Test";
            var fileNameWithExtension = fileName + ".xml";
            var fullFilePath = Path.Combine(directoryPath, fileNameWithExtension);
            orderXMLFileManager.ExportToFile(orders, directoryPath, fileName);
            Assert.True(File.Exists(fullFilePath));
        }
    }
}
