using Avalonia;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// An category in a <see cref="PropertyPanel"/>.
/// </summary>
/// <param name="name">Category name.</param>
/// <param name="properties">List of category properties.</param>
public class PropertyCategory(string name, IEnumerable<PropertyItem> properties) : AvaloniaObject
{
    /// <summary>
    /// Defines the <see cref="Name"/> property.
    /// </summary>
    public static readonly DirectProperty<PropertyCategory, string> NameProperty
        = AvaloniaProperty.RegisterDirect<PropertyCategory, string>(
            nameof(Name),
            o => o.Name);

    /// <summary>
    /// Defines the <see cref="Properties"/> property.
    /// </summary>
    public static readonly DirectProperty<PropertyCategory, IEnumerable<PropertyItem>> PropertiesProperty
        = AvaloniaProperty.RegisterDirect<PropertyCategory, IEnumerable<PropertyItem>>(
            nameof(Properties),
            o => o.Properties);

    private string _name = name;
    private IEnumerable<PropertyItem> _properties = properties;

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name {
        get => _name;
        private set => SetAndRaise(NameProperty, ref _name, value);
    }

    /// <summary>
    /// Gets or sets a list of category properties.
    /// </summary>
    public IEnumerable<PropertyItem> Properties {
        get => _properties;
        private set => SetAndRaise(PropertiesProperty, ref _properties, value);
    }
}
