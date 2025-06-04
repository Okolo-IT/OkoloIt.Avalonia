using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OkoloIt.Avalonia.UiKit.Models;

public class Position : INotifyPropertyChanged
{
    private int _x;
    private int _y;

    public int X {
        get => _x;
        set {
            _x = value;
            OnPropertyChanged();
        }
    }

    public int Y {
        get => _y;
        set {
            _y = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
