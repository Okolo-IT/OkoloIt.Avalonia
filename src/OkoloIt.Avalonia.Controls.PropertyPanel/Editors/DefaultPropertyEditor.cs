using Avalonia.Controls;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Default property editor.
/// </summary>
internal class DefaultPropertyEditor : ContentControl, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor.
    /// </summary>
    internal DefaultPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalContentAlignment = VerticalAlignment.Center;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        Content = new TextBox() {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalContentAlignment = VerticalAlignment.Center,
            Text = property.Value?.ToString(),
            IsReadOnly = true,
        };
    }
}
