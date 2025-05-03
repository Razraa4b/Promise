using Avalonia.Data.Converters;
using Promise.Domain.Enums;
using System;
using System.Globalization;

namespace Promise.UI.Converters
{
    public class NotificationTypeToIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is NotificationType type)
            {
                switch (type)
                {
                    case NotificationType.Success:
                        return "/Assets/alert-success.svg";
                    case NotificationType.Info:
                        return "/Assets/alert-info.svg";
                    case NotificationType.Warning:
                        return "/Assets/alert-warning.svg";
                    case NotificationType.Error:
                        return "/Assets/alert-error.svg";
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
