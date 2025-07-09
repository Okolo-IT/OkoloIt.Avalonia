using System.Globalization;

using Avalonia.Data.Converters;

namespace OkoloIt.Avalonia.Dialogs.Converters;

internal class MessageBoxTypeToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return MessageBoxProperties.IconConverter.Convert(value, targetType, parameter, culture);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return MessageBoxProperties.IconConverter.ConvertBack(value, targetType, parameter, culture);
    }
}

internal class DefaultMessageBoxTypeToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}