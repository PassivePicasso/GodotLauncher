using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using GodotLauncher.Converters;
using GodotLauncher.ViewModels;
using System;
using System.Windows.Input;

namespace GodotLauncher.Views
{
    public partial class MainView : CommandingControl
    {
        public static readonly StyledProperty<ICommand> OnLoadProperty = AvaloniaProperty.Register<MainView, ICommand>(nameof(OnLoad));
        public ICommand OnLoad
        {
            get { return GetValue(OnLoadProperty); }
            set { SetValue(OnLoadProperty, value); }
        }

        public MainView()
        {
            InitializeComponent();
            Loaded += MainView_Loaded;
            Unloaded += MainView_Unloaded;
        }

        private void MainView_Unloaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (Application.Current?.FindResource("MainViewModel") is MainViewModel mainViewModel)
            {
                SaveBrushes(mainViewModel.Theme);
                mainViewModel.Theme.Save();
            }
        }

        private void MainView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
            if (Application.Current?.FindResource("MainViewModel") is MainViewModel mainViewModel)
            {
                mainViewModel.Theme.Load();
                LoadBrushes(mainViewModel.Theme);
            }
        }

        void LoadBrushes(Theme theme)
        {
            var converter = new StringColorConverter();
            void Convert(string value, string name)
            {
                if (Application.Current?.FindResource(name.Replace("Color", "Brush")) is not SolidColorBrush scb)
                    return;
                if (converter.Convert(value, typeof(Color)) is not Color c)
                    return;

                scb.Color = c;
            }
            Convert(theme.NormalBackgroundColor, nameof(theme.NormalBackgroundColor));
            Convert(theme.NormalForegroundColor, nameof(theme.NormalForegroundColor));
            Convert(theme.NormalBorderColor,/**/ nameof(theme.NormalBorderColor));

            Convert(theme.EditableBackgroundColor, nameof(theme.EditableBackgroundColor));
            Convert(theme.EditableForegroundColor, nameof(theme.EditableForegroundColor));
            Convert(theme.EditableBorderColor,/**/ nameof(theme.EditableBorderColor));

            Convert(theme.ActiveBackgroundColor, nameof(theme.ActiveBackgroundColor));
            Convert(theme.ActiveForegroundColor, nameof(theme.ActiveForegroundColor));
            Convert(theme.ActiveBorderColor,/**/ nameof(theme.ActiveBorderColor));

            Convert(theme.DisabledBackgroundColor, nameof(theme.DisabledBackgroundColor));
            Convert(theme.DisabledForegroundColor, nameof(theme.DisabledForegroundColor));
            Convert(theme.DisabledBorderColor,/**/ nameof(theme.DisabledBorderColor));

            Convert(theme.HoverBackgroundColor, nameof(theme.HoverBackgroundColor));
            Convert(theme.HoverForegroundColor, nameof(theme.HoverForegroundColor));
            Convert(theme.HoverBorderColor,/**/ nameof(theme.HoverBorderColor));

            Convert(theme.SelectedBackgroundColor, nameof(theme.SelectedBackgroundColor));
            Convert(theme.SelectedForegroundColor, nameof(theme.SelectedForegroundColor));
            Convert(theme.SelectionHighlightColor, nameof(theme.SelectionHighlightColor));

            Convert(theme.GlyphColor, nameof(theme.GlyphColor));
        }
        void SaveBrushes(Theme theme)
        {
            var converter = new StringColorConverter();
            string Convert(string valueName)
            {
                var value = Application.Current?.FindResource(valueName.Replace("Color", "Brush"));
                if (converter.ConvertBack(value, typeof(string)) is not string s)
                    throw new ArgumentException($"{value?.GetType().FullName ?? "null"} value not convertable to string");

                return s;
            }
            theme.NormalBackgroundColor = Convert(nameof(theme.NormalBackgroundColor));
            theme.NormalForegroundColor = Convert(nameof(theme.NormalForegroundColor));
            theme.NormalBorderColor = Convert(nameof(theme.NormalBorderColor));

            theme.EditableBackgroundColor = Convert(nameof(theme.EditableBackgroundColor));
            theme.EditableForegroundColor = Convert(nameof(theme.EditableForegroundColor));
            theme.EditableBorderColor = Convert(nameof(theme.EditableBorderColor));

            theme.ActiveBackgroundColor = Convert(nameof(theme.ActiveBackgroundColor));
            theme.ActiveForegroundColor = Convert(nameof(theme.ActiveForegroundColor));
            theme.ActiveBorderColor = Convert(nameof(theme.ActiveBorderColor));

            theme.DisabledBackgroundColor = Convert(nameof(theme.DisabledBackgroundColor));
            theme.DisabledForegroundColor = Convert(nameof(theme.DisabledForegroundColor));
            theme.DisabledBorderColor = Convert(nameof(theme.DisabledBorderColor));

            theme.HoverBackgroundColor = Convert(nameof(theme.HoverBackgroundColor));
            theme.HoverForegroundColor = Convert(nameof(theme.HoverForegroundColor));
            theme.HoverBorderColor = Convert(nameof(theme.HoverBorderColor));

            theme.SelectedBackgroundColor = Convert(nameof(theme.SelectedBackgroundColor));
            theme.SelectedForegroundColor = Convert(nameof(theme.SelectedForegroundColor));
            theme.SelectionHighlightColor = Convert(nameof(theme.SelectionHighlightColor));

            theme.GlyphColor = Convert(nameof(theme.GlyphColor));
        }
    }
}
