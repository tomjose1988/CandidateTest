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
        private bool _continueProcessing=false;
        private IOrderImportManager _orderImportManager;
        private IOrderExportManager _orderExportManager;


        public OrderManager(IOrderImportManager importManager,IOrderExportManager exportManager)
        {
            this._orderImportManager = importManager;
            this._orderExportManager = exportManager;           
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
                this._continueProcessing = true;
                await Task.Run(() => {
                    ProcessFiles();
                });               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ProcessFiles()
        {
            try
            {
                while (this._continueProcessing)
                {
                    var itemKeys=this._orderImportManager.GetImportItemKeys();
                    if (itemKeys != null)
                    {
                        foreach (var item in itemKeys)
                        {
                            var importedData=this._orderImportManager.ImportOrderData(item);
                            if (importedData != null)
                            {
                                var formattedData = this._orderImportManager.FormatOrderData(importedData);
                                if(formattedData != null)
                                {
                                    var result=this._orderExportManager.ExportOrders(formattedData, item);
                                }
                            }
                            this._orderImportManager.AddProcessed(item);
                        }
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
