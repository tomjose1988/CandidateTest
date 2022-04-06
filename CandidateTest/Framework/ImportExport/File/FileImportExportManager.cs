
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Interfaces;

namespace Framework.ImportExport.File
{
    public class FileImportExportManager : ImportExportManager,IFileImportExportManager
    {
        public override ImportExportFormat format => ImportExportFormat.File;

        public virtual FileFormat fileType { get; set; }
        public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DirectoryPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CreateDirectory(string directoryPath)
        {
            var dirInfo=Directory.CreateDirectory(directoryPath);
            return dirInfo.Exists;
        }

        public bool DeleteDirectory(string directoryPath, bool recursive)
        {
            Directory.Delete(directoryPath, recursive);
            return !Directory.Exists(directoryPath);
        }

        public IEnumerable<string> EnumerateFiles(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath);
        }

        public bool CreateFile(string filePath,bool overWrite=false)
        {
            if (overWrite)
            {
                System.IO.File.Create(filePath);
            }
            else
            {
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath);

                }
            }
            return System.IO.File.Exists(filePath);
        }

        public bool DeleteFile(string filePath)
        {
            System.IO.File.Delete(filePath);
            return System.IO.File.Exists(filePath);
        }
    }
}
