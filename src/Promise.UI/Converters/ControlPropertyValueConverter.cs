using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Globalization;
using System.Reflection;

namespace Promise.UI.Converters
{
    public class ControlPropertyValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? property = parameter?.ToString();
            if (property != null && value != null)
            {
                Control control = (Control)value;
                Type type = control.GetType();

                PropertyInfo? propertyInfo = type.GetProperty(property);
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(control);
                }
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
