using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Framework.Interfaces;
using Framework.ImportExport.File.Export;
using Framework.Data;

namespace Business.Interfaces
{
    public interface IOrderExportManager
    {
        bool ExportOrders(OrderCollection orders,ItemKey key);
    }
}
