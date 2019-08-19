using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    [Flags]
    public enum CleanerOptions
    {
        None = 0x00,
        ClearTemporaryFiles = 0x01,
        ClearAspNetFiles = 0x02,
        ClearNugetPackages = 0x04,
        ClearNodeModules = 0x08,
    }
}
