using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Interfaces;

namespace Framework.ImportExport
{
    public abstract class ImportExportManager : IImportExportManager
    {

        public abstract ImportExportFormat format { get; }

        public virtual ImportExportType ImportExportType => throw new NotImplementedException();
    }
}
