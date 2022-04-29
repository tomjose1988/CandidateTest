
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

        public List<string> EnumerateFiles(string directoryPath)
        {
            List<string> files = new List<string>();

            var result= Directory.EnumerateFiles(directoryPath);
            if (result != null)
            {
                foreach (var item in result)
                {
                    files.Add(item);
                }
            }
            return files;
        }
        public bool IsDirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        public bool IsFileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
        public bool CreateFile(string filePath,bool overWrite=false)
        {
            if (overWrite)
            {
                System.IO.File.Create(filePath);
            }
            else
            {
                if (!IsFileExists(filePath))
                {
                    System.IO.File.Create(filePath);

                }
            }
            return System.IO.File.Exists(filePath);
        }

        public bool DeleteFile(string filePath)
        {
            System.IO.File.Delete(filePath);
            return !IsFileExists(filePath);
        }

        public string GetFileName(string filePath)
        {
            var fileNameWithExtension = Path.GetFileName(filePath);
            fileNameWithExtension.Replace(".", "_");
            var fileName = fileNameWithExtension.Replace(".", "_");
            return fileName;
        }

        /// <summary>
        /// returns a list of moved sourceFilePaths.
        /// </summary>
        /// <param name="inputFilePaths"></param>
        /// <param name="outputDirectory"></param>
        /// <returns></returns>
        public List<string> MoveFiles(List<string> sourceFilePaths, string outputDirectory)
        {
            List<string> movedFiles = new List<string>();
            foreach (var inputFilePath in sourceFilePaths)
            {
                if (IsFileExists(inputFilePath))
                {
                    var fileNameWithExtension= Path.GetFileName(inputFilePath);
                    var outputFilePath=Path.Combine(outputDirectory, fileNameWithExtension);
                    try
                    {
                        System.IO.File.Move(inputFilePath, outputFilePath);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    if (!IsFileExists(inputFilePath))
                    {
                        movedFiles.Add(inputFilePath);
                    }
                }
            }
            return movedFiles;
        }

        protected string GetCompleteFilePath(string directoryPath,string fileNameWithOutExtension,string extension)
        {
            var completePath= Path.Combine(directoryPath, fileNameWithOutExtension)+ extension;
            return completePath;
        }
    }
}
