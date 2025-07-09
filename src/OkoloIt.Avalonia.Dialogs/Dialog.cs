using System.Windows.Input;

using Avalonia;

using Avalonia.Controls;

namespace OkoloIt.Avalonia.Dialogs;

public class Dialog : UserControl
{
    public static readonly StyledProperty<MessageBoxType> TypeProperty
        = AvaloniaProperty.Register<Dialog, MessageBoxType>(nameof(TypeProperty));

    public static readonly StyledProperty<string> TitleProperty
        = AvaloniaProperty.Register<Dialog, string>(nameof(TitleProperty), string.Empty);

    public static readonly StyledProperty<string> TextProperty
        = AvaloniaProperty.Register<Dialog, string>(nameof(TextProperty), string.Empty);


    public static readonly StyledProperty<bool> VisibleOkButtonProperty
        = AvaloniaProperty.Register<Dialog, bool>(nameof(VisibleOkButtonProperty));

    public static readonly StyledProperty<bool> VisibleYesButtonProperty
        = AvaloniaProperty.Register<Dialog, bool>(nameof(VisibleYesButtonProperty));

    public static readonly StyledProperty<bool> VisibleNoButtonProperty
        = AvaloniaProperty.Register<Dialog, bool>(nameof(VisibleNoButtonProperty));

    public static readonly StyledProperty<bool> VisibleCancelButtonProperty
        = AvaloniaProperty.Register<Dialog, bool>(nameof(VisibleCancelButtonProperty));


    public static readonly StyledProperty<ICommand> OkCommandProperty
        = AvaloniaProperty.Register<Dialog, ICommand>(nameof(OkCommandProperty));

    public static readonly StyledProperty<ICommand> NoCommandProperty
        = AvaloniaProperty.Register<Dialog, ICommand>(nameof(NoCommandProperty));

    public static readonly StyledProperty<ICommand> YesCommandProperty
        = AvaloniaProperty.Register<Dialog, ICommand>(nameof(YesCommandProperty));

    public static readonly StyledProperty<ICommand> CancelCommandProperty
        = AvaloniaProperty.Register<Dialog, ICommand>(nameof(CancelCommandProperty));

    public Dialog()
    {
    }

    public Dialog(MessageBoxType type, string title, string text)
    {
        Type = type;
        Title = title;
        Text = text;

        if (Type == MessageBoxType.Information) {
            VisibleOkButton = true;
            VisibleNoButton = false;
            VisibleYesButton = false;
            VisibleCancelButton = false;
            return;
        }

        if (Type == MessageBoxType.Warning) {
            VisibleOkButton = true;
            VisibleNoButton = false;
            VisibleYesButton = false;
            VisibleCancelButton = false;
            return;
        }

        if (Type == MessageBoxType.Question) {
            VisibleOkButton = false;
            VisibleNoButton = true;
            VisibleYesButton = true;
            VisibleCancelButton = true;
            return;
        }

        if (Type == MessageBoxType.Error) {
            VisibleOkButton = true;
            VisibleNoButton = false;
            VisibleYesButton = false;
            VisibleCancelButton = false;
            return;
        }
    }

    public MessageBoxType Type {
        get => GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public string Title {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Text {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool VisibleOkButton {
        get => GetValue(VisibleOkButtonProperty);
        set => SetValue(VisibleOkButtonProperty, value);
    }

    public bool VisibleYesButton {
        get => GetValue(VisibleYesButtonProperty);
        set => SetValue(VisibleYesButtonProperty, value);
    }

    public bool VisibleNoButton {
        get => GetValue(VisibleNoButtonProperty);
        set => SetValue(VisibleNoButtonProperty, value);
    }

    public bool VisibleCancelButton {
        get => GetValue(VisibleCancelButtonProperty);
        set => SetValue(VisibleCancelButtonProperty, value);
    }

    public ICommand OkCommand {
        get => GetValue(OkCommandProperty);
        set => SetValue(OkCommandProperty, value);
    }

    public ICommand NoCommand {
        get => GetValue(NoCommandProperty);
        set => SetValue(NoCommandProperty, value);
    }

    public ICommand YesCommand {
        get => GetValue(YesCommandProperty);
        set => SetValue(YesCommandProperty, value);
    }

    public ICommand CancelCommand {
        get => GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }
}
