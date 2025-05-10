using MVVM.Generator.Attributes;

namespace GodotLauncher.ViewModels.Github
{
    public partial class ReleaseAssetViewModel
    {
        [AutoNotify] ReleaseViewModel release;
        [AutoNotify] string? name;
        [AutoNotify] string downloadUrl;
    }
}
