using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Framework.Interfaces;
using Framework.ImportExport.File.Export;

namespace Business.Interfaces
{
    public interface IOrderExportManager:IXMLFileExportManager
    {
        bool ExportToFile(OrderCollection orders, string directoryPath,string fileName);
    }
}
