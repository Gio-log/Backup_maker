using System;
using System.Collections.Generic;
using System.Text;

namespace Backup_Maker.Models
{
    internal interface IFileEntry
    {
        string Name { get; }
        string Extension { get; }
    }
}
