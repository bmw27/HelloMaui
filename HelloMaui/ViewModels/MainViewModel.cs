using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HelloMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IConnectivity _connectivity;

    [ObservableProperty]
    private ObservableCollection<string> _items = new();

    [ObservableProperty]
    private string _text;

    public MainViewModel(IConnectivity connectivity)
    {
        _connectivity = connectivity;
    }

    [RelayCommand]
    private async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
            return;
        }

        Items.Add(Text);

        Text = string.Empty;
    }

    [RelayCommand]
    private void Delete(string s)
    {
        if (Items.Contains(s)) Items.Remove(s);
    }

    [RelayCommand]
    private async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
}
