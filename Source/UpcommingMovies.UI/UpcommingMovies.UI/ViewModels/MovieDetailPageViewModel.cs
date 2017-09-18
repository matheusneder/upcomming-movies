using Prism.Mvvm;
using Prism.Navigation;
using UpcommingMovies.Core.Domain.Models;
using UpcommingMovies.UI.Models;

namespace UpcommingMovies.UI.ViewModels
{
    /// <summary>
    /// ViewModel class for the MovieDetailPage view.
    /// </summary>
    public class MovieDetailPageViewModel : BindableBase, INavigationAware
    {
        private MovieDetail _movieDetail;
        public MovieDetail MovieDetail
        {
            get => _movieDetail;
            set => SetProperty(ref _movieDetail, value);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            MovieDetail = new MovieDetail(parameters["model"] as Movie);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {   
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }
    }
}
