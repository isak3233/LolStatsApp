using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Enities.Requests;
using Domain.Models.Exceptions;
using LoLStatsMaui.Application.Facade;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.ViewModels
{
    public partial class LinkAccountViewModel : ObservableObject
    {
        private readonly IUserFacade _userFacade;
        private readonly ILolFacade _lolFacade;

        private LolAccountMetaData _lolAccountMetaData;
        private int _randomIconId;

        [ObservableProperty]
        private string _lolName;
        [ObservableProperty]
        private string _errorMessage;


        [ObservableProperty]
        private string _profileIconString;
        [ObservableProperty]
        private bool _accountFound;
        [ObservableProperty]
        private string _verifiedMessage;




        public LinkAccountViewModel(IUserFacade userFacade, ILolFacade lolFacade)
        {
            _userFacade = userFacade;
            _lolFacade = lolFacade;
        }
        [RelayCommand]
        public async Task FindAccount()
        {
            try
            {
                // Försöker hitta kontot
                _lolAccountMetaData = await _lolFacade.GetLolAccountMetaDataAsync(LolName);
                ErrorMessage = "";
                _randomIconId = await _lolFacade.GetRandomProfileImageIdAsync(_lolAccountMetaData);
                ProfileIconString = $"ProfileIcons/{_randomIconId}.png";
                AccountFound = true;
            }
            catch (NotFoundException ex)
            {
                ErrorMessage = "Konto hittades inte, Se till att det är rätt stavat";
                AccountFound = false;
                Debug.WriteLine(ex);
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = "Konto namnet är inte giltigt, Se till och kolla att du har med din # och tillräckligt många tecken.";
                AccountFound = false;
                Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Något gick fel, testa senare";
                AccountFound = false;
                Debug.WriteLine(ex);
            } 
        }
        [RelayCommand]
        public async Task VerifyAccount()
        {
            try
            {
                var successful = await _userFacade.VerifyAccountAsync(_lolAccountMetaData, _randomIconId);

                if (successful)
                {
                    ErrorMessage = "";
                    VerifiedMessage = "Lyckades verifera LoL kontot. Du skickas tillbaka till huvudsidan om 3 sekunder";
                    await Task.Delay(1000);
                    VerifiedMessage = "Lyckades verifera LoL kontot. Du skickas tillbaka till huvudsidan om 2 sekunder";
                    await Task.Delay(1000);
                    VerifiedMessage = "Lyckades verifera LoL kontot. Du skickas tillbaka till huvudsidan om 1 sekund";
                    await Task.Delay(1000);
                    await Shell.Current.GoToAsync("..");

                }
                else
                {
                    ErrorMessage = "Lyckades inte verifiera LoL kontot. Se till att sätta på den angivna profil bilden. Ibland kan det ta lite tid för lol att uppdateras";
                }
            }
            catch (LolAccountAlreadyLinkedException ex)
            {
                ErrorMessage = "Detta LoL konto är redan kopplat till ett användar konto";
            }
            catch (UserNotLoggedInException ex)
            {
                ErrorMessage = "Du är inte inloggad";
            } 
            catch (Exception ex)
            {
                ErrorMessage = "Något gick fel";
            }
            
            
            
        }
    }
}
