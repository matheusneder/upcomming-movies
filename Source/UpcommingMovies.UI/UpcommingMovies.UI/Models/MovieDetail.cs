using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.UI.Models
{
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

        public string Genres { get; set; }
        public bool HasGenres { get; set; }
        public bool HasOverview { get; set; }
        public Movie Movie { get; set; }
        public Uri Image { get; set; }
    }
}
