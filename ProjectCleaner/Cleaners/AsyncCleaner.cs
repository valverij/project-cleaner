using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCleaner.Cleaners
{
    public class AsyncCleaner : IAsyncCleaner
    {
        private IStatusTracker _statusTracker;

        public AsyncCleaner(IStatusTracker statusTracker)
        {
            _statusTracker = statusTracker ?? throw new ArgumentNullException(nameof(statusTracker));
        }

        public async Task CleanAsync(string filePath, CleanerOptions options = CleanerOptions.None, RecycleOption recycleOption = RecycleOption.SendToRecycleBin)
        {
            var tasks = new List<Task>()
            {
                CleanRecursiveAsync(filePath, options, recycleOption)
            };

            if ((options & CleanerOptions.ClearTemporaryFiles) != CleanerOptions.None) tasks.Add(CleanTemporayFilesAsync(recycleOption));
            if ((options & CleanerOptions.ClearAspNetFiles) != CleanerOptions.None) tasks.Add(CleanAspNetFilesAsync(recycleOption));

            await Task.WhenAll(tasks);
        }

        private async Task CleanAspNetFilesAsync(RecycleOption recycleOption)
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
                            tasks.Add(DeleteAllDirectoriesAsync(tempPath, recycleOption));
                            tasks.Add(DeleteAllFilesAsync(tempPath, recycleOption));
                        }
                    }
                }
            }

            foreach (var filePath in otherPaths)
            {
                if (Directory.Exists(filePath))
                {
                    tasks.Add(DeleteAllDirectoriesAsync(filePath, recycleOption));
                    tasks.Add(DeleteAllFilesAsync(filePath, recycleOption));
                }
            }

            await Task.WhenAll(tasks);
        }

        private async Task CleanTemporayFilesAsync(RecycleOption recycleOption)
        {
            var filePath = Path.GetTempPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                await Task.WhenAll(DeleteAllDirectoriesAsync(filePath, recycleOption), DeleteAllFilesAsync(filePath, recycleOption));
            }
        }

        private async Task CleanRecursiveAsync(string filePath, CleanerOptions options, RecycleOption recycleOption)
        {
            var bin = Path.Combine(filePath, "bin");
            var obj = Path.Combine(filePath, "obj");
            var packages = (options & CleanerOptions.ClearNugetPackages) != CleanerOptions.None ? Path.Combine(filePath, "packages") : string.Empty;
            var nodeModules = (options & CleanerOptions.ClearNodeModules) != CleanerOptions.None ? Path.Combine(filePath, "node_modules") : string.Empty;

            var tasks = new List<Task>()
            {
                DeleteDirectoryIfExistsAsync(obj, recycleOption),
                DeleteDirectoryIfExistsAsync(packages, recycleOption),
                DeleteDirectoryIfExistsAsync(nodeModules, recycleOption)
            };

            // deleting the roslyn folder can cause some weird issues
            if (Directory.Exists(Path.Combine(bin, "roslyn")))
                tasks.Add(DeleteAllFilesAsync(bin, recycleOption));
            else
                tasks.Add(DeleteDirectoryIfExistsAsync(bin, recycleOption));

            await Task.WhenAll(tasks);

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                // a lot of npm packages have bin & obj folders, so ignore it
                // also, deleting the roslyn folder can cause issues
                if (folder.EndsWith("node_modules") || folder.EndsWith("roslyn"))
                    continue;

                await CleanRecursiveAsync(folder, options, recycleOption);
            }

            //var tasks = new List<Task>();

            //if (!string.IsNullOrEmpty(bin) && Directory.Exists(bin)) tasks.Add(DeleteDirectoryAsync(bin, recycleOption));
            //if (!string.IsNullOrEmpty(obj) && Directory.Exists(obj)) tasks.Add(DeleteDirectoryAsync(obj, recycleOption));
            //if (!string.IsNullOrEmpty(packages) && Directory.Exists(packages)) tasks.Add(DeleteDirectoryAsync(packages, recycleOption));
            //if (!string.IsNullOrEmpty(nodeModules) && Directory.Exists(nodeModules)) tasks.Add(DeleteDirectoryAsync(nodeModules, recycleOption));

            //if (tasks.Any())
            //{
            //    await Task.WhenAll(tasks);
            //}
            //else
            //{
            //    foreach (var folder in Directory.GetDirectories(filePath))
            //    {
            //        await CleanRecursiveAsync(folder, options, recycleOption);
            //    }
            //}
        }

        private async Task DeleteDirectoryIfExistsAsync(string directory, RecycleOption recycleOption)
        {
            if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
                await DeleteDirectoryAsync(directory, recycleOption);
        }

        private async Task DeleteAllFilesAsync(string filePath, RecycleOption recycleOption)
        {
            foreach (var fileName in Directory.GetFiles(filePath))
            {
                var fullPath = Path.Combine(filePath, fileName);
                await DeleteFileAsync(fullPath, recycleOption);
            }
        }

        private async Task DeleteAllDirectoriesAsync(string filePath, RecycleOption recycleOption)
        {
            foreach (var childPath in Directory.GetDirectories(filePath))
            {
                await DeleteDirectoryAsync(childPath, recycleOption);
            }
        }

        private async Task DeleteFileAsync(string filePath, RecycleOption recycleOption)
        {
            try
            {
                if (File.Exists(filePath))
                    await Task.Run(() => FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, recycleOption, UICancelOption.DoNothing));
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

        private async Task DeleteDirectoryAsync(string filePath, RecycleOption recycleOption)
        {
            try
            {
                if (Directory.Exists(filePath))
                    await Task.Run(() => FileSystem.DeleteDirectory(filePath, UIOption.OnlyErrorDialogs, recycleOption, UICancelOption.DoNothing));
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
