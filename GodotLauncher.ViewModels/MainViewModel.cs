using GodotLauncher.ViewModels.Github;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVM.Generator.Attributes;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows.Markup;

namespace GodotLauncher.ViewModels
{
    [ContentProperty(nameof(GithubSources))]
    public partial class MainViewModel : IAddChild
    {
        [AutoNotify] DataViewModel data;
        [AutoNotify] Theme theme;
        [AutoNotify] ObservableCollection<GithubSource> githubSources;
        [AutoNotify] string personalAccessToken = "Test";

        Dictionary<ReleaseAssetViewModel, string> downloadLocations = new Dictionary<ReleaseAssetViewModel, string>();
        public MainViewModel()
        {
            data = new DataViewModel();
            theme = new Theme();
            githubSources = new ObservableCollection<GithubSource>();
        }

        [AutoCommand]
        public void Load()
        {
            // Initialize if null
            if (Data == null)
                Data = new DataViewModel();
            if (Theme == null)
                Theme = new Theme();

            Data.Load();
            Theme.Load();
            if (GithubSources == null) return;
            foreach (var source in GithubSources)
                source.Load();
        }

        [AutoCommand]
        public void Save()
        {
            Data?.Save();
            Theme?.Save();
            if (GithubSources != null)
            {
                foreach (var source in GithubSources)
                    source.Save();
            }
        }

        [AutoCommand]
        public void LoadGithubSources()
        {
            foreach (var source in GithubSources)
                source.LoadRepository();
        }

        [AutoCommand]
        public async void Download(ReleaseAssetViewModel asset)
        {
            using var dialog = new CommonSaveFileDialog
            {
                AddToMostRecentlyUsedList = true,
                RestoreDirectory = true,
                DefaultFileName = asset.Name,
            };
            dialog.Filters.Add(new CommonFileDialogFilter("Zip", ".zip"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var fileName = dialog.FileName;
                downloadLocations[asset] = fileName;
                var downloadUrl = asset.DownloadUrl;
                using var client = new HttpClient();
                using var message = new HttpRequestMessage(HttpMethod.Get, new Uri(downloadUrl));
                using var response = await client.SendAsync(message);
                using var responseStream = response.Content.ReadAsStream();
                asset.Release.IsDownloading = true;
                var bytes = await response.Content.ReadAsByteArrayAsync();
                asset.Release.IsDownloading = false;
                File.WriteAllBytes(fileName, bytes);
            }
        }

        [AutoCommand]
        public void Unpack(ReleaseAssetViewModel asset)
        {
            if (string.IsNullOrEmpty(Data.EnginesRootDirectory)) return;

            var fileName = downloadLocations[asset];
            var isZip = ZipArchive.IsZipFile(fileName);
            if (!isZip) return;
            var zipFile = ZipArchive.Open(fileName);
            var destination = Path.Combine(Data.EnginesRootDirectory, Path.GetFileNameWithoutExtension(fileName));
            zipFile.ExtractToDirectory(destination);

            // After unpacking, scan for new engines
            ScanForEngines();
        }

        [AutoCommand]
        public void BrowseForProjectFolder()
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                RestoreDirectory = true,
                AddToMostRecentlyUsedList = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Data.ProjectsRootDirectory = dialog.FileName;
                Save();
            }
        }


        [AutoCommand]
        public void BrowseForEngineFolder()
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                RestoreDirectory = true,
                AddToMostRecentlyUsedList = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Data.EnginesRootDirectory = dialog.FileName;
                Save();
            }
        }

        [AutoCommand]
        public void ScanForProjects()
        {
            if (string.IsNullOrEmpty(Data?.ProjectsRootDirectory)) return;
            if (!Directory.Exists(Data.ProjectsRootDirectory)) return;
            var godotProjectFiles = Directory.EnumerateFiles(Data.ProjectsRootDirectory, "project.godot", SearchOption.AllDirectories);
            foreach (string projectFile in godotProjectFiles)
            {
                if (Data.Projects.Any(proj => proj.Path == projectFile)) continue;

                var project = new Project
                {
                    Path = projectFile
                };
                project.Load();

                if (Data.Engines?.Any() ?? false)
                    project.LaunchInstallation = Data.Engines.First();

                Data.Projects.Add(project);
            }
            Save(); // Auto-save after scanning
        }

        [AutoCommand]
        public void ClearProjects()
        {
            Data.Projects.Clear();
            Save(); // Auto-save after clearing
        }

        [AutoCommand]
        public void ClearEngines()
        {
            Data.Engines.Clear();
            Save(); // Auto-save after clearing
        }

        [AutoCommand]
        public void ScanForEngines()
        {
            if (string.IsNullOrEmpty(Data.EnginesRootDirectory)) return;
            if (!Directory.Exists(Data.EnginesRootDirectory)) return;

            var godotFolders = Directory.EnumerateFiles(Data.EnginesRootDirectory, "Godot_*.exe", SearchOption.AllDirectories);
            foreach (string executable in godotFolders)
            {
                if (Data.Engines.Any(inst => inst.Path == executable)) continue;
                Data.Engines.Add(new GodotInstallation(executable));
            }
            Save(); // Auto-save after scanning
        }

        public void AddChild(object value)
        {
            if (value is GithubSource githubSource && GithubSources != null)
                GithubSources.Add(githubSource);
        }

        public void AddText(string text)
        {
        }
    }
}