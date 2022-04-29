
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Framework.Interfaces;

namespace Framework.ImportExport.File.Import
{
    public abstract class FileImportManager:FileImportExportManager,IFileImportManager
    {       


        public FileImportManager()
        {

        }


        public List<string> GetDefaultColumnHeaders(int columnCount)
        {
            List<string> columnHeaders = new List<string>();
            var baseName = "Column ";
            for (int i = 0; i < columnCount; i++)
            {
                var columnName = baseName + (i + 1).ToString();
                columnHeaders.Add(columnName);
            }
            return columnHeaders;
        }

        public abstract ImportData ImportFile(string filePath, bool isHeaderPresent = true);

    }
}
