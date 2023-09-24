using MVVMGenerator.Attributes;
using System.Diagnostics;

namespace GodotLauncher.ViewModels
{
    public partial class GodotInstallation
    {
        [AutoNotify] string? path;
        [AutoNotify] string? name;
        [AutoNotify] string? arguments;
        public GodotInstallation(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path).Replace("_", " ").Replace("-", " ");
        }

        [AutoCommand]
        public void Launch()
        {
            if(Path == null) return; 
            
            var processStartInfo = new ProcessStartInfo
            {
                FileName = Path,
                ArgumentList = {
                    Path
                },
            };
            if (!string.IsNullOrEmpty(Arguments))
                processStartInfo.ArgumentList.Add(Arguments);

            Process.Start(processStartInfo);
        }
    }
}
