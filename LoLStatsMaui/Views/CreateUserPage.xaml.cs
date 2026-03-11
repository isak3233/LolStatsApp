using LoLStatsMaui.ViewModels;

namespace LoLStatsMaui.Views;

public partial class CreateUserPage : ContentPage
{
	public CreateUserPage(CreateUserViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

    }
}