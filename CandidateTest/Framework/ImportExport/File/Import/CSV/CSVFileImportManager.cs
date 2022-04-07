
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
        protected string seperator => ",";

        public ImportData ImportCSVFile(string filePath, bool isHeaderPresent = true)
        {
            ImportData data = new ImportData();

            if (IO.File.Exists(filePath))
            {
                var lines=IO.File.ReadAllLines(filePath);
                if (lines.Any())
                {
                    int lineIndex = 0;
                    var headerLine = lines[0].Split(seperator);
                    if (isHeaderPresent)
                    {
                        data.AddCoumnHeaders(headerLine);
                    }
                    else
                    {
                        data.AddCoumnHeaders(GetDefaultColumnHeaders(headerLine.Length).ToArray());
                    }
                    lineIndex++;
                    int rowIndex = 0;
                    for (int i=lineIndex; i<lines.Length; i++)
                    {
                        var line=lines[i];
                        string[] lineData = line.Split(seperator);

                        for (int j = 0; j < lineData.Length; j++)
                        {
                            data.AddRow(rowIndex, j, lineData[j], typeof(string));
                        }
                        rowIndex++;
                    }

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
