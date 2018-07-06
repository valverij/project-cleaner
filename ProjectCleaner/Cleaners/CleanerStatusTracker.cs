using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    public class CleanerStatusTracker : IStatusTracker
    {
        private object _lock = new object();

        public IncrementHandler OnIncrement { get; set; }

        public int Complete { get; set; } = 0;

        public Dictionary<string, string> FailedFiles { get; } = new Dictionary<string, string>();

        public void Advance()
        {
            lock(_lock)
            {
                Complete++;
                OnIncrement?.Invoke(Complete);
            }
        }

        public void TrackException(string filePath, Exception ex)
        {
            lock (_lock)
            {
                FailedFiles.Add(filePath, ex.Message);
            }
        }
    }
}
