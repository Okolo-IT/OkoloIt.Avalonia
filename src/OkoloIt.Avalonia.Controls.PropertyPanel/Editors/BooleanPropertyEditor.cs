using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a <see langword="bool"/> type.
/// </summary>
internal class BooleanPropertyEditor : ContentControl, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor for a <see langword="bool"/> type.
    /// </summary>
    internal BooleanPropertyEditor()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Center;
        VerticalContentAlignment = VerticalAlignment.Center;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        CheckBox checkBox = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            DataContext = property,
        };

        checkBox.Bind(CheckBox.IsCheckedProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = checkBox;
    }
}
