using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    public interface IAsyncCleaner
    {
        Task CleanAsync(string filePath, CleanerOptions options = CleanerOptions.None, RecycleOption recycleOption = RecycleOption.SendToRecycleBin);
    }
}
