using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;

using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// A property element representing its display in the interface.
/// Supports binding to one or many target instances at once (multi-selection editing).
/// </summary>
public class PropertyItem : INotifyPropertyChanged, IDisposable
{
    private readonly PropertyDescriptor _descriptor;
    private readonly List<Target> _targets = [];
    private Control? _editor;
    private bool _disposed = false;
    private bool _hasMultipleValues = false;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Creates an instance of a property element bound to a single model.
    /// </summary>
    /// <param name="descriptor">Property descriptor.</param>
    /// <param name="instance">
    /// Model whose property will be displayed, implementing interface
    /// <see cref="INotifyPropertyChanged"/>.
    /// </param>
    public PropertyItem(PropertyDescriptor descriptor, INotifyPropertyChanged instance)
        : this(descriptor, [instance]) { }

    /// <summary>
    /// Creates an instance of a property element bound to multiple models.
    /// </summary>
    /// <param name="descriptor">Representative property descriptor used for metadata.</param>
    /// <param name="instances">Models whose shared property will be displayed and edited.</param>
    public PropertyItem(PropertyDescriptor descriptor, IEnumerable<INotifyPropertyChanged> instances)
    {
        _descriptor = descriptor;

        foreach (INotifyPropertyChanged instance in instances) {
            var target = new Target(ResolveDescriptor(instance), instance);
            target.Instance.PropertyChanged += OnInstancePropertyChanged;
            _targets.Add(target);
        }

        Order = descriptor.Attributes
            .OfType<DisplayAttribute>()
            .FirstOrDefault()?
            .GetOrder() ?? int.MinValue;
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
    /// Gets or sets order the property.
    /// </summary>
    public int Order { get; } = 0;

    /// <summary>
    /// Gets a value indicating whether this property is read-only.
    /// </summary>
    public bool IsReadOnly => _descriptor.IsReadOnly;

    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    public Type PropertyType => _descriptor.PropertyType;

    /// <summary>
    /// Gets a value indicating whether the bound targets currently hold different values.
    /// </summary>
    public bool HasMultipleValues {
        get => _hasMultipleValues;
        private set {
            if (_hasMultipleValues == value)
                return;

            _hasMultipleValues = value;
            OnPropertyChanged(nameof(HasMultipleValues));
            OnPropertyChanged(nameof(Watermark));
        }
    }

    /// <summary>
    /// Gets or sets the text shown by editors when bound targets hold different values.
    /// Supplied by the owner (<see cref="PropertyPanel"/>) so the library stays free of
    /// hard-coded, locale-specific text.
    /// </summary>
    public string MixedValuesWatermark { get; set; } = string.Empty;

    /// <summary>
    /// Gets the placeholder text shown by editors when values differ across targets.
    /// </summary>
    public string Watermark => HasMultipleValues ? MixedValuesWatermark : string.Empty;

    /// <summary>
    /// Editor for the property.
    /// </summary>
    public Control Editor => _editor ?? CreateEditor();

    /// <summary>
    /// Gets the collection of attributes for the property.
    /// </summary>
    public AttributeCollection Attributes => _descriptor.Attributes;

    /// <summary>
    /// Property value. Returns <see langword="null"/> when bound targets hold different values.
    /// Setting the value writes it to every bound target.
    /// </summary>
    public object? Value {
        get {
            if (_targets.Count == 0)
                return null;

            object? firstRaw = _targets[0].Descriptor.GetValue(_targets[0].Instance);
            bool mixed = false;

            for (int i = 1; i < _targets.Count; i++) {
                object? raw = _targets[i].Descriptor.GetValue(_targets[i].Instance);

                if (!Equals(raw, firstRaw)) {
                    mixed = true;
                    break;
                }
            }

            HasMultipleValues = mixed;

            if (mixed)
                return null;

            return Editor is IValueConverter converter
                ? converter.Convert(firstRaw, _descriptor.PropertyType, null, CultureInfo.CurrentCulture)
                : firstRaw;
        }
        set {
            object? convertedValue = Editor is IValueConverter converter
                ? converter.ConvertBack(value, _descriptor.PropertyType, null, CultureInfo.CurrentCulture)
                : Converters.DefaultValueConverter.ConvertValue(value, _descriptor.PropertyType);

            foreach (Target target in _targets)
                target.Descriptor.SetValue(target.Instance, convertedValue);

            OnPropertyChanged(nameof(Value));
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the object's resources.
    /// </summary>
    /// <param name="disposing">
    /// <see langword="true"/> if called from Dispose();
    /// <see langword="false"/> if called from a finalizer.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing) {
            foreach (Target target in _targets)
                target.Instance.PropertyChanged -= OnInstancePropertyChanged;

            _targets.Clear();
        }

        _disposed = true;
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
        if (_disposed)
            return;

        if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == _descriptor.Name) {
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(HasMultipleValues));
            OnPropertyChanged(nameof(Watermark));
            OnPropertyChanged(nameof(Editor));
        }
    }

    private Control CreateEditor()
    {
        _editor = PropertyEditorFactory.CreateEditor(_descriptor);
        PropertyEditorFactory.BindEditor(_editor, this);
        return _editor;
    }

    private PropertyDescriptor ResolveDescriptor(INotifyPropertyChanged instance)
    {
        return TypeDescriptor.GetProperties(instance).Find(_descriptor.Name, true) ?? _descriptor;
    }

    private sealed record Target(PropertyDescriptor Descriptor, INotifyPropertyChanged Instance);
}
