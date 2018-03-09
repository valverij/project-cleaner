using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    interface IAsyncCleaner
    {
        RecycleOption RecycleOption { get; set; }
        Task CleanAsync(string filePath, CleanerOptions options = CleanerOptions.None);
    }
}
