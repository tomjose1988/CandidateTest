using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Framework.Data;
using Framework.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Business.Interfaces
{
    public interface IOrderImportManager
    {
        List<ItemKey> GetImportItemKeys();
        ImportData ImportOrderData(ItemKey key);
        OrderCollection FormatOrderData(ImportData data);
        void AddProcessed(ItemKey key);
    }
}
