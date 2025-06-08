using System.ComponentModel;
using System.Globalization;

using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a <see langword="enum"/> type.
/// </summary>
internal class EnumPropertyEditor : ContentControl, IPropertyEditor, IValueConverter
{
    private List<EnumItem> _enumValues = [];

    private record struct EnumItem(Enum Value, string Name);

    /// <summary>
    /// Creates an instance of the default property editor for a <see langword="enum"/> type.
    /// </summary>
    internal EnumPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        _enumValues.Clear();

        _enumValues = Enum.GetValues(property.PropertyType)
            .Cast<Enum>()
            .Select(x => new EnumItem(x, GetDescription(x)))
            .ToList();

        ComboBox comboBox = new() {
            ItemsSource  = _enumValues.Select(x => x.Name),
            SelectedItem = _enumValues.FirstOrDefault(x => x.Value.Equals(property.Value)).Name,
        };

        comboBox.Bind(ComboBox.SelectedItemProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = comboBox;
    }

    /// <inheritdoc/>
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        object? convertedValue = default;

        if (value is Enum enumValue)
            convertedValue = _enumValues.FirstOrDefault(x => x.Value.Equals(enumValue)).Name;

        return convertedValue ?? value?.ToString() ?? string.Empty;
    }

    /// <inheritdoc/>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value is string enumName
            ? _enumValues.FirstOrDefault(x => x.Name.Equals(enumName)).Value
            : default;
    }

    private static string GetDescription(Enum value)
    {
        var attributes = value.GetType()
            .GetField(value.ToString())?
            .GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return attributes?.Length > 0
            ? attributes[0].Description
            : value.ToString();
    }
}
