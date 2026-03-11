using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Exceptions;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.ViewModels
{
    public partial class CreateUserViewModel : ObservableObject
    {
        private IUserFacade _userFacade;
        
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _errorMessage;
        [ObservableProperty]
        private string _userCreatedMessage;

        public CreateUserViewModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [RelayCommand]
        public async Task CreateUser()
        {
            if (string.IsNullOrEmpty(Username) || Username.Length < 3)
            {
                ErrorMessage = "Användar namnet måste vara minst 3 tecken";
                return;
            }
            if (string.IsNullOrEmpty(Password) || Password.Length < 8)
            {
                ErrorMessage = "Lösenordet måste vara minst 8 tecken";
                return;
            }

            try
            {
                await _userFacade.CreateUserAsync(Username, Password);
                ErrorMessage = "";
                UserCreatedMessage = "Användaren Skapades, Du skickas tillbaka till huvud sidan om 3 sekunder.";
                await Task.Delay(1000);
                UserCreatedMessage = "Användaren Skapades, Du skickas tillbaka till huvud sidan om 2 sekunder.";
                await Task.Delay(1000);
                UserCreatedMessage = "Användaren Skapades, Du skickas tillbaka till huvud sidan om 1 sekunder.";
                await Task.Delay(1000);
                await Shell.Current.GoToAsync("..");
            } 
            catch(UsernameExistException e)
            {
                ErrorMessage = "Användarnamnet används redan. Testa ett annat användarnamn";
                Debug.WriteLine(e);
            }
            catch(Exception e)
            {
                ErrorMessage = "Något gick fel";
                Debug.WriteLine(e);
            }
            
        }
    }
}
