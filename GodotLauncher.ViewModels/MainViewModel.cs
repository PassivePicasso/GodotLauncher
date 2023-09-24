using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMGenerator.Attributes;
using System.IO;

namespace GodotLauncher.ViewModels
{
    public partial class MainViewModel
    {
        [AutoNotify] DataViewModel? data;
        [AutoNotify] Theme? theme;

        public MainViewModel()
        {
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Data):
                    Data?.Load();
                    break;

                case nameof(Theme):
                    Theme?.Load();
                    break;
            }
        }

        public void Save()
        {
            Data?.Save();
            Theme?.Save();
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
            }
        }

        [AutoCommand]
        public void ScanForProjects()
        {
            if (string.IsNullOrEmpty(Data.ProjectsRootDirectory)) return;
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
        }

        [AutoCommand]
        public void ClearProjects() => Data.Projects.Clear();

        [AutoCommand]
        public void ClearEngines() => Data.Engines.Clear();

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
        }
    }
}