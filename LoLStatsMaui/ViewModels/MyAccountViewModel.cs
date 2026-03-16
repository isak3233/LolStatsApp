using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Enities.LolEnities;
using Domain.Models.Exceptions;
using LoLStatsMaui.Application.Facade;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.ViewModels
{
    public partial class MyAccountViewModel : ObservableObject
    {
        private readonly IUserFacade _userFacade;

        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private List<SummonerOverview> _linkedAccounts;
        [ObservableProperty]
        private List<SummonerOverview> _followedAccounts;
        [ObservableProperty]
        private SummonerOverview? _selectedAccount;
        [ObservableProperty]
        private string _errorMessage;

        public bool HasNoLinkedAccounts => LinkedAccounts == null || LinkedAccounts.Count == 0;
        partial void OnLinkedAccountsChanged(List<SummonerOverview> value) => OnPropertyChanged(nameof(HasNoLinkedAccounts));

        public bool HasNoFollowedAccounts => FollowedAccounts == null || FollowedAccounts.Count == 0;
        partial void OnFollowedAccountsChanged(List<SummonerOverview> value) => OnPropertyChanged(nameof(HasNoFollowedAccounts));

        public MyAccountViewModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        public async void OnAppearing()
        {
            try
            {
                await LoadAccount();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        private async Task LoadAccount()
        {
            try
            {
                Username = _userFacade.GetUsername();
                var linkedAccountsTask = _userFacade.GetLinkedSummonerOverviewsAsync();
                var followedAccountsTask = _userFacade.GetFollowersSummonerOverviewsAsync();
                LinkedAccounts = await linkedAccountsTask;
                FollowedAccounts = await followedAccountsTask;
                ErrorMessage = "";
            }
            catch (RateLimitException e)
            {
                ErrorMessage = "Du skickar för många anrop, Testa senare";
                Debug.WriteLine(e);
            }
            catch (UnauthorizedException e)
            {
                ErrorMessage = "Otillåten API nyckel, Se till att uppdatera API nyckeln";
                Debug.WriteLine(e);
            }
            catch (ServerException e)
            {
                ErrorMessage = "Riots API har just nu problem, Försök senare";
                Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                ErrorMessage = "Något gick fel";
                Debug.WriteLine(e);
            }
        }
        [RelayCommand]
        private async Task NavigateToAccount(SummonerOverview account)
        {
            if (account == null) return;
            var lolName = Uri.EscapeDataString($"{account.SummonerName}#{account.TagLine}");
            await Shell.Current.GoToAsync($"{nameof(LolAccountOverviewPage)}?lolName={lolName}");
            SelectedAccount = null;
        }
        [RelayCommand]
        private async Task UnlinkAccount(string uuid)
        {
            await _userFacade.UnlinkLolAccountAsync(uuid);
            LinkedAccounts = await _userFacade.GetLinkedSummonerOverviewsAsync();
        }
        [RelayCommand]
        private async Task UnfollowAccount(string uuid)
        {
            await _userFacade.HandleFollow(uuid);
            FollowedAccounts = await _userFacade.GetFollowersSummonerOverviewsAsync();
        }
        [RelayCommand]
        private async Task NavigateToLinkAccount()
        {
            await Shell.Current.GoToAsync(nameof(LinkAccountPage));
        }

    }
}
