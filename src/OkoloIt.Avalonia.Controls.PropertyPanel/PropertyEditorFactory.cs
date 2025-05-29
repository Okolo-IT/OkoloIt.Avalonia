using System.ComponentModel;

using Avalonia.Controls;
using Avalonia.Data;

using OkoloIt.Avalonia.Controls.Editors;

namespace OkoloIt.Avalonia.Controls;

public static class PropertyEditorFactory
{
    public static Control Create(PropertyDescriptor property, PropertyItem item)
    {
        var editor = CreateEditor(property, item);
        BindEditor(editor, item);

        return editor;
    }

    private static Control CreateEditor(PropertyDescriptor property, PropertyItem item)
    {
        // Создание специального редактора свойства.
        var editorAttribute = property.Attributes.OfType<EditorAttribute>().FirstOrDefault();

        if (editorAttribute is not null) {
            var editorType = Type.GetType(editorAttribute.EditorTypeName);

            if (editorType is not null) {
                var instance = Activator.CreateInstance(editorType) as Control;

                if (instance is Control editor)
                    return editor;
            }
        }

        // Использование встроенных редакторов.
        if (property.PropertyType == typeof(bool))
            return new BooleanPropertyEditor();
        if (property.PropertyType.IsEnum)
            return new EnumPropertyEditor();
        if (property.PropertyType == typeof(string))
            return new TextPropertyEditor();
        if (property.PropertyType == typeof(int))
            return new NumericPropertyEditor();

        // Использование редактора по умолчанию.
        return new DefaultPropertyEditor();
    }

    private static void BindEditor(Control editor, PropertyItem item)
    {
        // Автоматическое связывание.
        if (editor is IPropertyEditor propertyEditor) {
            propertyEditor.Bind(item);
            return;
        }

        // Ручное связывание.
        var valueProperty = editor.GetType().GetProperty("Value")
            ?? editor.GetType().GetProperty("Text");

        if (valueProperty is null)
            return;

        var binding = new Binding("Value") {
            Mode = BindingMode.TwoWay
        };

        // editor.Bind(valueProperty.PropertyType, binding, this);
    }
}
