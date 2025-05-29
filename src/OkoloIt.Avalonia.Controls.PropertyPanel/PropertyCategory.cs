using Avalonia;

namespace OkoloIt.Avalonia.Controls;

public class PropertyCategory(string name, IEnumerable<PropertyItem> properties) : AvaloniaObject
{
    public static readonly DirectProperty<PropertyCategory, string> NameProperty
        = AvaloniaProperty.RegisterDirect<PropertyCategory, string>(
            nameof(Name),
            o => o.Name);

    public static readonly DirectProperty<PropertyCategory, IEnumerable<PropertyItem>> PropertiesProperty
        = AvaloniaProperty.RegisterDirect<PropertyCategory, IEnumerable<PropertyItem>>(
            nameof(Properties),
            o => o.Properties);

    private string _name = name;
    private IEnumerable<PropertyItem> _properties = properties;

    public string Name {
        get => _name;
        private set => SetAndRaise(NameProperty, ref _name, value);
    }

    public IEnumerable<PropertyItem> Properties {
        get => _properties;
        private set => SetAndRaise(PropertiesProperty, ref _properties, value);
    }
}
