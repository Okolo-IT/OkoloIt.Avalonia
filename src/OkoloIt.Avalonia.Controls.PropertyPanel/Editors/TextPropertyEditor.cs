using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

public class TextPropertyEditor : ContentControl, IPropertyEditor
{
    public TextPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    public void Bind(PropertyItem property)
    {
        TextBox textBox = new() {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Text = property.Value?.ToString(),
            IsReadOnly = property.IsReadOnly,
        };

        textBox.Bind(TextBox.TextProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = textBox;
    }
}
