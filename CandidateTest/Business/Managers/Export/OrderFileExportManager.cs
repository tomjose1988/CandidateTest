using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Framework.Data;
using Framework.ImportExport.File.Export.XML;
using Framework.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Business.Managers.Export
{
    public class OrderFileExportManager : IOrderExportManager
    {
        private string _directoryPath = string.Empty;
        private IFileExportManager _fileExportManager;
        public OrderFileExportManager(IFileExportManager fileExportManager, IConfiguration configuration)
        {
            this._fileExportManager = fileExportManager;
            this._directoryPath= configuration.GetSection("outputDirectory").Value;
        }

        public bool ExportOrders(OrderCollection orders,ItemKey key)
        {
            var fileNameWithOutExtension = this._fileExportManager.GetFileName(key.Value);
            return ExportToFile(orders, fileNameWithOutExtension);
        }

        private bool ExportToFile(OrderCollection orders,string fileNameWithOutExtension)
        {
            if (!this._fileExportManager.IsDirectoryExists(this._directoryPath))
            {
                this._fileExportManager.CreateDirectory(this._directoryPath);
            }
            var filePath=this._fileExportManager.ExportToFile<OrderCollection>(orders, this._directoryPath, fileNameWithOutExtension);
            return System.IO.File.Exists(filePath);
        }
    }
}
