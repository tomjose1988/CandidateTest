using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public class ImportData
    {
        private Dictionary<int, string> headers;
        private Dictionary<int,Record> records;

        public ImportData()
        {
            headers=new Dictionary<int, string>();
            records = new Dictionary<int, Record>();
        }

        public int RowCount { get { return records.Count; } }

        public void AddCoumnHeader(int columnIndex,string columnName)
        {
            headers.Add(columnIndex,columnName);
        }

        public void AddRow(int rowIndex,int columnIndex,object value,Type type)
        {
            var columnName=headers[columnIndex];
            var property=new Property() { Name = columnName, Value = value ,Type=type};
            Record recordData=null;
            if(!records.TryGetValue(rowIndex, out recordData))
            {
                recordData = new Record(rowIndex);
            }
            recordData.Add(property);
        }

        public void AddCoumnHeaders(string[] columnNames)
        {
            if (columnNames != null && columnNames.Length > 0)
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    AddCoumnHeader(i, columnNames[i]);
                }
            }

        }

        public Record GetRecord(int rowIndex)
        {
            return records[rowIndex];
        }

        

    }
}
