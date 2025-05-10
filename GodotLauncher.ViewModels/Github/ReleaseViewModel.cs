using MVVM.Generator.Attributes;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace GodotLauncher.ViewModels.Github
{
    public partial class ReleaseViewModel
    {
        [AutoNotify] string? tagName;
        [AutoNotify] string? url;
        [AutoNotify] string? name;
        [AutoNotify, JsonIgnore] bool isDownloading = false;
        [AutoNotify] DateTimeOffset? publishedAt;
        [AutoNotify] ObservableCollection<ReleaseAssetViewModel> assets;
    }
}
