using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Framework.Data;
using Framework.Interfaces;

namespace Business.Interfaces
{
    public interface IOrderImportManager:ICSVFileImportManager
    {
        OrderCollection FormatData(ImportData data);
    }
}
