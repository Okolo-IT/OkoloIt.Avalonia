using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
    private static readonly Dictionary<Type, Control> _editors = [];

    /// <summary>
    /// Registers a custom editor for a specific target property type.
    /// </summary>
    /// <typeparam name="TEditor">
    /// The editor type. Must inherit from <see cref="Control"/> and implement <see cref="IPropertyEditor"/>.
    /// </typeparam>
    /// <param name="targetType">he type of the property that the editor will handle.</param>
    public static void Register<TEditor>(Type targetType)
        where TEditor : Control, IPropertyEditor
    {
        var editor = Activator.CreateInstance<TEditor>();
        _editors.Add(targetType, editor);
    }

    /// <summary>
    /// Creates an editor for the property.
    /// </summary>
    /// <param name="property">Descriptor of the property for which the editor is created.</param>
    /// <returns>Editor for the property.</returns>
    public static Control CreateEditor(PropertyDescriptor property)
    {
        if (TryCreateDedicatedEditor(property, out Control? dedicatedEditor))
            return dedicatedEditor;

        if (TryCreateRegisteredEditor(property, out Control? registeredEditor))
            return registeredEditor;

        if (TryCreateInlineEditor(property, out Control? inlineEditor))
            return inlineEditor;

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
        throw new NotSupportedException("Manual binding is not supported.");
    }

    private static bool TryCreateDedicatedEditor(
        PropertyDescriptor property,
        [NotNullWhen(true)] out Control? dedicatedEditor)
    {
        var editorAttribute = property.Attributes.OfType<EditorAttribute>().FirstOrDefault();

        if (editorAttribute is null) {
            dedicatedEditor = null;
            return false;
        }

        var editorType = Type.GetType(editorAttribute.EditorTypeName, throwOnError: false);
        if (editorType is not null && Activator.CreateInstance(editorType) is Control attribytedEditor) {
            dedicatedEditor = attribytedEditor;
            return true;
        }

        var assembly = Assembly.GetEntryAssembly();
        var editorTypeInfo = assembly?.DefinedTypes
            .FirstOrDefault(t => t.Name.Equals(
                editorAttribute.EditorTypeName,
                StringComparison.OrdinalIgnoreCase));

        if (editorTypeInfo?.FullName is null) {
            dedicatedEditor = null;
            return false;
        }

        editorType = assembly?.GetType(editorTypeInfo.FullName);
        if (editorType is not null && Activator.CreateInstance(editorType) is Control namedEditor) {
            dedicatedEditor = namedEditor;
            return true;
        }

        dedicatedEditor = null;
        return false;
    }

    private static bool TryCreateRegisteredEditor(
        PropertyDescriptor property,
        [NotNullWhen(true)] out Control? registeredEditor)
    {
        if (_editors.TryGetValue(property.PropertyType, out Control? editor)) {
            registeredEditor = editor;
            return true;
        }

        registeredEditor = null;
        return false;
    }

    private static bool TryCreateInlineEditor(
        PropertyDescriptor property,
        [NotNullWhen(true)] out Control? inlineEditor)
    {
        if (property.PropertyType.IsBool()) {
            inlineEditor = new BooleanPropertyEditor();
            return true;
        }

        if (property.PropertyType.IsEnum) {
            inlineEditor = new EnumPropertyEditor();
            return true;
        }

        if (property.PropertyType.IsText()) {
            inlineEditor = new TextPropertyEditor();
            return true;
        }

        if (property.PropertyType.IsGuid()) {
            inlineEditor = new GuidPropertyEditor();
            return true;
        }

        if (property.PropertyType.IsDateTime()) {
            inlineEditor = new DateTimePropertyEditor();
            return true;
        }

        if (property.PropertyType.IsNumeric()) {
            inlineEditor = new NumericPropertyEditor();
            return true;
        }

        inlineEditor = null;
        return false;
    }
}
