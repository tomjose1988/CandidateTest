
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Interfaces;

namespace Framework.ImportExport.File.Export
{
    public class FileExportManager :FileImportExportManager, IFileExportManager
    {
        public void ExportFile(byte[] fileData, string directoryPath, string fileName, string extension)
        {
            throw new NotImplementedException();
        }
    }
}
