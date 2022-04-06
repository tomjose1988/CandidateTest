﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IImportExportManager
    {
        ImportExportType ImportExportType { get; }
        ImportExportFormat format { get; }
    }
}
