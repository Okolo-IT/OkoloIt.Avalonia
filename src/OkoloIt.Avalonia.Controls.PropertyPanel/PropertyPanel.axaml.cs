using System.ComponentModel;

using Avalonia;
using Avalonia.Controls.Primitives;

namespace OkoloIt.Avalonia.Controls;

public class PropertyPanel : TemplatedControl
{
    public static readonly StyledProperty<INotifyPropertyChanged?> ContentProperty
        = AvaloniaProperty.Register<PropertyPanel, INotifyPropertyChanged?>(nameof(ContentProperty));

    public static readonly DirectProperty<PropertyPanel, IEnumerable<PropertyCategory>> CategoriesProperty
        = AvaloniaProperty.RegisterDirect<PropertyPanel, IEnumerable<PropertyCategory>>(nameof(CategoriesProperty), o => o.Categories);

    private IEnumerable<PropertyCategory> _categories = [];

    public INotifyPropertyChanged? Content {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public IEnumerable<PropertyCategory> Categories {
        get => _categories;
        set => SetAndRaise(CategoriesProperty, ref _categories, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (!change.Property.Equals(ContentProperty))
            return;

        UpdateProperties();
    }

    private void UpdateProperties()
    {
        if (Content is null)
            return;

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