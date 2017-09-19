using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.UI.Models
{
    /// <summary>
    /// Model for MovieDetailPage/MovieDetailPageViewModel
    /// </summary>
    public class MovieDetail
    {
        public MovieDetail(Movie movie)
        {
            Movie = movie;
            Genres = string.Join(", ", movie.Genres.Select(g => g.Name));
            HasGenres = movie.Genres.Any();
            Image = movie.BuildPosterImageUri(600);
            HasOverview = !string.IsNullOrWhiteSpace(movie.Overview);
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
        /// True if the movie has overview, false otherwise.
        /// </summary>
        public bool HasOverview { get; set; }

        /// <summary>
        /// The movie object.
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// Poster image uri or null if no image found.
        /// </summary>
        public Uri Image { get; set; }
    }
}
