using System.Windows.Input;

using Avalonia.Controls;

namespace OkoloIt.Avalonia.Dialogs.Commands;

internal class OkCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter is Window window)
            window.Close(true);
    }
}
