using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Utilities;
using Octokit;
using System;
using System.Globalization;

namespace GodotLauncher.Converters
{

    public class StringColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type? targetType = null, object? parameter = null, CultureInfo? culture = null)
        {
            if (targetType == null || targetType == typeof(Color))
                switch (value)
                {
                    case SolidColorBrush valueBrush:
                        {
                            // A brush may have an opacity set along with alpha transparency
                            double alpha = valueBrush.Color.A * valueBrush.Opacity;

                            return new Color(
                                (byte)MathUtilities.Clamp(alpha, 0x00, 0xFF),
                                valueBrush.Color.R,
                                valueBrush.Color.G,
                                valueBrush.Color.B);
                        }
                    case Color valueColor:
                        return valueColor;
                    case HslColor valueHslColor:
                        return valueHslColor.ToRgb();
                    case HsvColor valueHsvColor:
                        return valueHsvColor.ToRgb();
                    case string valueText when HsvColor.TryParse(valueText, out var hsvColor):
                        return hsvColor.ToRgb();
                    case string valueText when HslColor.TryParse(valueText, out var hsvColor):
                        return hsvColor.ToRgb();
                    case string valueText when Color.TryParse(valueText, out var hsvColor):
                        return hsvColor;
                }

            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter = null, CultureInfo? culture = null)
        {
            if (targetType == typeof(string))
                switch (value)
                {
                    case string valueText:
                        return valueText;
                    case SolidColorBrush valueBrush:
                        return valueBrush.Color.ToString();
                    case Color valueColor:
                        return valueColor.ToString();
                    case HslColor valueHslColor:
                        return valueHslColor.ToRgb().ToString();
                    case HsvColor valueHsvColor:
                        return valueHsvColor.ToRgb().ToString();
                    default:
                        return value?.ToString() ?? string.Empty;
                }

            return AvaloniaProperty.UnsetValue;
        }
    }
}
