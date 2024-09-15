using MVVMGenerator.Attributes;
using System.Collections.ObjectModel;

namespace GodotLauncher.ViewModels.Github
{
    public partial class RepositoryViewModel
    {
        [AutoNotify] string? fullName;
        [AutoNotify] string? description;
        [AutoNotify] string? url;
        [AutoNotify] DateTimeOffset? updatedAt;
        [AutoNotify] ObservableCollection<ReleaseViewModel>? releases;
        [AutoNotify] ApiInfoViewModel? apiInfo;
    }
}
