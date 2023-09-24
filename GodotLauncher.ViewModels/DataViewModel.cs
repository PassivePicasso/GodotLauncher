using MVVMGenerator.Attributes;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GodotLauncher.ViewModels
{
    public partial class DataViewModel 
    {
        private static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string SettingsPath = System.IO.Path.Combine(LocalAppData, "GodotLauncher", "Settings.json");

        [AutoNotify] string enginesRootDirectory = string.Empty;
        [AutoNotify] string projectsRootDirectory = string.Empty;
        [AutoNotify] ObservableCollection<Project> projects = new();
        [AutoNotify] ObservableCollection<GodotInstallation> engines = new();
        [AutoNotify, JsonIgnore] string sourcePath = SettingsPath;

        public void Load() => SerializationUtilities.Populate(SourcePath, this);

        public void Save() => SerializationUtilities.Save(SourcePath, this);
    }
}
