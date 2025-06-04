using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace OkoloIt.Avalonia.UiKit.Models;

public class PropertyModel : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private bool _boolValue;
    private int _intValue;
    private string _ignoredValue = string.Empty;
    private Position _position;

    public string Name {
        get => _name;
        set {
            _name = value;
            OnPropertyChanged();
        }
    }

    public bool BoolValue {
        get => _boolValue;
        set {
            _boolValue = value;
            OnPropertyChanged();
        }
    }

    public int IntValue {
        get => _intValue;
        set {
            _intValue = value;
            OnPropertyChanged();
        }
    }

    [IgnoreDataMember]
    public string IgnoredValue {
        get => _ignoredValue;
        set {
            _ignoredValue = value;
            OnPropertyChanged();
        }
    }

    public Position Position {
        get => _position;
        set {
            _position = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
