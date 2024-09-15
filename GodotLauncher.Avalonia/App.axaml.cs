using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using GodotLauncher.Views;

namespace GodotLauncher;
using IUseScreen = ISingleViewApplicationLifetime;
using IUseWindow = IClassicDesktopStyleApplicationLifetime;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);


        if (ApplicationLifetime is IUseWindow windowHost) windowHost.MainWindow = new MainWindow { };
        else if (ApplicationLifetime is IUseScreen screenHost) screenHost.MainView = new MainView { };


        base.OnFrameworkInitializationCompleted();
    }
}
