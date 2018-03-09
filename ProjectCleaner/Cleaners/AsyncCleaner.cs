using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    public class AsyncCleaner : IAsyncCleaner
    {
        private IStatusTracker _statusTracker;

        private readonly List<string> _foldersToRemove = new List<string>() { "bin", "proj" };

        public AsyncCleaner(RecycleOption recycleOption, IStatusTracker statusTracker)
        {
            if (statusTracker == null)
                throw new ArgumentNullException(nameof(statusTracker));

            RecycleOption = recycleOption;
            _statusTracker = statusTracker;
        }

        public RecycleOption RecycleOption { get; set; }

        public async Task CleanAsync(string filePath, CleanerOptions options = CleanerOptions.None)
        {
            var tasks = new List<Task>();

            //if ((options & CleanerOptions.ClearNugetPackages != CleanerOptions.None) 

            tasks.Add(CleanRecursiveAsync(filePath));

            if ((options & CleanerOptions.ClearTemporaryFiles) != CleanerOptions.None) tasks.Add(CleanTemporayFilesAsync());
            if ((options & CleanerOptions.ClearAspNetFiles) != CleanerOptions.None) tasks.Add(CleanAspNetFilesAsync());

            await Task.WhenAll(tasks);
        }

        private async Task CleanAspNetFilesAsync()
        {
            var windowsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows, Environment.SpecialFolderOption.None);
            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.None);
            var tasks = new List<Task>();

            var windowsPaths = new[]
            {
                windowsFolder + @"\Microsoft.NET\Framework",
                windowsFolder + @"\Microsoft.NET\Framework64",
            };

            var otherPaths = new[]
            {
                localAppDataFolder + @"\Temp\Temporary ASP.NET Files"
            };

            foreach (var filePath in windowsPaths)
            {
                if (Directory.Exists(filePath))
                {
                    var dotNetDirectories = Directory.GetDirectories(filePath, "v?.*", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (var childPath in dotNetDirectories)
                    {
                        var tempPath = childPath + @"\Temporary ASP.NET Files";
                        if (Directory.Exists(tempPath))
                        {
                            tasks.Add(DeleteAllDirectoriesAsync(tempPath));
                            tasks.Add(DeleteAllFilesAsync(tempPath));
                        }
                    }
                }
            }

            foreach (var filePath in otherPaths)
            {
                if (Directory.Exists(filePath))
                {
                    tasks.Add(DeleteAllDirectoriesAsync(filePath));
                    tasks.Add(DeleteAllFilesAsync(filePath));
                }
            }

            await Task.WhenAll(tasks);
        }

        private async Task CleanTemporayFilesAsync()
        {
            var filePath = Path.GetTempPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                await Task.WhenAll(DeleteAllDirectoriesAsync(filePath), DeleteAllFilesAsync(filePath));
            }
        }

        private async Task CleanRecursiveAsync(string filePath)
        {
            var bin = filePath + @"\bin";
            var obj = filePath + @"\obj";

            if (Directory.Exists(bin) || Directory.Exists(obj))
            {
                await Task.WhenAll(DeleteDirectoryAsync(bin), DeleteDirectoryAsync(obj));
            }
            else
            {
                foreach (var folder in Directory.GetDirectories(filePath))
                {
                    await CleanRecursiveAsync(folder);
                }
            }
        }

        private async Task DeleteAllFilesAsync(string filePath)
        {
            foreach (var fileName in Directory.GetFiles(filePath))
            {
                var fullPath = Path.Combine(filePath, fileName);
                await DeleteFileAsync(fullPath);
            }
        }

        private async Task DeleteAllDirectoriesAsync(string filePath)
        {
            foreach (var childPath in System.IO.Directory.GetDirectories(filePath))
            {
                await DeleteDirectoryAsync(childPath);
            }
        }

        private async Task DeleteFileAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    await Task.Run(() => FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption, UICancelOption.DoNothing));
            }
            catch (IOException ex) // thrown when a file is in use
            {
                _statusTracker.TrackException(filePath, ex);
            }
            finally
            {
                _statusTracker.Advance();
            }
        }

        private async Task DeleteDirectoryAsync(string filePath)
        {
            try
            {
                if (Directory.Exists(filePath))
                    await Task.Run(() => FileSystem.DeleteDirectory(filePath, UIOption.OnlyErrorDialogs, RecycleOption, UICancelOption.DoNothing));
            }
            catch (IOException ex) // thrown when a folder is in use
            {
                _statusTracker.TrackException(filePath, ex);
            }
            finally
            {
                _statusTracker.Advance();
            }
        }
    }
}
