using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace OkoloIt.Avalonia.Controls.Themes;

public class Fluent : Styles
{

    public Fluent(IServiceProvider? serviceProvider = null)
    {
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
