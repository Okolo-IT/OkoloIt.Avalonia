using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

public class NumericPropertyEditor : ContentControl, IPropertyEditor
{
    public NumericPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    public void Bind(PropertyItem property)
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
