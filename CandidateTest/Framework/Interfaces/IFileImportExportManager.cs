using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IFileImportExportManager:IImportExportManager
    {
        FileFormat fileType { get; set; }
        string FilePath { get; set; }
        string DirectoryPath { get; set; }
        bool IsDirectoryExists(string directoryPath);
        bool IsFileExists(string filePath);
        bool CreateDirectory(string directoryPath);
        bool DeleteDirectory(string directoryPath,bool recursive);
        List<string> EnumerateFiles(string directoryPath);
        bool CreateFile(string filePath, bool overWrite = false);
        bool DeleteFile(string filePath);
        string GetFileName(string filePath);
        List<string> MoveFiles(List<string> sourceFilePaths, string outputDirectory);
    }
}
