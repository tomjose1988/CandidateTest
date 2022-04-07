using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IXMLFileExportManager:IFileExportManager
    {
        void ExportXml<T>(T data,string filePath);
    }
}
