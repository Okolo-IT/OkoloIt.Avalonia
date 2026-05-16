using Avalonia.Controls;

using OkoloIt.Avalonia.Controls.Editors;

namespace OkoloIt.Avalonia.Controls;

/// <summary>
/// Holds metadata for a registered property editor.
/// </summary>
/// <param name="TargetType">
/// The target property type that this editor can handle (e.g., <c>typeof(int)</c>).
/// </param>
/// <param name="Editor">
/// An instantiated editor control that implements <see cref="IPropertyEditor"/>.
/// (The name follows the original declaration; if a typo exists you may rename it to
/// <c>Editor</c>.)
/// </param>
public record class EditorInfo(
    Type TargetType,
    Control Editor);
