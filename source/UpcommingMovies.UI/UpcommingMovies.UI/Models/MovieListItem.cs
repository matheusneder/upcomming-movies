using System;
using System.Linq;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.UI.Models
{
    /// <summary>
    /// Model for MainPage/MainPageViewModel
    /// </summary>
    public class MovieListItem
    {
        public MovieListItem(Movie movie)
        {
            Movie = movie;
            Genres = string.Join(", ", movie.Genres.Select(g => g.Name));
            HasGenres = movie.Genres.Any();
            Image = movie.BuildPosterImageUri(90);
        }

        /// <summary>
        /// String with all genres separated by ",".
        /// </summary>
        public string Genres { get; set; }

        /// <summary>
        /// True if the movie has at least one genre, false otherwise.
        /// </summary>
        public bool HasGenres { get; set; }

        /// <summary>
        /// Backdrop image uri if exists or poster image uri or null.
        /// </summary>
        public Uri Image { get; set; }
        
        /// <summary>
        /// The movie object.
        /// </summary>
        public Movie Movie { get; set; }
    }
}
