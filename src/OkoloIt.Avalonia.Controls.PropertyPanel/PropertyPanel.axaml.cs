using System.ComponentModel;

using Avalonia;
using Avalonia.Controls.Primitives;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// Property panel.
/// </summary>
public class PropertyPanel : TemplatedControl
{
    private IEnumerable<PropertyCategory> _categories = [];

    /// <summary>
    /// Defines the <see cref="Content"/> property.
    /// </summary>
    public static readonly StyledProperty<INotifyPropertyChanged?> ContentProperty
        = AvaloniaProperty.Register<PropertyPanel, INotifyPropertyChanged?>(nameof(Content));

    /// <summary>
    /// Defines the <see cref="Targets"/> property.
    /// </summary>
    public static readonly StyledProperty<IEnumerable<INotifyPropertyChanged>?> TargetsProperty
        = AvaloniaProperty.Register<PropertyPanel, IEnumerable<INotifyPropertyChanged>?>(nameof(Targets));

    /// <summary>
    /// Defines the <see cref="MixedValuesWatermark"/> property.
    /// </summary>
    public static readonly StyledProperty<string> MixedValuesWatermarkProperty
        = AvaloniaProperty.Register<PropertyPanel, string>(nameof(MixedValuesWatermark), string.Empty);

    /// <summary>
    /// Defines the <see cref="Categories"/> property.
    /// </summary>
    public static readonly DirectProperty<PropertyPanel, IEnumerable<PropertyCategory>> CategoriesProperty
        = AvaloniaProperty.RegisterDirect<PropertyPanel, IEnumerable<PropertyCategory>>(
            nameof(Categories),
            o => o.Categories);

    /// <summary>
    /// Gets or sets the single model that implements interface <see cref="INotifyPropertyChanged"/>,
    /// for whose properties the properties panel is initialized.
    /// </summary>
    public INotifyPropertyChanged? Content {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the multiple models for whose shared properties the panel is initialized.
    /// Takes precedence over <see cref="Content"/> when it contains at least one element.
    /// </summary>
    public IEnumerable<INotifyPropertyChanged>? Targets {
        get => GetValue(TargetsProperty);
        set => SetValue(TargetsProperty, value);
    }

    /// <summary>
    /// Gets or sets the text shown by editors when the selected targets hold different values.
    /// </summary>
    public string MixedValuesWatermark {
        get => GetValue(MixedValuesWatermarkProperty);
        set => SetValue(MixedValuesWatermarkProperty, value);
    }

    /// <summary>
    /// Gets or sets the property categories.
    /// </summary>
    public IEnumerable<PropertyCategory> Categories {
        get => _categories;
        set => SetAndRaise(CategoriesProperty, ref _categories, value);
    }

    /// <summary>
    /// Optional: Explicit disposal method for the panel itself.
    /// Call this if the panel is manually removed or disposed by the parent.
    /// </summary>
    public void Dispose()
    {
        CleanupOldCategories();
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property.Equals(ContentProperty) || change.Property.Equals(TargetsProperty)) {
            CleanupOldCategories();
            UpdateProperties();
            return;
        }

        if (change.Property.Equals(MixedValuesWatermarkProperty))
            ApplyMixedValuesWatermark();
    }

    private void UpdateProperties()
    {
        List<INotifyPropertyChanged> targets = ResolveTargets();

        if (targets.Count == 0) {
            Categories = [];
            return;
        }

        List<PropertyItem> properties = targets.Count == 1
            ? BuildSingle(targets[0])
            : BuildShared(targets);

        foreach (PropertyItem item in properties)
            item.MixedValuesWatermark = MixedValuesWatermark;

        Categories = properties
            .OrderBy(p => p.Order)
            .ThenBy(p => p.DisplayName)
            .GroupBy(p => p.Category)
            .Select(g => new PropertyCategory(g.Key, g))
            .OrderBy(c => c.Name)
            .ToList();
    }

    private void ApplyMixedValuesWatermark()
    {
        foreach (var category in _categories) {
            foreach (PropertyItem item in category.Properties)
                item.MixedValuesWatermark = MixedValuesWatermark;
        }
    }

    private List<INotifyPropertyChanged> ResolveTargets()
    {
        var targets = Targets?.Where(x => x is not null).Distinct().ToList();

        if (targets is { Count: > 0 })
            return targets;

        return Content is not null
            ? [Content]
            : [];
    }

    private static List<PropertyItem> BuildSingle(INotifyPropertyChanged instance)
    {
        return TypeDescriptor.GetProperties(instance)
            .OfType<PropertyDescriptor>()
            .Where(p => p.IsBrowsable)
            .Select(p => new PropertyItem(p, instance))
            .ToList();
    }

    private static List<PropertyItem> BuildShared(List<INotifyPropertyChanged> targets)
    {
        var propertyMaps = targets
            .Select(instance => TypeDescriptor.GetProperties(instance)
                .OfType<PropertyDescriptor>()
                .Where(p => p.IsBrowsable)
                .ToDictionary(p => p.Name, p => p))
            .ToList();

        var shared = propertyMaps
            .Aggregate((a, b) => a.Keys.Intersect(b.Keys).ToDictionary(k => k, k => a[k]));

        return shared.Values
            .Where(descriptor => SamePropertyType(descriptor.Name, descriptor.PropertyType, targets))
            .Select(descriptor => new PropertyItem(descriptor, targets))
            .ToList();
    }

    private static bool SamePropertyType(string name, Type type, List<INotifyPropertyChanged> targets)
    {
        foreach (INotifyPropertyChanged instance in targets) {
            var descriptor = TypeDescriptor.GetProperties(instance).Find(name, true);

            if (descriptor is null || descriptor.PropertyType != type)
                return false;
        }

        return true;
    }

    private void CleanupOldCategories()
    {
        foreach (var category in _categories) {
            foreach (IDisposable disposable in category.Properties)
                disposable.Dispose();
        }
    }
}
