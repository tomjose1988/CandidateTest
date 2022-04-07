
using Framework.Data;
using Business.Managers.Import;
using NUnit.Framework;

namespace BusinessTest
{
    [TestFixture]
    public class OrdersCSVFileFormatTest
    {
        private ImportData data;

        [SetUp]
        public void SetUp()
        {
            data=new ImportData();
            data.AddCoumnHeader(0, "Order No");
            data.AddCoumnHeader(1, "Consignment No");
            data.AddCoumnHeader(2, "Address 1");
            data.AddCoumnHeader(3, "Address 2");
            data.AddCoumnHeader(4, "City");
            data.AddCoumnHeader(5, "State");
            data.AddCoumnHeader(6, "Country Code");
            data.AddCoumnHeader(7, "Parcel Code");
            data.AddCoumnHeader(8, "Item Description");
            data.AddCoumnHeader(9, "Item Quantity");
            data.AddCoumnHeader(10, "Item Value");
            data.AddCoumnHeader(11, "Item Weight");
            data.AddCoumnHeader(12, "Item Currency");
            data.AddCoumnHeader(13, "Consignee Name");

            data.AddRow(0, 0, "Order1");
            data.AddRow(0, 1, "Consignment1"); 
            data.AddRow(0, 2, "46");
            data.AddRow(0, 3, "Lothian");
            data.AddRow(0, 4, "Stockton");
            data.AddRow(0, 5, "NorthEast");
            data.AddRow(0, 6, "GB");
            data.AddRow(0, 7, "Parcel1");
            data.AddRow(0, 8, "Book");
            data.AddRow(0, 9, "2");
            data.AddRow(0, 10, "20.50");
            data.AddRow(0, 11, "200");
            data.AddRow(0, 12, "£");
            data.AddRow(0, 13, "Consignee1");

            data.AddRow(1, 0, "Order1");
            data.AddRow(1, 1, "Consignment1");
            data.AddRow(1, 2, "46");
            data.AddRow(1, 3, "Lothian");
            data.AddRow(1, 4, "Stockton");
            data.AddRow(1, 5, "NorthEast");
            data.AddRow(1, 6, "GB");
            data.AddRow(1, 7, "Parcel1");
            data.AddRow(1, 8, "Box");
            data.AddRow(1, 9, "3");
            data.AddRow(1, 10, "10.50");
            data.AddRow(1, 11, "100");
            data.AddRow(1, 12, "£");
            data.AddRow(1, 13, "Consignee1");

            data.AddRow(2, 0, "Order1");
            data.AddRow(2, 1, "Consignment1");
            data.AddRow(2, 2, "46");
            data.AddRow(2, 3, "Lothian");
            data.AddRow(2, 4, "Stockton");
            data.AddRow(2, 5, "NorthEast");
            data.AddRow(2, 6, "GB");
            data.AddRow(2, 7, "Parcel2");
            data.AddRow(2, 8, "Drone");
            data.AddRow(2, 9, "1");
            data.AddRow(2, 10, "90.50");
            data.AddRow(2, 11, "500");
            data.AddRow(2, 12, "£");
            data.AddRow(2, 13, "Consignee1");

            data.AddRow(3, 0, "Order1");
            data.AddRow(3, 1, "Consignment2");
            data.AddRow(3, 2, "7");
            data.AddRow(3, 3, "Egmond Road");
            data.AddRow(3, 4, "Middlesbrough");
            data.AddRow(3, 5, "NorthEast");
            data.AddRow(3, 6, "GB");
            data.AddRow(3, 7, "MobTech");
            data.AddRow(3, 8, "iPhone");
            data.AddRow(3, 9, "1");
            data.AddRow(3, 10, "190.50");
            data.AddRow(3, 11, "200");
            data.AddRow(3, 12, "£");
            data.AddRow(3, 13, "Consignee2");

            data.AddRow(4, 0, "Order1");
            data.AddRow(4, 1, "Consignment2");
            data.AddRow(4, 2, "7");
            data.AddRow(4, 3, "Egmond Road");
            data.AddRow(4, 4, "Middlesbrough");
            data.AddRow(4, 5, "NorthEast");
            data.AddRow(4, 6, "GB");
            data.AddRow(4, 7, "MobTech");
            data.AddRow(4, 8, "iPhone charger");
            data.AddRow(4, 9, "1");
            data.AddRow(4, 10, "80.50");
            data.AddRow(4, 11, "100");
            data.AddRow(4, 12, "£");
            data.AddRow(4, 13, "Consignee2");

            data.AddRow(5, 0, "Order1");
            data.AddRow(5, 1, "Consignment2");
            data.AddRow(5, 2, "7");
            data.AddRow(5, 3, "Egmond Road");
            data.AddRow(5, 4, "Middlesbrough");
            data.AddRow(5, 5, "NorthEast");
            data.AddRow(5, 6, "GB");
            data.AddRow(5, 7, "AgriTech");
            data.AddRow(5, 8, "Soil Tester");
            data.AddRow(5, 9, "1");
            data.AddRow(5, 10, "290.50");
            data.AddRow(5, 11, "100");
            data.AddRow(5, 12, "£");
            data.AddRow(5, 13, "Consignee2");

            data.AddRow(6, 0, "Order1");
            data.AddRow(6, 1, "Consignment2");
            data.AddRow(6, 2, "7");
            data.AddRow(6, 3, "Egmond Road");
            data.AddRow(6, 4, "Middlesbrough");
            data.AddRow(6, 5, "NorthEast");
            data.AddRow(6, 6, "GB");
            data.AddRow(6, 7, "AgriTech");
            data.AddRow(6, 8, "Tempearture Tester");
            data.AddRow(6, 9, "1");
            data.AddRow(6, 10, "120.50");
            data.AddRow(6, 11, "70");
            data.AddRow(6, 12, "£");
            data.AddRow(6, 13, "Consignee2");

            data.AddRow(7, 0, "NewOrder1");
            data.AddRow(7, 1, "NewConsignment1");
            data.AddRow(7, 2, "20");
            data.AddRow(7, 3, "Ayresome Road");
            data.AddRow(7, 4, "Middlesbrough");
            data.AddRow(7, 5, "NorthEast");
            data.AddRow(7, 6, "GB");
            data.AddRow(7, 7, "Wearables");
            data.AddRow(7, 8, "Shoe");
            data.AddRow(7, 9, "1");
            data.AddRow(7, 10, "80.50");
            data.AddRow(7, 11, "170");
            data.AddRow(7, 12, "£");
            data.AddRow(7, 13, "NewConsignee");
        }

        [Test]
        public void FormatData_WithNullInput_ReturnsEmptyObject()
        {
            var ordersCSVFileManager=new OrderCSVFileImportManager();
            var orderCollection=ordersCSVFileManager.FormatData(null);
            Assert.IsNotNull(orderCollection);
        }

        [Test]
        public void FormatData_WithEmptyInput_ReturnsEmptyObject()
        {
            var ordersCSVFileManager = new OrderCSVFileImportManager();
            var orderCollection = ordersCSVFileManager.FormatData(new Framework.Data.ImportData());
            Assert.IsNotNull(orderCollection);
        }

        [Test]
        public void FormatData_WithValidInput_ReturnsProperOrderCollection()
        {
            var ordersCSVFileManager = new OrderCSVFileImportManager();
            var orderCollection = ordersCSVFileManager.FormatData(data);
            Assert.That(orderCollection, Is.Not.Null);
            var orderList = orderCollection.OrderList;
            Assert.That(orderList, Is.Not.Null);
            Assert.That(orderList.Count==2);
            {
                var order1 = orderList[0];
                Assert.That(order1.OrderNo, Is.EqualTo("Order1"));
                var consignments = order1.Consignments;
                Assert.That(consignments.Count, Is.EqualTo(2));
                {
                    var consignment1 = consignments[0];
                    Assert.That(consignment1.ConsignmentNo, Is.EqualTo("Consignment1"));
                    var parcels = consignment1.Parcels;
                    Assert.That(parcels.Count, Is.EqualTo(2));
                    {
                        var parcel1 = parcels[0];
                        Assert.That(parcel1.ParcelCode, Is.EqualTo("Parcel1"));
                        var items=parcel1.Items;
                        Assert.That(items.Count, Is.EqualTo(2));
                        {
                            var item1 = items[0];
                            Assert.That(item1.Description, Is.EqualTo("Book"));
                            var item2 = items[1];
                            Assert.That(item2.Description, Is.EqualTo("Box"));
                        }

                        var parcel2 = parcels[1];
                        Assert.That(parcel2.ParcelCode, Is.EqualTo("Parcel2"));
                        var pitems = parcel2.Items;
                        Assert.That(pitems.Count, Is.EqualTo(1));
                        {
                            var item1 = pitems[0];
                            Assert.That(item1.Description, Is.EqualTo("Drone"));
                        }
                    }

                    var consignment2 = consignments[1];
                    Assert.That(consignment2.ConsignmentNo, Is.EqualTo("Consignment2"));
                    var cparcels = consignment2.Parcels;
                    Assert.That(cparcels.Count, Is.EqualTo(2));
                    {
                        var parcel1 = cparcels[0];
                        Assert.That(parcel1.ParcelCode, Is.EqualTo("MobTech"));
                        var items = parcel1.Items;
                        Assert.That(items.Count, Is.EqualTo(2));
                        {
                            var item1 = items[0];
                            Assert.That(item1.Description, Is.EqualTo("iPhone"));
                            var item2 = items[1];
                            Assert.That(item2.Description, Is.EqualTo("iPhone charger"));
                        }

                        var parcel2 = cparcels[1];
                        Assert.That(parcel2.ParcelCode, Is.EqualTo("AgriTech"));
                        var pitems = parcel2.Items;
                        Assert.That(pitems.Count, Is.EqualTo(2));
                        {
                            var item1 = pitems[0];
                            Assert.That(item1.Description, Is.EqualTo("Soil Tester"));
                            var item2 = pitems[1];
                            Assert.That(item2.Description, Is.EqualTo("Tempearture Tester"));
                        }
                    }
                }

                var order2 = orderList[1];
                Assert.That(order2.OrderNo, Is.EqualTo("NewOrder1"));
                var newConsignments = order2.Consignments;
                Assert.That(newConsignments.Count, Is.EqualTo(1));
                {
                    var consignment1 = newConsignments[0];
                    Assert.That(consignment1.ConsignmentNo, Is.EqualTo("NewConsignment1"));
                    var parcels = consignment1.Parcels;
                    Assert.That(parcels.Count, Is.EqualTo(1));
                    {
                        var parcel1 = parcels[0];
                        Assert.That(parcel1.ParcelCode, Is.EqualTo("Wearables"));
                        var items = parcel1.Items;
                        Assert.That(items.Count, Is.EqualTo(1));
                        {
                            var item1 = items[0];
                            Assert.That(item1.Description, Is.EqualTo("Shoe"));
                        }
                    }                    
                }

            }

        }
    }
}
