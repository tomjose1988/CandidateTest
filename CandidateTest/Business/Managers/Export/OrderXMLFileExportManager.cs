using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Interfaces;
using Framework.ImportExport.File.Export.XML;

namespace Business.Managers.Export
{
    public class OrderXMLFileExportManager :XMLFileExportManager, IOrderExportManager
    {
        public OrderXMLFileExportManager()
        {

        }

        public virtual bool ExportToFile(OrderCollection orders, string directoryPath, string fileName)
        {
            if (!IsDirectoryExists(directoryPath))
            {
                CreateDirectory(directoryPath);
            }
            var filePath = directoryPath + @"\" + fileName+".xml";
            base.ExportXml<OrderCollection>(orders,filePath);
            return System.IO.File.Exists(filePath);
        }
    }
}
