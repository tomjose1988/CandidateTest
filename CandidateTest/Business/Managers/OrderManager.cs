﻿using System;
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
        private string outputDirectory;
        private FileSystemWatcher watcher;
        private IOrderImportManager orderImportManager;
        private IOrderExportManager orderExportManager;
        private object lockObj=new object();
        private bool continueProcessing=false;

        public OrderManager(IOrderImportManager importManager,IOrderExportManager exportManager)
        {
            this.outputDirectory=string.Empty;
            this.orderImportManager = importManager;
            this.orderExportManager = exportManager;
        }

        /// <summary>
        /// Used to end processing
        /// </summary>
        public void EndProcessing()
        {
            try
            {
                lock (lockObj)
                {
                    this.continueProcessing = false;
                    if(this.watcher!=null)
                        this.watcher.Dispose();
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
                this.outputDirectory= outputDirectory;
                this.continueProcessing=true;
                this.watcher = new FileSystemWatcher(inputDirectory);
                this.watcher.Created += this.OnCreated;
                this.watcher.EnableRaisingEvents = true;
                var files=this.orderImportManager.EnumerateFiles(inputDirectory);
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
                    if (this.continueProcessing)
                    {
                        var importedData = this.orderImportManager.ImportCSVFile(filePath);
                        var formattedData = this.orderImportManager.FormatData(importedData);
                        var inputFileName = ParseInputFileName(filePath);
                        var outputFileName = inputFileName + "_Output_" + DateTime.Now.Ticks.ToString();
                        this.orderExportManager.ExportToFile(formattedData, this.outputDirectory, outputFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
