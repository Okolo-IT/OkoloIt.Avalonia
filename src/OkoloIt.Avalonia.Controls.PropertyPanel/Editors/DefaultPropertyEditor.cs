using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Default property editor.
/// </summary>
internal class DefaultPropertyEditor : Grid, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor.
    /// </summary>
    internal DefaultPropertyEditor()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;

        ColumnSpacing = 5;
        ColumnDefinitions = [
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Auto),
        ];
    }

    /// <inheritdoc/>
    public void Bind(PropertyItem property)
    {
        // Text value of the object.
        TextBox textBox = new() {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            IsReadOnly = property.IsReadOnly,
            Text = property.Value?.ToString(),
        };

        textBox.Bind(TextBox.TextProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Children.Add(textBox);
        Grid.SetColumn(textBox, 0);

        // Button for complex objects.
        if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string)) {
            var button = new Button {
                Content = "...",
                Width = 30
            };

            button.Click += OnEditComplexObject;
            Children.Add(button);
            Grid.SetColumn(button, 1);
        }
    }

    private void OnEditComplexObject(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException("Editing complex objects in development.");
    }
}
