﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Exceptions;
using UpcommingMovies.Core.Domain.Models;
using UpcommingMovies.Core.Domain.Services;
using UpcommingMovies.UI.I18n;
using UpcommingMovies.UI.Models;

namespace UpcommingMovies.UI.ViewModels
{
    /// <summary>
    /// ViewModel class for the MainPage view, which is the entry point of app.
    /// </summary>
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private readonly IMovieDiscoverService _movieDicoverService;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(IMovieDiscoverService movieDicoverService, INavigationService navigationService)
        {
            _movieDicoverService = movieDicoverService;
            _navigationService = navigationService;
            MovieList = new ObservableCollection<MovieListItem>();

            ItemAppearingCommand = new DelegateCommand<MovieListItem>(HandleItemAppearingEvent);
            ItemTappedCommand = new DelegateCommand<MovieListItem>(HandleItemTappedEvent);
            SearchCommand = new DelegateCommand(HandleSearchBarButtonPressed);
        }

        /// <summary>
        /// This is the list linked to ListView.
        /// </summary>
        public ObservableCollection<MovieListItem> MovieList { get; set; }

        /// <summary>
        /// Handle ItemAppearing event of the ListView which responsible
        /// to identify when the next page should be retrieved.
        /// </summary>
        public DelegateCommand<MovieListItem> ItemAppearingCommand { get; set; }

        /// <summary>
        /// Handle item tapped in order to navigate do detail page.
        /// </summary>
        public DelegateCommand<MovieListItem> ItemTappedCommand { get; set; }

        /// <summary>
        /// Handle search button pressed event.
        /// </summary>
        public DelegateCommand SearchCommand { get; set; }

        private string _searchBarText;

        /// <summary>
        /// The search bar text.
        /// </summary>
        public string SearchBarText
        {
            get => _searchBarText;
            set
            {
                // TODO: Find a better way to get searchBar "reset" event
                if(string.IsNullOrEmpty(value))
                {
                    SearchBarReseted();
                }

                SetProperty(ref _searchBarText, value);
            }
        }

        private async void HandleItemTappedEvent(MovieListItem itemTapped)
        {
            Debug.WriteLine($"Movie tapped: {itemTapped?.Movie?.Title}");

            var navigationParameters = new NavigationParameters()
            {
                { "model", itemTapped.Movie  }
            };

            await _navigationService.NavigateAsync("MovieDetailPage", navigationParameters);
        }

        private async void HandleItemAppearingEvent(MovieListItem itemAppearing)
        {
            if(ShouldLoadNextPage() && 
                itemAppearing == MovieList.LastOrDefault() /* Reached the bottom of the ViewList */) 
            {
                await LoadNextPage();
            }
        }

        // Force reload from first page.
        private async Task Reload()
        {
            _currentPage = 0;
            MovieList.Clear();

            if (ShouldLoadNextPage())
            {
                await LoadNextPage();
            }
        }

        private async void HandleSearchBarButtonPressed()
        {
            _isSearching = true;
            await Reload();
        }

        private async void SearchBarReseted()
        {
            _isSearching = false;
            await Reload();
        }

        private int _currentPage = 0;
        private DiscoverResult _lastResult = null;

        private bool _loadingPage = false;
        private bool _isSearching;

        /// <summary>
        /// True if a paging is in progress of load, false otherwise.
        /// </summary>
        public bool LoadingPage
        {
            get => _loadingPage;
            set => SetProperty(ref _loadingPage, value);
        }

        private bool ShouldLoadNextPage() => !LoadingPage && (_lastResult?.TotalPages ?? long.MaxValue) > _currentPage;

        private async Task LoadNextPage()
        {
            LoadingPage = true;
            int nextPage = _currentPage + 1;
            Debug.WriteLine($"Loading page {nextPage}!");

            try
            {
                var result = await _movieDicoverService.RetrieveMoviesAsync(nextPage, _isSearching ? SearchBarText : null);
                _currentPage = nextPage;

                // copy movie list to the ObservableCollection
                foreach (var movie in result.Movies)
                {
                    MovieList.Add(new MovieListItem(movie));
                }

                _lastResult = result;
            }
            catch (CoreException e)
            {
                await App.Current.MainPage.DisplayAlert(Expressions.Error, e.FriendlyMessage, Expressions.Ok);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert(Expressions.Error, Expressions.UnexpectedErrorMessage, Expressions.Ok);
                throw;
            } 
            finally 
            {
                LoadingPage = false;         
            }
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (MovieList.Count == 0 && ShouldLoadNextPage())
            {
                await LoadNextPage();
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
