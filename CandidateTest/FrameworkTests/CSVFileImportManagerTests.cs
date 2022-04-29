using NUnit.Framework;
using Framework.ImportExport.File.Import.CSV;
using System.Reflection;

namespace FrameworkTests
{
    public class CSVFileImportManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InportData_WithInValidFile_ReturnsEmptyImportData()
        {
            var csvFileImportManager = new CSVFileImportManager();
            var importData = csvFileImportManager.ImportFile(@"..\invalid");
            Assert.IsNotNull(importData);
        }

        [Test]
        public void InportData_WithValidFile_ReturnsNonEmptyImportData()
        {
            var csvFileImportManager = new CSVFileImportManager();
            var importData = csvFileImportManager.ImportFile(@"..\..\..\TestData\SampleData.csv");
            Assert.IsNotNull(importData);
            Assert.That(importData.RowCount, Is.EqualTo(5));
        }

        [Test]
        public void GetDefaultColumnHeader_WithZeroColumnCount_ReturnsEmptyList()
        {
            var csvFileImportManager=new CSVFileImportManager();
            var columns = csvFileImportManager.GetDefaultColumnHeaders(0);
            Assert.IsNotNull(columns);
            Assert.AreEqual(0, columns.Count);
        }
        [Test]
        public void GetDefaultColumnHeader_WithNegativeColumnCount_ReturnsEmptyList()
        {
            var csvFileImportManager = new CSVFileImportManager();
            var columns = csvFileImportManager.GetDefaultColumnHeaders(-1);
            Assert.IsNotNull(columns);
            Assert.AreEqual(0, columns.Count);
        }

        [Test]
        public void GetDefaultColumnHeader_WithTwoColumns_ReturnsList()
        {
            var csvFileImportManager = new CSVFileImportManager();
            var columns = csvFileImportManager.GetDefaultColumnHeaders(2);
            Assert.IsNotNull(columns);
            Assert.AreEqual(2, columns.Count);
            var column1=columns[0];
            Assert.That("Column 1",Is.EqualTo(column1));
            var column2 = columns[1];
            Assert.That("Column 2", Is.EqualTo(column2));
        }
    }
}