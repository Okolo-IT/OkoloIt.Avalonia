using System.Globalization;

using Avalonia.Data.Converters;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a <see cref="Guid"/> type.
/// </summary>
internal class GuidPropertyEditor : TextPropertyEditor, IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value;
    }

    /// <inheritdoc/>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return Guid.TryParse(value?.ToString(), out Guid guid)
            ? guid
            : Guid.Empty;
    }
}
