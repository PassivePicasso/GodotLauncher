using MVVMGenerator.Attributes;
using System.Text.Json.Serialization;

namespace GodotLauncher.ViewModels
{
    public partial class Theme
    {
        private static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string ThemePath = System.IO.Path.Combine(LocalAppData, "GodotLauncher", "Theme.json");

        [AutoNotify] string editableForegroundColor = "#c92";
        [AutoNotify] string editableBackgroundColor = "#333";
        [AutoNotify] string editableBorderColor = "#333";

        [AutoNotify] string selectionHighlightColor = "#00F";
        [AutoNotify] string selectedBackgroundColor = "#888";
        [AutoNotify] string selectedForegroundColor = "#FFF";

        [AutoNotify] string standardBackgroundColor = "#222";
        [AutoNotify] string standardForegroundColor = "#CCC";
        [AutoNotify] string standardBorderColor = "#333";

        [AutoNotify] string hoverBackgroundColor = "#666";
        [AutoNotify] string hoverForegroundColor = "#666";
        [AutoNotify] string hoverBorderColor = "#00F";

        [AutoNotify] string activeBackgroundColor = "#444";
        [AutoNotify] string activeForegroundColor = "#444";
        [AutoNotify] string activeBorderColor = "#666";

        [AutoNotify] string disabledForegroundColor = "#888";
        [AutoNotify] string disabledBackgroundColor = "#888";
        [AutoNotify] string disabledBorderColor = "#888";

        [AutoNotify] string glyphColor = "#444";

        [AutoNotify] string normalColor = "#c8c8c8";
        [AutoNotify] string normalBorderColor = "#888";
        [AutoNotify] string horizontalNormalColor = "#c8c8c8";
        [AutoNotify] string horizontalNormalBorderColor = "#888";

        [AutoNotify, JsonIgnore] string sourcePath = ThemePath;

        public void Load()
        {
            SerializationUtilities.Populate(SourcePath, this);
        }

        public void Save() => SerializationUtilities.Save(SourcePath, this);
    }
}
