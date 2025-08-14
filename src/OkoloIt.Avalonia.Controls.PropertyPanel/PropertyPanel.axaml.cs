using System.ComponentModel;

using Avalonia;
using Avalonia.Controls.Primitives;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// Property panel.
/// </summary>
public class PropertyPanel : TemplatedControl
{
    /// <summary>
    /// Defines the <see cref="Content"/> property.
    /// </summary>
    public static readonly StyledProperty<INotifyPropertyChanged?> ContentProperty
        = AvaloniaProperty.Register<PropertyPanel, INotifyPropertyChanged?>(nameof(ContentProperty));

    /// <summary>
    /// Defines the <see cref="Categories"/> property.
    /// </summary>
    public static readonly DirectProperty<PropertyPanel, IEnumerable<PropertyCategory>> CategoriesProperty
        = AvaloniaProperty.RegisterDirect<PropertyPanel, IEnumerable<PropertyCategory>>(
            nameof(CategoriesProperty),
            o => o.Categories);

    private IEnumerable<PropertyCategory> _categories = [];

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

    /// <inheritdoc/>
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (!change.Property.Equals(ContentProperty))
            return;

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
            .ToList();

        Categories = properties
            .GroupBy(p => p.Category)
            .Select(g => new PropertyCategory(g.Key, g.OrderBy(p => p.DisplayName)))
            .OrderBy(c => c.Name)
            .ToList();
    }
}