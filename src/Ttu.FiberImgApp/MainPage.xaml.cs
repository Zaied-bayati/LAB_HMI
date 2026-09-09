using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Ttu.FiberImgApp.ViewModels;

namespace Ttu.FiberImgApp;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel { get; }

    public MainPage()
    {
        ViewModel = App.Services.GetRequiredService<MainViewModel>();
        InitializeComponent();
    }
}
