using LoLStatsMaui.ViewModels;
using Microsoft.Maui.Controls;
namespace LoLStatsMaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MainViewModel vm)
            {
                vm.UpdatePage();
            }
                
        }

    }
}
