using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

public class EnumPropertyEditor : ContentControl, IPropertyEditor
{
    public EnumPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

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
