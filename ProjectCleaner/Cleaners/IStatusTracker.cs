using System;
using System.Collections.Generic;

namespace ProjectCleaner.Cleaners
{
    public delegate void IncrementHandler(int currentValue);

    public interface IStatusTracker
    {
        IncrementHandler OnIncrement { get; set; }
        Dictionary<string, string> FailedFiles { get; }
        void Advance();
        void TrackException(string filePath, Exception ex);
    }
}
