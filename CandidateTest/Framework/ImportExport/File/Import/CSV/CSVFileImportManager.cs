
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO=System.IO;
using Framework.Data;
using Framework.Interfaces;

namespace Framework.ImportExport.File.Import.CSV
{
    public class CSVFileImportManager :FileImportManager, ICSVFileImportManager
    {
        protected string seperator => ";";

        public ImportData ImportCSVFile(string filePath, bool isHeaderPresent = true)
        {
            ImportData data = new ImportData();

            if (IO.File.Exists(filePath))
            {
                var lines=IO.File.ReadAllLines(filePath);
                int lineIndex = 0;
                foreach (var line in lines)
                {
                    string[] lineData = line.Split(seperator);
                    if (lineIndex == 0)
                    {
                        if (isHeaderPresent)
                        {
                            data.AddCoumnHeaders(lineData);
                        }
                        else
                        {
                            data.AddCoumnHeaders(GetDefaultColumnHeaders(lineData.Length).ToArray());
                        }
                    }
                    else
                    {
                        for(int i = 0; i < lineData.Length; i++)
                        {
                            data.AddRow(lineIndex,i,lineData[i],typeof(string));
                        }
                    }
                    lineIndex++;
                }
            }
            return data;
        }

        public List<string> GetDefaultColumnHeaders(int columnCount)
        {
            List<string> columnHeaders = new List<string>();
            var baseName = "Column ";
            for (int i = 0; i < columnCount; i++)
            {
                var columnName=baseName+(i+1).ToString();
                columnHeaders.Add(columnName);
            }
            return columnHeaders;
        }

    }
}
