
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Framework.Interfaces;

namespace Framework.ImportExport.File.Export.XML
{
    public class XMLFileExportManager:FileExportManager,IXMLFileExportManager
    {
        public void ExportXml<T>(T data, string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
