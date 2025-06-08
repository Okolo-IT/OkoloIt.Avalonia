using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OkoloIt.Avalonia.UiKit.Models;

public class PropertyModel : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private bool _boolValue;
    private int _intValue;
    private ModelType _type;

    private Position _position = new() {
        X = 10,
        Y = 20,
    };

    public Guid Id { get; } = Guid.NewGuid();

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

    [DisplayName("Number")]
    [Category("Numbers")]
    public int IntValue {
        get => _intValue;
        set {
            _intValue = value;
            OnPropertyChanged();
        }
    }

    [Category("Numbers")]
    public ushort UShortValue { get; set; }

    [Category("Numbers")]
    public double DoubleValue { get; set; }

    [Browsable(false)]
    public string IgnoredValue { get; set; } = string.Empty;

    [Category("Layout")]
    public Position Position {
        get => _position;
        set {
            _position = value;
            OnPropertyChanged();
        }
    }

    public ModelType Type {
        get => _type;
        set {
            _type = value;
            OnPropertyChanged();
        }
    }

    [Category("Date & time")]
    public DateTime FullDate { get; set; } = DateTime.Now;

    [Category("Date & time")]
    public TimeOnly Time { get; set; } = new TimeOnly(12, 55);

    [Category("Date & time")]
    public DateOnly Date { get; set; } = new DateOnly(2025, 12, 01);

    [Category("Date & time")]
    public TimeSpan TimeSpan { get; set; } = DateTime.Now.TimeOfDay;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
