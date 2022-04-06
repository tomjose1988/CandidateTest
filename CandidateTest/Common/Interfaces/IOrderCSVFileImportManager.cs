using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Framework.Data;
using Framework.Interfaces;

namespace Common.Interfaces
{
    public interface IOrderCSVFileImportManager:ICSVFileImportManager
    {
        OrderCollection FormatData(ImportData data);
    }
}
