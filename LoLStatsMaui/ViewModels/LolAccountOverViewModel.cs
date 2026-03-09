using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Enities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities;
using Domain.Models.Entities.Requests;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using LoLStatsMaui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;


namespace LoLStatsMaui.ViewModels
{
    [QueryProperty(nameof(LolName), "lolName")]
    public partial class LolAccountOverViewModel : ObservableObject
    {
        
        private ILolService _lolService;

        [ObservableProperty]
        private string _lolName;
        [ObservableProperty]
        private SummonerOverview _summonerOverview;

        [ObservableProperty]
        private ObservableCollection<LolMatch> _matchList = new();

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        public bool ShowContent => !HasError && !IsLoading;

        partial void OnHasErrorChanged(bool value) => OnPropertyChanged(nameof(ShowContent));
        partial void OnIsLoadingChanged(bool value) => OnPropertyChanged(nameof(ShowContent));
        public LolAccountOverViewModel(ILolService lolService)
        {
            _lolService = lolService;
            SummonerOverview = new SummonerOverview();

        }
        partial void OnLolNameChanged(string value)
        {
            if (value != null)
            {
                LoadPage();
            }
        }
        private async void LoadPage()
        {
            await LoadPageAsync();
        }
        private async Task LoadPageAsync()
        {
            IsLoading = true;
            try
            {
                await LoadLolProfile();
            }
            catch (NotFoundException e)
            {
                
                ErrorMessage = "Kontot hittades inte, Kolla om du stavade fel.";
                Debug.WriteLine(e);
                return;
            }
            catch (UnauthorizedException e)
            {
                ErrorMessage = "Otillåten API nyckel, Se till att uppdatera API nyckeln";
                Debug.WriteLine(e);
                return;
            }
            catch (ServerException e)
            {
                ErrorMessage = "Riots API har just nu problem, Försök senare";
                Debug.WriteLine(e);
                return;
            }
            catch (Exception e)
            {
                ErrorMessage = "Något gick fel!";
                Debug.WriteLine(e);
                return;
            } finally
            {
                IsLoading = false;
                HasError = true;
            }
            HasError = false;
            
        }
        private async Task LoadLolProfile()
        {
            var profile = await _lolService.GetLolProfileAsync(LolName);
            SummonerOverview = profile.SummonerOverview;
            MatchList = new ObservableCollection<LolMatch>(profile.Matches);
        }

        [RelayCommand]
        private async Task NavigateToPlayer(LolMatchPlayer player)
        {
            if (player == null || player.IsTargetPlayer) return;
            await Shell.Current.GoToAsync($"{nameof(LolAccountOverviewPage)}?lolName={Uri.EscapeDataString(player.FullLolName)}");
        }



    }
}
