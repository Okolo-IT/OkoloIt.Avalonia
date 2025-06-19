using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using OkoloIt.Avalonia.Dialogs;
using OkoloIt.Avalonia.UiKit.Models;

namespace OkoloIt.Avalonia.UiKit.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private PropertyModel _propertyModel;

    public MainViewModel()
    {
        PropertyModel = new() {
            Name = "Name"
        };
    }

    [RelayCommand]
    private async Task OnGenerateRandomValues()
    {
        PropertyModel.Name = Path.GetRandomFileName();
        PropertyModel.Position.X = Random.Shared.Next();
        PropertyModel.Position.Y = Random.Shared.Next();
        PropertyModel.Type       = ModelType.Simple;

        await MessageBox.ShowErrorAsync("Error?");
    }
}
