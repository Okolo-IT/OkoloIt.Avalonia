using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Default property editor.
/// </summary>
internal class DefaultPropertyEditor : StackPanel, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor.
    /// </summary>
    internal DefaultPropertyEditor()
    {
        Orientation = Orientation.Horizontal;
        Spacing = 5;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public void Bind(PropertyItem property)
    {
        TextBox textBox = new() {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            IsReadOnly = property.IsReadOnly,
            Text = property.Value?.ToString(),
        };

        Children.Add(textBox);

        // Кнопка для сложных объектов
        if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string)) {
            var button = new Button {
                Content = "...",
                Width = 30
            };

            button.Click += OnEditComplexObject;
            Children.Add(button);
        }

        textBox.Bind(TextBox.TextProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });
    }

    private void OnEditComplexObject(object? sender, RoutedEventArgs e)
    {
        // Реализация редактирования сложных объектов
        // Можно открыть диалог или встроенный PropertyGrid
    }
}
