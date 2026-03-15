using LoLStatsMaui.ViewModels;

namespace LoLStatsMaui.Views;

public partial class MyAccountPage : ContentPage
{
	public MyAccountPage(MyAccountViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MyAccountViewModel vm)
        {
            vm.OnAppearing();
        }

    }
}