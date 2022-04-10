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
    public class OrderManager:IOrderManager
    {
        private string _outputDirectory;
        private FileSystemWatcher _watcher;
        private IOrderImportManager _orderImportManager;
        private IOrderExportManager _orderExportManager;
        private List<string> processedInputFiles;
        private bool continueProcessing;
        private object lockObj=new object();
        public OrderManager(IOrderImportManager importManager,IOrderExportManager exportManager)
        {
            this._outputDirectory=string.Empty;
            this.continueProcessing = false;
            this.processedInputFiles = new List<string>();
            this._orderImportManager = importManager;
            this._orderExportManager = exportManager;
        }

        public void EndProcessing()
        {
            try
            {
                lock (lockObj)
                {
                    this.continueProcessing = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual async void StartProcessing(string inputDirectory,string outputDirectory)
        {
            try
            {
                this._outputDirectory= outputDirectory;
                this._watcher = new FileSystemWatcher(inputDirectory);
                this._watcher.Created += this.OnCreated;
                this._watcher.EnableRaisingEvents = true;
                var files=this._orderImportManager.EnumerateFiles(inputDirectory);
                ProcessFiles(files.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string ParseInputFileName(string filePath)
        {
            var fileNameWithExtension= Path.GetFileName(filePath);
            fileNameWithExtension.Replace(".", "_");
            var fileName= fileNameWithExtension.Replace(".", "_");
            return fileName;
        }

        private void OnCreated(object source,FileSystemEventArgs e)
        {
            ProcessFile(e.FullPath);
        }

        private void ProcessFiles(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                ProcessFile(filePath);
            }
        }

        private void ProcessFile(string filePath)
        {
            try
            {
                lock (this.lockObj)
                {
                    var importedData = this._orderImportManager.ImportCSVFile(filePath);
                    var formattedData = this._orderImportManager.FormatData(importedData);
                    var inputFileName = ParseInputFileName(filePath);
                    var outputFileName = inputFileName + "_Output_" + DateTime.Now.Ticks.ToString();
                    this._orderExportManager.ExportToFile(formattedData, this._outputDirectory, outputFileName);
                    this.processedInputFiles.Add(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
