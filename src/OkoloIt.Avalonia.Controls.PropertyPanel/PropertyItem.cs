using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// A property element representing its display in the interface.
/// </summary>
public class PropertyItem : INotifyPropertyChanged
{
    private readonly PropertyDescriptor _descriptor;
    private readonly INotifyPropertyChanged _instance;
    private Control? _editor;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Creates an instance of a property element representing its mapping in the interface.
    /// </summary>
    /// <param name="descriptor">Property descriptor.</param>
    /// <param name="instance">
    /// Model whose properties will be displayed, implementing interface
    /// <see cref="INotifyPropertyChanged"/>.
    /// and inherits the <see cref="Control"/> class.</param>
    public PropertyItem(PropertyDescriptor descriptor, INotifyPropertyChanged instance)
    {
        _descriptor = descriptor;
        _instance = instance;

        _instance.PropertyChanged += OnInstancePropertyChanged;
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public string Name => _descriptor.Name;

    /// <summary>
    /// Gets the name that can be displayed in a window.
    /// </summary>
    public string DisplayName => _descriptor.DisplayName;

    /// <summary>
    ///  Gets the name of the category to which the property belongs.
    /// </summary>
    public string Category => _descriptor.Category;

    /// <summary>
    /// Gets the description of the property.
    /// </summary>
    public string Description => _descriptor.Description;

    /// <summary>
    /// Gets a value indicating whether this property is read-only.
    /// </summary>
    public bool IsReadOnly => _descriptor.IsReadOnly;

    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    public Type PropertyType => _descriptor.PropertyType;

    /// <summary>
    /// Editor for the property.
    /// </summary>
    public Control Editor => _editor ?? CreateEditor();

    /// <summary>
    /// Property value.
    /// </summary>
    public object? Value {
        get {
            var value = _descriptor.GetValue(_instance);

            return Editor is IValueConverter converter
                ? converter.Convert(value, _descriptor.PropertyType, null, CultureInfo.CurrentCulture)
                : value;
        }
        set {
            object? convertedValue = Editor is IValueConverter converter
                ? converter.ConvertBack(value, _descriptor.PropertyType, null, CultureInfo.CurrentCulture)
                : Converters.DefaultValueConverter.ConvertValue(value, _descriptor.PropertyType);

            _descriptor.SetValue(_instance, convertedValue);
            OnPropertyChanged(nameof(Value));
        }
    }

    /// <summary>
    /// Called when a property changes on the object.
    /// </summary>
    /// <param name="propertyName">Changed property name.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnInstancePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == _descriptor.Name) {
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(Editor));
        }
    }

    private Control CreateEditor()
    {
        _editor = PropertyEditorFactory.CreateEditor(_descriptor);
        PropertyEditorFactory.BindEditor(_editor, this);
        return _editor;
    }
}
