using Avalonia.Data.Converters;

using OkoloIt.Avalonia.Dialogs.Converters;

namespace OkoloIt.Avalonia.Dialogs;

public static class MessageBoxProperties
{
    public static string DefaultTitle { get; set; } = "";
    public static string DefaultInformationTitle { get; set; } = "Information";
    public static string DefaultQuestionTitle { get; set; } = "Question";
    public static string DefaultWarningTitle { get; set; } = "Warning";
    public static string DefaultErrorTitle { get; set; } = "Error";

    public static string OkText { get; set; } = "OK";
    public static string YesText { get; set; } = "Yes";
    public static string NoText { get; set; } = "No";
    public static string CancelText { get; set; } = "Cancel";

    public static IValueConverter IconConverter { get; set; } = new DefaultMessageBoxTypeToIconConverter();
}
