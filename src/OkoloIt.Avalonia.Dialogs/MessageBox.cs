using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace OkoloIt.Avalonia.Dialogs;

public static class MessageBox
{
    public static string DefaultTitle { get; set; } = "";
    public static string DefaultInformationTitle { get; set; } = "Information";
    public static string DefaultQuestionTitle { get; set; } = "Question";
    public static string DefaultWarningTitle { get; set; } = "Warning";
    public static string DefaultErrorTitle { get; set; } = "Error";

    public static async Task<bool?> ShowInformationAsync(string message, string? title = default)
    {
        title ??= DefaultInformationTitle;
        return await ShowAsync(message, title, MessageBoxType.Information);
    }

    public static async Task<bool?> ShowQuestionAsync(string message, string? title = default)
    {
        title ??= DefaultQuestionTitle;
        return await ShowAsync(message, title, MessageBoxType.None);
    }

    public static async Task<bool?> ShowWarningAsync(string message, string? title = default)
    {
        title ??= DefaultWarningTitle;
        return await ShowAsync(message, title, MessageBoxType.None);
    }

    public static async Task<bool?> ShowErrorAsync(string message, string? title = default)
    {
        title ??= DefaultErrorTitle;
        return await ShowAsync(message, title, MessageBoxType.None);
    }

    public static async Task<bool?> ShowAsync(
        string message,
        string title = "",
        MessageBoxType messageType = MessageBoxType.None)
    {
        title ??= DefaultTitle;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            return await ShowForDesktopAsync(desktop, message, title, messageType);

        throw new NotSupportedException("Supported only desktop.");

        // if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        //     return await ShowForDesktopAsync();
    }

    private static async Task<bool?> ShowForDesktopAsync(
        IClassicDesktopStyleApplicationLifetime desktop,
        string message,
        string title,
        MessageBoxType messageType)
    {
        if (desktop.MainWindow is not Window owner)
            return null;

        Window dialog = CreateDialog(message, title, messageType);

        return await dialog.ShowDialog<bool?>(owner);
    }

    private static Window CreateDialog(
        string message,
        string title,
        MessageBoxType messageType)
    {
        Grid dialogSpace = new();
        dialogSpace.Children.Add(new TextBlock() {
            Text = $"[{messageType}] '{message}'"
        });

        Window dialog = new() {
            Title = title,
            Width = 400,
            Height = 300,
            Content = dialogSpace
        };

        return dialog;
    }
}
