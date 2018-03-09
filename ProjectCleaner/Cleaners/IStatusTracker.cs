using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    public interface IStatusTracker
    {
        Dictionary<string, string> FailedFiles { get; }
        void Advance();
        void TrackException(string filePath, Exception ex);
    }
}
