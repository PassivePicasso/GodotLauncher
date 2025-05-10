#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0169
#pragma warning disable IDE0044 // Add readonly modifier
using Avalonia.Controls;
using Avalonia.Interactivity;
using MVVM.Generator.Attributes;
using System.Windows.Input;

namespace GodotLauncher.Views
{
    public partial class CommandingControl : UserControl
    {
        [AutoSProp] ICommand onLoadedCommand;
        [AutoSProp] object onLoadedCommandParameter;
        [AutoSProp] ICommand onUnloadedCommand;
        [AutoSProp] object onUnloadedCommandParameter;

        public CommandingControl()
        {
            Loaded += ControlLoaded;
            Unloaded += ControlUnloaded;
        }


        private void ControlUnloaded(object? sender, RoutedEventArgs e)
        {
            if (OnUnloadedCommand?.CanExecute(OnUnloadedCommandParameter) ?? false)
                OnUnloadedCommand.Execute(OnUnloadedCommandParameter);
        }

        private void ControlLoaded(object? sender, RoutedEventArgs e)
        {
            if (OnLoadedCommand?.CanExecute(OnLoadedCommandParameter) ?? false)
                OnLoadedCommand.Execute(OnLoadedCommandParameter);
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
