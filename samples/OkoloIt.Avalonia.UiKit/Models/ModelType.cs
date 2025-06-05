using System.ComponentModel;

namespace OkoloIt.Avalonia.UiKit.Models;

public enum ModelType
{
    Default,

    [Description("Fluent type")]
    Fluent,

    [Description("Simple type")]
    Simple,
}
