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
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        NumericUpDown numericUpDown = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            DataContext = property,
        };

        numericUpDown.Bind(NumericUpDown.ValueProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = numericUpDown;
    }
}
