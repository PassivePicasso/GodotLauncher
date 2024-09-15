#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0169
using MVVMGenerator.Attributes;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GodotLauncher.View
{
    public partial class CommandingWindow : Window
    {
        [AutoDProp] ICommand onLoadedCommand;
        [AutoDProp] object onLoadedCommandParameter;
        [AutoDProp] ICommand onUnloadedCommand;
        [AutoDProp] object onUnloadedCommandParameter;
        [AutoDProp] ICommand onClosingCommand;

        public CommandingWindow()
        {
            Loaded += MainWindow_Loaded;
            Unloaded += CommandingWindow_Unloaded;
            Closing += CommandingWindow_Closing;
        }

        private void CommandingWindow_Closing(object? sender, CancelEventArgs e)
        {
            if (OnClosingCommand?.CanExecute(e) ?? false)
                OnClosingCommand.Execute(e);
        }

        private void CommandingWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (OnUnloadedCommand?.CanExecute(OnUnloadedCommandParameter) ?? false)
                OnUnloadedCommand.Execute(OnUnloadedCommandParameter);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (OnLoadedCommand?.CanExecute(OnLoadedCommandParameter) ?? false)
                OnLoadedCommand.Execute(OnLoadedCommandParameter);
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
