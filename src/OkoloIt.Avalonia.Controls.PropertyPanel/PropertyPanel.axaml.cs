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
    /// Defines the <see cref="Categories"/> property.
    /// </summary>
    public static readonly DirectProperty<PropertyPanel, IEnumerable<PropertyCategory>> CategoriesProperty
        = AvaloniaProperty.RegisterDirect<PropertyPanel, IEnumerable<PropertyCategory>>(
            nameof(Categories),
            o => o.Categories);

    /// <summary>
    /// Gets or sets the model that implements interface <see cref="INotifyPropertyChanged"/>,
    /// for whose properties the properties panel is initialized.
    /// </summary>
    public INotifyPropertyChanged? Content {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
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

        if (!change.Property.Equals(ContentProperty))
            return;

        CleanupOldCategories();
        UpdateProperties();
    }

    private void UpdateProperties()
    {
        if (Content is null) {
            Categories = [];
            return;
        }

        var properties = TypeDescriptor.GetProperties(Content)
            .OfType<PropertyDescriptor>()
            .Where(p => p.IsBrowsable)
            .Select(p => new PropertyItem(p, Content))
            .OrderBy(p => p.Order)
            .ThenBy(p => p.DisplayName)
            .ToList();

        Categories = properties
            .GroupBy(p => p.Category)
            .Select(g => new PropertyCategory(g.Key, g))
            .OrderBy(c => c.Name)
            .ToList();
    }

    private void CleanupOldCategories()
    {
        foreach (var category in _categories) {
            foreach (IDisposable disposable in category.Properties)
                disposable.Dispose();
        }
    }
}