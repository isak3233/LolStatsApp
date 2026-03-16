using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.Enities.LolEnities;
using Domain.Models.Enities.Requests;
using Domain.Models.Entities.Requests;
using Domain.Models.Exceptions;
using Domain.Models.Interfaces;
using LoLStatsMaui.Application.Facade;
using LoLStatsMaui.Application.Interfaces;
using LoLStatsMaui.Domain.Exceptions;
using LoLStatsMaui.Infrastructure.Constants;
using LoLStatsMaui.Views;
using System;
using System.Collections;
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
        
        private ILolFacade _lolFacade;
        private IUserFacade _userFacade;
        private MatchQueryRequest _matchRequest;

        [ObservableProperty]
        private string _lolName;
        [ObservableProperty]
        private SummonerOverview _summonerOverview;

        [ObservableProperty]
        private ObservableCollection<LolMatch> _matchList = new();

        [ObservableProperty]
        private string _selectedQueueType;

        public List<string> QueueTypes => new()
        {
            "Alla",
            "Ranked Solo/Duo",
            "Ranked Flex",
            "Normal",
            "Arena",
            "Aram",
        };

        [ObservableProperty]
        private string _errorMessage;
        [ObservableProperty]
        private string _loadMatchError;

        [ObservableProperty]
        private bool _isLoggedIn;
        [ObservableProperty]
        private bool _isFollowing;

        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private bool _isLoadingMoreMatches;

        [ObservableProperty]
        private bool _hasError;

        public bool ShowContent => !HasError && !IsLoading;

        partial void OnHasErrorChanged(bool value) => OnPropertyChanged(nameof(ShowContent));
        partial void OnIsLoadingChanged(bool value) => OnPropertyChanged(nameof(ShowContent));

        public LolAccountOverViewModel(ILolFacade lolFacade, IUserFacade userFacade)
        {
            _lolFacade = lolFacade;
            _userFacade = userFacade;
            IsLoggedIn = userFacade.IsLoggedIn;
            SummonerOverview = new SummonerOverview();

        }
        partial void OnLolNameChanged(string value)
        {
            if (value != null)
            {
                LoadPage();
            }
        }
        partial void OnSelectedQueueTypeChanged(string value)
        {
            if (SummonerOverview != null)
            {
                _matchRequest = _matchRequest with
                {
                    Start = -10,
                    Queue = RiotMapper.GetQueueId(SelectedQueueType)

                };
                MatchList = new();
                AddLolMatchesToList();
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
                IsFollowing = _userFacade.GetFollowInfo(SummonerOverview.Uuid);
                _matchRequest = new MatchQueryRequest
                {
                    Start = -10,
                    Count = 10
                };
                SelectedQueueType = "Alla"; // Detta kommer trigga OnSelectedQueueTypeChanged som loadar in våra matcher
            }
            catch (NotFoundException e)
            {
                
                ErrorMessage = "Kontot hittades inte, Kolla om du stavade fel";
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
            catch (RateLimitException e)
            {
                ErrorMessage = "Du har skickat för många anrop, Vänta lite innan du försöker igen";
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
            var profile = await _lolFacade.GetLolProfileAsync(LolName);
            SummonerOverview = profile.SummonerOverview;
            MatchList = new ObservableCollection<LolMatch>(profile.Matches);
        }
        private async Task AddLolMatchesToList()
        {
            IsLoadingMoreMatches = true;
            try
            {
                _matchRequest = _matchRequest with
                {
                    Uuid = SummonerOverview.Uuid,
                    Region = SummonerOverview.RawRegion,
                    Start = _matchRequest.Start + 10,
                };
                var matches = await _lolFacade.GetLolMatches(_matchRequest);
                foreach (var match in matches)
                {
                    MatchList.Add(match);
                }
            }
            finally
            {
                IsLoadingMoreMatches = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToPlayer(LolMatchPlayer player)
        {
            if (player == null || player.IsTargetPlayer) return;
            await Shell.Current.GoToAsync($"{nameof(LolAccountOverviewPage)}?lolName={Uri.EscapeDataString(player.FullLolName)}");
        }
        [RelayCommand]
        private async Task LoadMoreMatches()
        {
            LoadMatchError = "";
            try
            {
                await AddLolMatchesToList();
            } 
            catch (RateLimitException e)
            {
                LoadMatchError = "Du har skickat för många anrop, Vänta lite innan du försöker igen";
            }
            catch (Exception e)
            {
                LoadMatchError = "Något gick fel";
            }
            
        }
        
        [RelayCommand]
        private async Task HandleFollow()
        {
            await _userFacade.HandleFollow(SummonerOverview.Uuid);
            IsFollowing = !IsFollowing;

        }
        [RelayCommand]
        private async Task NavigateToLiveGame()
        {
            await Shell.Current.GoToAsync($"{nameof(LiveGamePage)}?lolName={Uri.EscapeDataString(LolName)}");
        }
        [RelayCommand]
        private async Task UpdateSummoner()
        {
            try
            {
                var profile = await _lolFacade.GetLolProfileAsync(LolName, true);
                SummonerOverview = profile.SummonerOverview;
            }
            catch (RateLimitException e)
            {
                ErrorMessage = "Du har skickat för många anrop, Vänta lite innan du försöker igen";
                Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                ErrorMessage = "Något gick fel";
                Debug.WriteLine(e);
            }
        }



    }
}
