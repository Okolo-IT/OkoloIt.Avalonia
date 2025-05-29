using System.ComponentModel;

using Avalonia.Controls;
using Avalonia.Data;

using OkoloIt.Avalonia.Controls.Editors;

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
    /// <param name="item">A property element representing its display in the interface.</param>
    /// <returns>Editor for the property.</returns>
    public static Control Create(PropertyDescriptor property, PropertyItem item)
    {
        var editor = CreateEditor(property, item);
        BindEditor(editor, item);

        return editor;
    }

    private static Control CreateEditor(PropertyDescriptor property, PropertyItem item)
    {
        // Creating a dedicated property editor.
        var editorAttribute = property.Attributes.OfType<EditorAttribute>().FirstOrDefault();

        if (editorAttribute is not null) {
            var editorType = Type.GetType(editorAttribute.EditorTypeName);

            if (editorType is not null) {
                var instance = Activator.CreateInstance(editorType) as Control;

                if (instance is Control editor)
                    return editor;
            }
        }

        // Creating inline editors.
        if (property.PropertyType == typeof(bool))
            return new BooleanPropertyEditor();
        if (property.PropertyType.IsEnum)
            return new EnumPropertyEditor();
        if (property.PropertyType == typeof(string))
            return new TextPropertyEditor();
        if (property.PropertyType == typeof(int))
            return new NumericPropertyEditor();

        // Creating a default editor.
        return new DefaultPropertyEditor();
    }

    private static void BindEditor(Control editor, PropertyItem item)
    {
        // Automatic linking.
        if (editor is IPropertyEditor propertyEditor) {
            propertyEditor.Bind(item);
            return;
        }

        // Manual binding.
        var valueProperty = editor.GetType().GetProperty("Value")
            ?? editor.GetType().GetProperty("Text");

        if (valueProperty is null)
            return;

        var binding = new Binding("Value") {
            Mode = BindingMode.TwoWay
        };

        // editor.Bind(valueProperty.PropertyType, binding, this);

        throw new NotSupportedException("Manual binding is not supported.");
    }
}
