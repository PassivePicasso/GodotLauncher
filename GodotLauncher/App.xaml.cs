using GodotLauncher.ViewModels;
using System.Windows;

namespace GodotLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            var mainViewModel = Resources["MainViewModel"] as MainViewModel;
            mainViewModel?.Save();
        }
    }
}
