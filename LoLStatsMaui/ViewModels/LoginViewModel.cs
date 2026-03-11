using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Exceptions;
using LoLStatsMaui.Application.Facade;
using LoLStatsMaui.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoLStatsMaui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserFacade _userFacade;

        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _errorMessage;
        [ObservableProperty]
        private string _loggedInMessage;

        public LoginViewModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Fyll i både användarnamn och lösenord";
                return;
            }
            try
            {

                await _userFacade.LoginAsync(Username, Password);
                ErrorMessage = "";
                LoggedInMessage = "Lyckades logga in, Du skickas tillbaka till huvudsidan om 3 sekunder";
                await Task.Delay(1000);
                LoggedInMessage = "Lyckades logga in, Du skickas tillbaka till huvudsidan om 2 sekunder";
                await Task.Delay(1000);
                LoggedInMessage = "Lyckades logga in, Du skickas tillbaka till huvudsidan om 1 sekund";
                await Task.Delay(1000);
                await Shell.Current.GoToAsync("..");
            }
            catch (InvalidCredentialsException e)
            {
                ErrorMessage = "Fel användarnamn eller lösenord";
            }
            catch (Exception e)
            {
                ErrorMessage = "Något gick fel";
            }
        }

    }
}
