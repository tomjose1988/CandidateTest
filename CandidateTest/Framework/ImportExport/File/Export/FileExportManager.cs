
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Interfaces;

namespace Framework.ImportExport.File.Export
{
    public abstract class FileExportManager :FileImportExportManager, IFileExportManager
    {
        protected virtual void ExportFile(byte[] fileData, string directoryPath, string fileName, string extension)
        {
            try
            {
                var filePath = directoryPath + @"\" + fileName + "." + extension;
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(fileData, 0, fileData.Length);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public abstract string ExportToFile<T>(T data, string directoryPath, string fileNameWithOutExtension);
    }
}
