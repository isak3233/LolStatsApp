using LoLStatsMaui.ViewModels;

namespace LoLStatsMaui.Views;

public partial class LiveGamePage : ContentPage
{
	public LiveGamePage(LiveGameViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}