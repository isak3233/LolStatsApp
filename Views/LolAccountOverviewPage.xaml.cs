using System;
using System.Collections.Generic;
using System.Text;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.ViewModels;

namespace LoLStatsMaui.Views
{
    public partial class LolAccountOverviewPage : ContentPage
    {
        public LolAccountOverviewPage(LolAccountOverViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
    }
}
