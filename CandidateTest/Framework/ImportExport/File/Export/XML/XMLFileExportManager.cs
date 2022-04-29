
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Framework.Interfaces;

namespace Framework.ImportExport.File.Export.XML
{
    public class XMLFileExportManager:FileExportManager,IFileExportManager
    {
        protected void ExportXml<T>(T data, string filePath)
        {
            
        }

        public override string ExportToFile<T>(T data, string directoryPath, string fileNameWithOutExtension)
        {
            string outputFilePath = string.Empty;
            try
            {
                outputFilePath = this.GetCompleteFilePath(directoryPath, fileNameWithOutExtension, ".xml");
                var serializer = new XmlSerializer(typeof(T));
                using (var writer = new StreamWriter(outputFilePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return outputFilePath;
        }
    }
}
