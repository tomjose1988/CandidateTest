using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;

namespace Framework.Interfaces
{
    public interface ICSVFileImportManager:IFileImportManager
    {
        List<string> GetDefaultColumnHeaders(int columnCount);
        ImportData ImportCSVFile(string filePath,bool isHeaderPresent=true);
    }
}
