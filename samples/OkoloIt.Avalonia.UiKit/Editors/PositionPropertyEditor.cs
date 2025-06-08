using System.Globalization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;

using OkoloIt.Avalonia.Controls;
using OkoloIt.Avalonia.Controls.Editors;
using OkoloIt.Avalonia.UiKit.Models;

namespace OkoloIt.Avalonia.UiKit.Editors;

public class PositionPropertyEditor : Grid, IPropertyEditor, IValueConverter
{
    public PositionPropertyEditor()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
        ColumnSpacing = 5;
        RowSpacing = 4;
        RowDefinitions = [
            new RowDefinition(GridLength.Auto),
            new RowDefinition(GridLength.Auto),
        ];
    }

    public void Bind(PropertyItem property)
    {
        NumericUpDown numericUpDownForX = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            FormatString = "0",
            DataContext = property,
        };

        numericUpDownForX.Bind(NumericUpDown.ValueProperty, new Binding($"{nameof(PropertyItem.Value)}.{nameof(Position.X)}") {
            Mode = BindingMode.TwoWay
        });

        Children.Add(numericUpDownForX);
        SetRow(numericUpDownForX, 0);

        NumericUpDown numericUpDownForY = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            FormatString = "0",
            DataContext = property,
        };

        numericUpDownForY.Bind(NumericUpDown.ValueProperty, new Binding($"{nameof(PropertyItem.Value)}.{nameof(Position.Y)}") {
            Mode = BindingMode.TwoWay
        });

        Children.Add(numericUpDownForY);
        SetRow(numericUpDownForY, 1);
    }

    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        return value;
    }
}
