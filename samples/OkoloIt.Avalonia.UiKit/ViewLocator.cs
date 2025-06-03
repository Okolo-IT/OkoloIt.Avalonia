using Avalonia.Controls;
using Avalonia.Controls.Templates;

using OkoloIt.Avalonia.UiKit.ViewModels;

namespace OkoloIt.Avalonia.UiKit;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        return type is not null
            ? (Control?)Activator.CreateInstance(type)
            : new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}