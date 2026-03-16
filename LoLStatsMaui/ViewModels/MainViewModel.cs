
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool _isLoggedIn;
        [ObservableProperty]
        private ObservableCollection<string> _searchHistory = new();
        public bool HasSearchHistory => SearchHistory.Count > 0;
        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private string _lolName;

        partial void OnSearchHistoryChanged(ObservableCollection<string> value) =>
            OnPropertyChanged(nameof(HasSearchHistory));




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
                IsLoggedIn = true;
                SearchHistory = new ObservableCollection<string>(_userFacade.GetSearchHistory()); 
            }
            else
            {
                LoginBtnText = "Logga in";
                IsLoggedIn = false;
                SearchHistory = new ObservableCollection<string>(new List<string>());
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
            if(IsLoggedIn)
            {
                _ = _userFacade.AddSearchedLolName(LolName);
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
        [RelayCommand]
        private void SelectHistory(string lolName)
        {
            LolName = lolName;
        }

        [RelayCommand]
        private async Task NavigateToLinkAccount()
        {
            await Shell.Current.GoToAsync(nameof(LinkAccountPage));
        }
        [RelayCommand]
        private async Task NavigateToMyAccount()
        {
            await Shell.Current.GoToAsync(nameof(MyAccountPage));
        }
    }
}
