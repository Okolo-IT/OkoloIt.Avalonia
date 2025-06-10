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
        VerticalAlignment   = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        Content = new TextBox() {
            VerticalAlignment   = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Text = property.Value?.ToString(),
            IsReadOnly = true,
        };
    }
}
