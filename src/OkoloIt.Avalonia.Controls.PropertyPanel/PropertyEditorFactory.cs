using System.ComponentModel;
using System.Reflection;
using Avalonia.Controls;

using OkoloIt.Avalonia.Controls.Editors;
using OkoloIt.Avalonia.Controls.Helpers;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// A factory that creates editors for properties of different types.
/// </summary>
public static class PropertyEditorFactory
{
    /// <summary>
    /// Creates an editor for the property.
    /// </summary>
    /// <param name="property">Descriptor of the property for which the editor is created.</param>
    /// <returns>Editor for the property.</returns>
    public static Control CreateEditor(PropertyDescriptor property)
    {
        // Creating a dedicated property editor.
        var editorAttribute = property.Attributes.OfType<EditorAttribute>().FirstOrDefault();

        if (editorAttribute is not null)
        {
            var assembly = Assembly.GetEntryAssembly();
            var editorTypeInfo = assembly.DefinedTypes
                .FirstOrDefault(t => t.Name.Equals(
                    editorAttribute.EditorTypeName,
                    StringComparison.OrdinalIgnoreCase));

            var editorType = assembly.GetType(editorTypeInfo?.FullName);

            if (Activator.CreateInstance(editorType) is Control editor)
                return editor;
        }

        // Creating inline editors.
        if (property.PropertyType.IsBool())
            return new BooleanPropertyEditor();
        if (property.PropertyType.IsEnum)
            return new EnumPropertyEditor();
        if (property.PropertyType.IsText())
            return new TextPropertyEditor();
        if (property.PropertyType.IsGuid())
            return new GuidPropertyEditor();
        if (property.PropertyType.IsDateTime())
            return new DateTimePropertyEditor();
        if (property.PropertyType.IsNumeric())
            return new NumericPropertyEditor();

        // Creating a default editor.
        return new DefaultPropertyEditor();
    }

    /// <summary>
    /// Binds the editor to the property.
    /// </summary>
    /// <param name="editor">An instance of a bindable editor.</param>
    /// <param name="item">The instance of the property to which the editor is bound.</param>
    /// <exception cref="NotSupportedException">Manual binding is not supported.</exception>
    public static void BindEditor(Control editor, PropertyItem item)
    {
        // Automatic linking.
        if (editor is IPropertyEditor propertyEditor)
        {
            propertyEditor.Bind(item);
            return;
        }

        // Manual binding.
        throw new NotSupportedException("Manual binding is not supported.");
    }
}
