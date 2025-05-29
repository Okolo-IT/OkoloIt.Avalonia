using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace OkoloIt.Avalonia.Controls.Themes;

/// <summary>
/// Includes the labs control themes in an application.
/// </summary>
public class Fluent : Styles
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Fluent"/> class.
    /// </summary>
    /// <param name="serviceProvider">The parent's service provider.</param>

    public Fluent(IServiceProvider? serviceProvider = default)
    {
        AvaloniaXamlLoader.Load(serviceProvider, this);
    }
}
