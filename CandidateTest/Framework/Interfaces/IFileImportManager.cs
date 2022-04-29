using Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IFileImportManager:IFileImportExportManager
    {
        List<string> GetDefaultColumnHeaders(int columnCount);
        ImportData ImportFile(string filePath, bool isHeaderPresent = true);
    }
}
