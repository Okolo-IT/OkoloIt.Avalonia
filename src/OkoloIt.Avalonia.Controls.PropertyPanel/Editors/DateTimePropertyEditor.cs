using System.Globalization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

file class TimeSpanObserver(PropertyItem property) : IObserver<TimeSpan?>
{
    private readonly PropertyItem _property = property;

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TimeSpan? value)
    {
        if (value is null || _property.Value is not DateTimeOffset dateTime)
            return;

        var newDateTime = dateTime.Date + value;

        if (newDateTime is not null)
            _property.Value = new DateTimeOffset(newDateTime.Value);
    }
}

/// <summary>
/// Property editor for a date or time types.
/// </summary>
internal class DateTimePropertyEditor : Grid, IPropertyEditor, IValueConverter
{
    private Type _propertyType = default!;

    /// <summary>
    /// Creates an instance of the default property editor for a date or time types.
    /// </summary>
    internal DateTimePropertyEditor()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        _propertyType = property.PropertyType;

        if (property.PropertyType == typeof(DateTime)) {
            RowSpacing = 4;

            RowDefinitions = [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Auto),
            ];

            DatePicker datePicker = CreateDatePicker(nameof(PropertyItem.Value));
            SetRow(datePicker, 0);

            TimePicker timePicker = CreateTimePicker($"{nameof(PropertyItem.Value)}.{nameof(DateTime.TimeOfDay)}");
            timePicker.GetObservable(TimePicker.SelectedTimeProperty)
                .Subscribe(new TimeSpanObserver(property));

            SetRow(timePicker, 1);
        }

#if NET6_0_OR_GREATER
        if (property.PropertyType == typeof(DateOnly))
            CreateDatePicker(nameof(PropertyItem.Value));
        if (property.PropertyType == typeof(TimeOnly))
            CreateTimePicker(nameof(PropertyItem.Value));
#endif

        if (property.PropertyType == typeof(TimeSpan))
            CreateTimePicker(nameof(PropertyItem.Value));
    }

    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (_propertyType == typeof(DateTime) && value is DateTime dateTime)
            return new DateTimeOffset(dateTime);

#if NET6_0_OR_GREATER
        if (_propertyType == typeof(DateOnly) && value is DateOnly dateOnly)
            return new DateTimeOffset(dateOnly.ToDateTime(new TimeOnly()));
        if (_propertyType == typeof(TimeOnly) && value is TimeOnly timeOnly)
            return timeOnly.ToTimeSpan();
#endif

        return value;
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan) {
#if NET6_0_OR_GREATER
            if (_propertyType == typeof(TimeOnly))
                return new TimeOnly(timeSpan.Ticks);
#endif
            return new TimeSpan(timeSpan.Ticks);
        }

        if (value is not DateTimeOffset dateTime)
            return default;

#if NET6_0_OR_GREATER
        if (_propertyType == typeof(DateOnly))
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
#endif

        if (_propertyType == typeof(DateTime))
            return dateTime.DateTime;

        return default;
    }

    private DatePicker CreateDatePicker(string bindingPath)
    {
        DatePicker datePicker = new();
        datePicker.Bind(DatePicker.SelectedDateProperty, new Binding(bindingPath) {
            Mode = BindingMode.TwoWay
        });

        Children.Add(datePicker);

        return datePicker;
    }

    private TimePicker CreateTimePicker(string bindingPath)
    {
        TimePicker timePicker = new();

        timePicker.Bind(TimePicker.SelectedTimeProperty, new Binding(bindingPath) {
            Mode = BindingMode.TwoWay
        });

        Children.Add(timePicker);

        return timePicker;
    }
}
