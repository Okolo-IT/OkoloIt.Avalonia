using System.ComponentModel;
using System.Runtime.CompilerServices;

using Avalonia.Controls;

namespace OkoloIt.Avalonia.Controls;

public class PropertyItem : INotifyPropertyChanged
{
    private readonly PropertyDescriptor _descriptor;
    private readonly INotifyPropertyChanged _instance;
    private Control? _editor;

    public event PropertyChangedEventHandler? PropertyChanged;

    public PropertyItem(PropertyDescriptor descriptor, INotifyPropertyChanged instance)
    {
        _descriptor = descriptor;
        _instance = instance;

        _instance.PropertyChanged += OnInstancePropertyChanged;
    }

    public string Name => _descriptor.Name;
    public string DisplayName => _descriptor.DisplayName;
    public string Category => _descriptor.Category;
    public string Description => _descriptor.Description;
    public bool IsReadOnly => _descriptor.IsReadOnly;
    public Type PropertyType => _descriptor.PropertyType;

    public Control Editor => _editor ??= CreateEditor();

    public object? Value {
        get => _descriptor.GetValue(_instance);
        set {
            var convertedValue = ConvertValue(value, _descriptor.PropertyType);
            _descriptor.SetValue(_instance, convertedValue);
            OnPropertyChanged(nameof(Value));
        }
    }

    private object? ConvertValue(object? value, Type targetType)
    {
        if (value == null)
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

        // Если тип уже соответствует.
        if (targetType.IsInstanceOfType(value))
            return value;

        // Особые случаи для числовых типов.
        if (targetType == typeof(int)) {
            if (value is decimal decimalValue)
                return (int)decimalValue;

            if (value is string strValue && int.TryParse(strValue, out var intValue))
                return intValue;
        }

        // Стандартное преобразование.
        return Convert.ChangeType(value, targetType);
    }

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
        var editor = PropertyEditorFactory.Create(_descriptor, this);
        editor.DataContext = this;
        return editor;
    }
}
