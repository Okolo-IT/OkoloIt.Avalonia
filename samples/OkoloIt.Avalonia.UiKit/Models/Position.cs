using System.ComponentModel;
using System.Runtime.CompilerServices;

using OkoloIt.Avalonia.Controls.Editors;
using OkoloIt.Avalonia.UiKit.Editors;

namespace OkoloIt.Avalonia.UiKit.Models;

[Editor(typeof(PositionPropertyEditor), typeof(IPropertyEditor))]
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
