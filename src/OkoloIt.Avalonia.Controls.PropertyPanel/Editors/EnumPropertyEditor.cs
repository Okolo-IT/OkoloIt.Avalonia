using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a <see langword="enum"/> type.
/// </summary>
internal class EnumPropertyEditor : ContentControl, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor for a <see langword="enum"/> type.
    /// </summary>
    internal EnumPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public void Bind(PropertyItem property)
    {
        ComboBox comboBox = new() {
            ItemsSource = Enum.GetValues(property.PropertyType),
            SelectedItem = property.Value,
        };

        comboBox.Bind(ComboBox.SelectedItemProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = comboBox;
    }
}
