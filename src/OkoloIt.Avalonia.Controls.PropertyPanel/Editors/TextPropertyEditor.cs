﻿using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace OkoloIt.Avalonia.Controls.Editors;

/// <summary>
/// Property editor for a <see langword="string"/> type.
/// </summary>
internal class TextPropertyEditor : ContentControl, IPropertyEditor
{
    /// <summary>
    /// Creates an instance of the default property editor for a <see langword="string"/> type.
    /// </summary>
    internal TextPropertyEditor()
    {
        VerticalAlignment = VerticalAlignment.Center;
        HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    /// <inheritdoc/>
    public virtual void Bind(PropertyItem property)
    {
        TextBox textBox = new() {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Text = property.Value?.ToString(),
            IsReadOnly = property.IsReadOnly,
        };

        textBox.Bind(TextBox.TextProperty, new Binding(nameof(PropertyItem.Value)) {
            Mode = BindingMode.TwoWay
        });

        Content = textBox;
    }
}
