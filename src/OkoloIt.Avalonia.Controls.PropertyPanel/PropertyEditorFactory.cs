using System.ComponentModel;
using System.Numerics;

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
    /// <returns>Editor for the property.</returns>
    public static Control CreateEditor(PropertyDescriptor property)
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
        if (property.PropertyType == typeof(Guid))
            return new GuidPropertyEditor();

        if (   property.PropertyType == typeof(int)
            || property.PropertyType == typeof(decimal)
            || property.PropertyType == typeof(uint)
            || property.PropertyType == typeof(long)
            || property.PropertyType == typeof(ulong)
            || property.PropertyType == typeof(short)
            || property.PropertyType == typeof(ushort)
            || property.PropertyType == typeof(byte)
            || property.PropertyType == typeof(sbyte)
            || property.PropertyType == typeof(float)
            || property.PropertyType == typeof(double)) {
            return new NumericPropertyEditor();
        }

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
