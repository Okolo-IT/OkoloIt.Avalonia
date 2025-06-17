using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

using OkoloIt.Avalonia.Controls.Helpers;

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

        ConfigureControl(numericUpDown, property);

        numericUpDown.Bind(NumericUpDown.ValueProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = numericUpDown;
    }

    private static void ConfigureControl(NumericUpDown numericUpDown, PropertyItem property)
    {
        var (minimum, maximum) = GetMinMax(property);

        if (property.PropertyType == typeof(int)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (int)maximum : int.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (int)minimum : int.MinValue;
            return;
        }

        if (property.PropertyType == typeof(uint)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (uint)maximum : uint.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (uint)minimum : uint.MinValue;
            return;
        }

        if (property.PropertyType == typeof(long)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (long)maximum : long.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (long)minimum : long.MinValue;
            return;
        }

        if (property.PropertyType == typeof(ulong)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (ulong)maximum : ulong.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (ulong)minimum : ulong.MinValue;
            return;
        }

        if (property.PropertyType == typeof(short)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (short)maximum : short.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (short)minimum : short.MinValue;
            return;
        }

        if (property.PropertyType == typeof(ushort)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (ushort)maximum : ushort.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (ushort)minimum : ushort.MinValue;
            return;
        }

        if (property.PropertyType == typeof(byte)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (byte)maximum : byte.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (byte)minimum : byte.MinValue;
            return;
        }

        if (property.PropertyType == typeof(sbyte)) {
            numericUpDown.FormatString = "0";
            numericUpDown.Maximum = maximum is not null ? (sbyte)maximum : sbyte.MaxValue;
            numericUpDown.Minimum = minimum is not null ? (sbyte)minimum : sbyte.MinValue;
            return;
        }
    }

    private static (object? Minimum, object? Maximum) GetMinMax(PropertyItem property)
    {
        try {
            foreach (var attribute in property.Attributes) {
                if (attribute is RangeAttribute range && range.OperandType.IsNumeric()) {
                    // Checking and converting minimum and maximum values.
                    _ = range.FormatErrorMessage(string.Empty);
                    return (range.Minimum, range.Maximum);
                }
            }
        }
        catch (Exception ex) {
            Debug.WriteLine($"[Okit]: {ex.Message}. Property `{property.Name}`.");
        }

        return (default, default);
    }
}
