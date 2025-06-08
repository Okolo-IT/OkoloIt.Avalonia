using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a number type.
/// </summary>
internal class NumericPropertyEditor : ContentControl, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor for a number type.
    /// </summary>
    internal NumericPropertyEditor()
    {
        VerticalAlignment   = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        NumericUpDown numericUpDown = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            DataContext = property,
        };

        if (property.PropertyType == typeof(int)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = int.MaxValue;
            numericUpDown.Minimum = int.MinValue;
        }

        if (property.PropertyType == typeof(uint)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = uint.MaxValue;
            numericUpDown.Minimum = uint.MinValue;
        }

        if (property.PropertyType == typeof(long)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = long.MaxValue;
            numericUpDown.Minimum = long.MinValue;
        }

        if (property.PropertyType == typeof(ulong)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = ulong.MaxValue;
            numericUpDown.Minimum = ulong.MinValue;
        }

        if (property.PropertyType == typeof(short)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = short.MaxValue;
            numericUpDown.Minimum = short.MinValue;
        }

        if (property.PropertyType == typeof(ushort)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = ushort.MaxValue;
            numericUpDown.Minimum = ushort.MinValue;
        }

        if (property.PropertyType == typeof(byte)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = byte.MaxValue;
            numericUpDown.Minimum = byte.MinValue;
        }

        if (property.PropertyType == typeof(sbyte)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = sbyte.MaxValue;
            numericUpDown.Minimum = sbyte.MinValue;
        }

        numericUpDown.Bind(NumericUpDown.ValueProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = numericUpDown;
    }
}
