using LoLStatsMaui.ViewModels;

namespace LoLStatsMaui.Views;

public partial class LinkAccountPage : ContentPage
{
	public LinkAccountPage(LinkAccountViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}