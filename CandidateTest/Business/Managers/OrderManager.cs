using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;

namespace Business.Managers
{
    public class OrderManager
    {
        private IOrderCSVFileImportManager orderCSVFileImportManager;
        private IOrderXmlFileExportManager orderCSVFileExportManager;
        public OrderManager(IOrderCSVFileImportManager importManager,IOrderXmlFileExportManager exportManager)
        {
            this.orderCSVFileImportManager = importManager;
            this.orderCSVFileExportManager = exportManager;
        }

        public virtual void StartExport(string inputDirectory,string outputDirectory)
        {
            var files=orderCSVFileImportManager.EnumerateFiles(inputDirectory);
            foreach (var file in files)
            {
                var importedData= orderCSVFileImportManager.ImportCSVFile(file);
                var formattedData= orderCSVFileImportManager.FormatData(importedData);
                var inputFileName=ParseInputFileName(file);
                var outputFileName = inputFileName + "_Output_" + DateTime.Now.Ticks.ToString();
                orderCSVFileExportManager.ExportXmlData(formattedData, outputDirectory, outputFileName);
            }
        }

        protected virtual string ParseInputFileName(string filePath)
        {
            var fileNameWithExtension= Path.GetFileName(filePath);
            fileNameWithExtension.Replace(".", "_");
            var fileName= fileNameWithExtension.Replace(".", "_");
            return fileName;
        }
    }
}
