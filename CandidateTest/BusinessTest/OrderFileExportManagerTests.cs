using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Business.Managers.Export;
using Framework.Data;
using Framework.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace BusinessTest
{
    [TestFixture]
    public class OrderFileExportManagerTests
    {
        private IFileExportManager fileExportManager;
        private IConfiguration configuration;
        private IOrderExportManager manager;
        private string directoryPath;
        private string inputFilePath;
        private OrderCollection orders;

        [SetUp]
        public void SetUp()
        {
            var mockExportManager = new Mock<IFileExportManager>();
            var mockConfiguration = new Mock<IConfiguration>();
            directoryPath =@"..\..\..\TestFolder";
            inputFilePath = "test.csv";
            var inputTestName = "test";
            var outputFilePath = Path.Combine(directoryPath, inputTestName) + ".xml";
            mockConfiguration.Setup(conf => conf.GetSection("outputDirectory").Value).Returns(directoryPath);
            mockExportManager.Setup(em => em.GetFileName(inputFilePath)).Returns(inputTestName);
            mockExportManager.Setup(em=>em.IsDirectoryExists(directoryPath)).Returns(true);
            
            fileExportManager = mockExportManager.Object;
            configuration = mockConfiguration.Object;
            manager = new OrderFileExportManager(fileExportManager, configuration);
            orders =new OrderCollection();
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
            mockExportManager.Setup(em => em.ExportToFile<OrderCollection>(orders, directoryPath, inputTestName)).Returns(outputFilePath);
        }

        [Test]
        public void ExportData_WithNullInput_ReturnsEmptyFilePath()
        {
            
            var item = new ItemKey {Value= inputFilePath };
            var result = manager.ExportOrders(orders, item);
            Assert.True(result);
        }
    }
}
