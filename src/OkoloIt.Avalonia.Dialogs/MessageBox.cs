using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using OkoloIt.Avalonia.Dialogs.Commands;

namespace OkoloIt.Avalonia.Dialogs;

public static class MessageBox
{
    public static async Task<bool?> ShowInformationAsync(string message, string? title = default)
    {
        title ??= MessageBoxProperties.DefaultInformationTitle;
        return await ShowAsync(message, title, MessageBoxType.Information);
    }

    public static async Task<bool?> ShowQuestionAsync(string message, string? title = default)
    {
        title ??= MessageBoxProperties.DefaultQuestionTitle;
        return await ShowAsync(message, title, MessageBoxType.Question);
    }

    public static async Task<bool?> ShowWarningAsync(string message, string? title = default)
    {
        title ??= MessageBoxProperties.DefaultWarningTitle;
        return await ShowAsync(message, title, MessageBoxType.Warning);
    }

    public static async Task<bool?> ShowErrorAsync(string message, string? title = default)
    {
        title ??= MessageBoxProperties.DefaultErrorTitle;
        return await ShowAsync(message, title, MessageBoxType.Error);
    }

    public static async Task<bool?> ShowAsync(
        string message,
        string title = "",
        MessageBoxType messageType = MessageBoxType.None)
    {
        title ??= MessageBoxProperties.DefaultTitle;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            return await ShowForDesktopAsync(desktop, message, title, messageType);

        throw new NotSupportedException("Supported only desktop.");
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
        Dialog dialogSpace = new(messageType, title, message) {
            OkCommand     = new OkCommand(),
            YesCommand    = new YesCommand(),
            NoCommand     = new NoCommand(),
            CancelCommand = new CancelCommand()
        };

        Window dialog = new() {
            Title = title,
            MaxWidth = 400,
            MaxHeight = 500,
            Content = dialogSpace,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            SizeToContent = SizeToContent.WidthAndHeight,
        };

        return dialog;
    }
}
