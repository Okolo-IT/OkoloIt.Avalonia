namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor interface for binding.
/// </summary>
public interface IPropertyEditor
{
    /// <summary>
    /// Binds a property to an editor.
    /// </summary>
    /// <param name="property">
    /// A property element representing its display in the interface.
    /// </param>
    public void Bind(PropertyItem property);
}
