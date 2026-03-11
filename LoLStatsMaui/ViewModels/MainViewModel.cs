
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace LoLStatsMaui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ICommand SubmitCommand { get; }
        private IUserFacade _userFacade;

        [ObservableProperty]
        private string _loginBtnText;
        [ObservableProperty]
        private bool _showRegisterBtn;
        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private string _lolName;




        public MainViewModel(IUserFacade userFacade)
        {
            SubmitCommand = new Command(OnSubmit);
            _userFacade = userFacade;
        }
        public void UpdatePage()
        {
            if (_userFacade.IsLoggedIn)
            {
                LoginBtnText = "Logga ut";
                ShowRegisterBtn = false;
            }
            else
            {
                LoginBtnText = "Logga in";
                ShowRegisterBtn = true;
            }
        }
        private async void OnSubmit()
        {
            if (LolName == null || LolName == "")
            {
                ErrorMessage = "Du skrev inget!";
                return;
            }
            if(!(LolName.Count(c => c == '#') == 1))
            {
                ErrorMessage = "Det sökta kontot måste innehålla bara ETT #";
                return;
            }
            if (LolName.Split('#')[0].Length < 4)
            {
                ErrorMessage = "Det sökta spel namnet är mindre än 4 tecken";
                return;
            }
            if (LolName.Split('#')[1].Length < 3)
            {
                ErrorMessage = "Det sökta spel tagen är mindre än 3 tecken";
                return;
            }
            ErrorMessage = "";
            await Shell.Current.GoToAsync($"{nameof(LolAccountOverviewPage)}?lolName={Uri.EscapeDataString(LolName)}");

        }
        [RelayCommand]
        private async Task NavigateToCreateUser()
        {
            await Shell.Current.GoToAsync(nameof(CreateUserPage));
        }
        [RelayCommand]
        private async Task NavigateToLogin()
        {
            if(_userFacade.IsLoggedIn)
            {
                _userFacade.Logout();
                UpdatePage();
            } else
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            
        }

    }
}
