
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Enities.LolEnities;
using Domain.Models.Exceptions;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LoLStatsMaui.ViewModels
{
    [QueryProperty(nameof(LolName), "lolName")]
    public partial class LiveGameViewModel : ObservableObject
    {
        private ILolFacade _lolFacade;

        [ObservableProperty]
        private string _lolName;

        [ObservableProperty]
        private SummonerOverview _summonerOverview;

        [ObservableProperty]
        private CurrentLolMatchPlayer? _selectedPlayer;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty]
        private string _errorMessage;

        public bool ShowContent => !HasError && !IsLoading;

        partial void OnHasErrorChanged(bool value) => OnPropertyChanged(nameof(ShowContent));
        partial void OnIsLoadingChanged(bool value) => OnPropertyChanged(nameof(ShowContent));

        public bool IsNotInGame => SummonerOverview?.CurrentLolMatch == null;
        public List<CurrentLolMatchPlayer> BlueTeamPlayers => SummonerOverview?.CurrentLolMatch?.Players.Where(p => p.TeamId == 100).ToList() ?? new();

        public List<CurrentLolMatchPlayer> RedTeamPlayers => SummonerOverview?.CurrentLolMatch?.Players.Where(p => p.TeamId == 200).ToList() ?? new();

        partial void OnSummonerOverviewChanged(SummonerOverview value)
        {
            OnPropertyChanged(nameof(BlueTeamPlayers));
            OnPropertyChanged(nameof(RedTeamPlayers));
            OnPropertyChanged(nameof(IsNotInGame));
        }

        public LiveGameViewModel(ILolFacade lolFacade)
        {
            _lolFacade = lolFacade;
        }
        partial void OnLolNameChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                LoadLiveGame();
            }
                
        }

        private async void LoadLiveGame()
        {
            await LoadLiveGameAsync();
        }
        private async Task LoadLiveGameAsync()
        {
            IsLoading = true;
            try
            {
                var profileData = await _lolFacade.GetLolProfileAsync(LolName);
                SummonerOverview = profileData.SummonerOverview;

            }
            catch (RateLimitException e)
            {
                ErrorMessage = "Du har skickat för många anrop försök senare";
                HasError = true;
                Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                ErrorMessage = "Något gick fel";
                HasError = true;
                Debug.WriteLine(e);
            }
            finally
            {
                IsLoading = false;
            }
        }
        [RelayCommand]
        private async Task NavigateToPlayer(CurrentLolMatchPlayer player)
        {
            if (player.Puuid == null) return;
            await Shell.Current.GoToAsync($"{nameof(LolAccountOverviewPage)}?lolName={Uri.EscapeDataString(player.RiotId)}");
            SelectedPlayer = null;
        }
    }
}
