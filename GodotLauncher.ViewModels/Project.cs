using MVVMGenerator.Attributes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace GodotLauncher.ViewModels
{
    public partial class Project
    {
        [AutoNotify] GodotInstallation? launchInstallation;
        [AutoNotify] string? path;
        [AutoNotify] string? name;
        [AutoNotify] ObservableCollection<string>? featureTags;
        [AutoNotify] ObservableCollection<string>? plugins;

        Regex projectNameRegex = NameRegex();
        Regex projectFeaturesRegex = FeaturesRegex();
        Regex projectPluginsRegex = PluginsRegex();

        [AutoCommand]
        public void Load()
        {
            if (Path == null || string.IsNullOrEmpty(Path)) return;
            var text = File.ReadAllLines(Path);
            var projectNameField = text.First(projectNameRegex.IsMatch);
            var nameMatch = projectNameRegex.Match(projectNameField);
            var projectName = nameMatch.Groups[1].Value;

            var projectFeatureField = text.First(projectFeaturesRegex.IsMatch);
            var featureMatch = projectFeaturesRegex.Match(projectFeatureField);
            var projectFeatures = featureMatch.Groups["features"].Value;
            var tags = projectFeatures
                .Split(",").
                Select(s => s.Trim(' ', '\"', '"'));

            var projectPluginField = text.First(projectPluginsRegex.IsMatch);
            var pluginMatch = projectPluginsRegex.Match(projectPluginField);
            var projectPlugins = pluginMatch.Groups["plugins"].Value;
            var plugins = projectPlugins
                .Split(",")
                .Select(s => s.Trim(' ', '\"', '"'))
                .Select(s =>
                        s.Replace("res://addons/", "")
                         .Replace("/plugin.cfg", "")
                         .Replace("_", " ")
                         .Replace("-", " ")
                       )
                .Select(s => $"{s.Substring(0, 1).ToUpperInvariant()}{s.Substring(1)}")
                .Select(s => Regex.Replace(s, @"([a-z])([A-Z])", "$1 $2"))
                .Select(s => Regex.Replace(s, @"(?<=\s)\w", m => m.Value.ToUpper()))
                ;

            Name = projectName;
            FeatureTags = new ObservableCollection<string>(tags);
            Plugins = new ObservableCollection<string>(plugins);
            
        }

        [AutoCommand]
        public void Launch()
        {
            if (launchInstallation == null || Path == null) return;

            var processStartInfo = new ProcessStartInfo
            {
                FileName = launchInstallation.Path,
                ArgumentList = {
                    Path
                },
            };
            if (!string.IsNullOrEmpty(launchInstallation.Arguments))
                processStartInfo.ArgumentList.Add(launchInstallation.Arguments);

            Process.Start(processStartInfo);
        }

        [GeneratedRegex("config/name=\"(.*?)\"", RegexOptions.Compiled)]
        private static partial Regex NameRegex();
        [GeneratedRegex("config/features=PackedStringArray\\((?<features>.*?)\\)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture)]
        private static partial Regex FeaturesRegex();
        [GeneratedRegex("enabled=PackedStringArray\\((?<plugins>.*?)\\)", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture)]
        private static partial Regex PluginsRegex();
    }
}
