using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IFileExportManager
    {
        void ExportFile(byte[] fileData,string directoryPath,string fileName,string extension);
    }
}
