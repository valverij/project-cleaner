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
        public delegate void IncrementHandler(int currentValue);

        private IncrementHandler _onIncrement;

        public CleanerStatusTracker(IncrementHandler onIncrement)
        {
            _onIncrement = onIncrement;
        }

        public int Complete { get; set; } = 0;

        public Dictionary<string, string> FailedFiles { get; } = new Dictionary<string, string>();

        public void Advance()
        {
            lock(_lock)
            {
                Complete++;
                if (_onIncrement != null)
                    _onIncrement(Complete);
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
