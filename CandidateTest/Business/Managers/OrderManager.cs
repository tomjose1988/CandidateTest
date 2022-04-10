using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Framework.ImportExport.File;
using Microsoft.Extensions.Configuration;

namespace Business.Managers
{
    public class OrderManager: FileImportExportManager,IManager
    {
        /// <summary>
        /// This variable is used to determine when to archive the input folder.
        /// </summary>
        private int _archiveThreshold=1;
        private string _inputDirectory;
        private string _outputDirectory;
        ///this directory would help in keeping the performance as the files in input directory grows.
        ///the processed input files would be moved to this directory. No output archive is added because
        ///of the assumption that whatever system that consumes our output files will take care of the cleaning up of
        ///that folder.
        private string _inputArchiveDirectory;

        //private FileSystemWatcher _watcher;
        private IOrderImportManager _orderImportManager;
        private IOrderExportManager _orderExportManager;
        private IConfiguration _configuration;
        private object _lockObj=new object();
        private bool _continueProcessing=false;
        private List<string> _processedInputFiles;
        private List<string> _processedOutputFiles;
        private Queue<string> _currentlyQueuedFiles;

        public OrderManager(IOrderImportManager importManager,IOrderExportManager exportManager,IConfiguration configuration)
        {
            this._outputDirectory=string.Empty;
            this._orderImportManager = importManager;
            this._orderExportManager = exportManager;
            this._configuration = configuration;
            this._inputDirectory = this._configuration.GetSection("inputDirectory").Value;
            this._outputDirectory=this._configuration.GetSection("outputDirectory").Value;
            this._inputArchiveDirectory= this._configuration.GetSection("inputArchiveDirectory").Value;
            var threshold= this._configuration.GetSection("inputArchiveThreshold").Value;
            int.TryParse(threshold, out this._archiveThreshold);
            //this._watcher = new FileSystemWatcher(this._inputDirectory);
            //this._watcher.Created += this.OnCreated;
            this._currentlyQueuedFiles = new Queue<string>();
            this._processedInputFiles = new List<string>();
            this._processedOutputFiles = new List<string>();
            
        }

        /// <summary>
        /// Used to end processing
        /// </summary>
        public void EndProcessing()
        {
            try
            {

                this._continueProcessing = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public virtual async void StartProcessing()
        {
            try
            {
                this._continueProcessing=true;

                await Task.Run(() =>
                {
                    while (this._continueProcessing)
                    {
 
                        FillQueue();
                        ProcessFiles();
                        CheckAndArchive();

                    }                    
                    
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FillQueue()
        {
            var files = this._orderImportManager.EnumerateFiles(this._inputDirectory);
            foreach (var file in files)
            {
                if (!this._currentlyQueuedFiles.Contains(file) && !this._processedInputFiles.Contains(file))
                {
                    this._currentlyQueuedFiles.Enqueue(file);
                }
            }
        }

        private void CheckAndArchive()
        {
            if (this._processedInputFiles.Count == this._archiveThreshold)
            {
                if (!this.IsDirectoryExists(this._inputArchiveDirectory))
                {
                    this.CreateDirectory(this._inputArchiveDirectory);
                }
                var movedFiles=this.MoveFiles(this._processedInputFiles, this._inputArchiveDirectory);
                foreach (var file in movedFiles)
                {
                    this._processedInputFiles.Remove(file);
                }
            }
        }

        private void ProcessFiles()
        {
            try
            {
                while (this._currentlyQueuedFiles.Any() && this._continueProcessing)
                {
                    var filePath = this._currentlyQueuedFiles.Peek();
                    var importedData = this._orderImportManager.ImportCSVFile(filePath);
                    var formattedData = this._orderImportManager.FormatData(importedData);
                    var inputFileName = this._orderExportManager.GetFileName(filePath);
                    var outputFileName = inputFileName + "_Output_" + DateTime.Now.Ticks.ToString();
                    var result = this._orderExportManager.ExportToFile(formattedData, this._outputDirectory, outputFileName);
                    this._currentlyQueuedFiles.Dequeue();
                    if (result)
                    {
                        this._processedInputFiles.Add(filePath);
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
