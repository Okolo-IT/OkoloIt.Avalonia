using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

public class BooleanPropertyEditor : ContentControl, IPropertyEditor
{
    public BooleanPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    public void Bind(PropertyItem property)
    {
        CheckBox checkBox = new() {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            DataContext = property,
        };

        checkBox.Bind(CheckBox.IsCheckedProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = checkBox;
    }
}
