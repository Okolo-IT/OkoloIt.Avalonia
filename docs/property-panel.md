# OkoloIt.Avalonia.Controls.PropertyPanel

This is a PropertyPanel implementation for [Avalonia](https://github.com/AvaloniaUI/Avalonia), you can use it in **Avalonia** Applications.

## Supported attributes

You can use the following attributes listed below to edit properties in the **PropertyPanel**:

- `System.ComponentModel.BrowsableAttribute`
- `System.ComponentModel.CategoryAttribut`
- `System.ComponentModel.DisplayNameAttribute`
- `System.ComponentModel.EditorAttribute`
- `System.ComponentModel.ReadOnlyAttribute`

## Supported types

PropertyPanel supports a small list of types, which is extended over time:

- `int`
- `bool`
- `string`
- `Guid`
- `Enum`

## How To Use

Install the Nuget package:

```shell
dotnet add package OkoloIt.Avalonia.Controls.PropertyPanel
```

Include `PropertyPanel` styles in your `App.axaml`:

```xml
<Application
    ...
    xmlns:okit="using:OkoloIt.Avalonia.Controls.Themes"
    >
    <Application.Styles>
        <okit:Fluent/>
    </Application.Styles>
</Application>
```

Implement inheritance from interface `INotifyPropertyChanged` for your model:

```cs
public class PropertyModel : INotifyPropertyChanged
{
    private string _name = string.Empty;

    public string Name {
        get => _name;
        set {
            _name = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

Add `PropertyPanel` to your view:

```xml
<Window
    ...
    xmlns:okit="using:OkoloIt.Avalonia.Controls"
    >

    <okit:PropertyPanel Content="{Binding PropertyModel}"/>
</Window>
```

[Read More](../samples/OkoloIt.Avalonia.UiKit)
