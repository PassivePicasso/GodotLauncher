using MVVM.Generator.Attributes;

namespace GodotLauncher.ViewModels.Github
{
    public partial class ApiInfoViewModel
    {
        [AutoNotify] DateTimeOffset? lastRequest;
        [AutoNotify] DateTimeOffset? resetTime;
        [AutoNotify] int? limit;
        [AutoNotify] int? remaining;
    }
}
