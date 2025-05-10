using GodotLauncher.ViewModels.Github;
using MVVM.Generator.Attributes;
using System.Diagnostics;

namespace GodotLauncher.ViewModels
{
    public partial class GithubSource
    {
        private static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static Octokit.GitHubClient client = new(new Octokit.ProductHeaderValue("Godot-Launcher"));

        [AutoNotify] string? owner;
        [AutoNotify] string? repositoryName;

        [AutoNotify] RepositoryViewModel? repository;
        [AutoNotify] DateTimeOffset? lastRequestTime;
        [AutoNotify] MainViewModel? mainViewModel;

        bool loading;
        string sourcePath => System.IO.Path.Combine(LocalAppData, "GodotLauncher", "GithubSources", $"{owner}.{repositoryName}.json");
        public GithubSource()
        {
        }
        public void Load() => SerializationUtilities.Populate(sourcePath, this);
        public void Save() => SerializationUtilities.Save(sourcePath, this);


        [AutoCommand]
        public void LoadRepository() => _ = LoadRepositoryAsync();
        private async Task LoadRepositoryAsync()
        {
            try
            {
                if (Repository == null)
                {
                    Load();
                }
                if (Repository != null && DateTimeOffset.Now - lastRequestTime < TimeSpan.FromMinutes(15))
                    return;
                lastRequestTime = DateTimeOffset.Now;

                if (loading) return;
                loading = true;
                var repository = await client.Repository.Get(Owner, RepositoryName);
                var rls = await client.Repository.Release.GetAll(repository.Id);
                if (rls == null) return;
                var rlsVms = rls
                    .Take(50)
                    .Select(r =>
                    {
                        var release = new ReleaseViewModel
                        {
                            Name = r.Name,
                            PublishedAt = r.PublishedAt,
                            TagName = r.TagName,
                            Url = r.Url
                        };
                        release.Assets = new(r.Assets.Select(ra => new ReleaseAssetViewModel
                        {
                            Name = ra.Name,
                            DownloadUrl = ra.BrowserDownloadUrl,
                            Release = release
                        }));
                        return release;
                    });

                var lastApiInfo = client.GetLastApiInfo();

                Repository = new RepositoryViewModel
                {
                    FullName = repository.FullName,
                    Description = repository.Description,
                    Releases = new(rlsVms),
                    ApiInfo = new ApiInfoViewModel
                    {
                        LastRequest = DateTimeOffset.Now,
                        Limit = lastApiInfo.RateLimit.Limit,
                        Remaining = lastApiInfo.RateLimit.Remaining,
                        ResetTime = lastApiInfo.RateLimit.Reset.ToLocalTime(),
                    }
                };
            }
            catch (Exception ex)
            {
                var x = ex;
                Debugger.Break();
            }
            finally
            {
                loading = false;
            }
        }
    }
}
