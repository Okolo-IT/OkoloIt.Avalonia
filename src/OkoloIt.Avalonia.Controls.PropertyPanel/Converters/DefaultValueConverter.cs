using Avalonia.Data.Converters;

namespace OkoloIt.Avalonia.Controls.Converters;

/// <summary>
/// Default value converter.
/// </summary>
public static class DefaultValueConverter
{
    /// <summary>
    /// Converts the value to the target type using standard conversion methods.
    /// </summary>
    /// <param name="value">Converted value.</param>
    /// <param name="targetType">Target type to convert the value to.</param>
    /// <returns>Converted value.</returns>
    public static object? ConvertValue(object? value, Type targetType)
    {
        if (value is null)
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

        // If the type already matches.
        if (targetType.IsInstanceOfType(value))
            return value;

        // Special cases for numeric types.
        if (targetType == typeof(int)) {
            if (value is decimal decimalValue)
                return (int)decimalValue;

            if (value is string strValue && int.TryParse(strValue, out var intValue))
                return intValue;
        }

        // Standard conversion.
        return Convert.ChangeType(value, targetType);
    }
}
