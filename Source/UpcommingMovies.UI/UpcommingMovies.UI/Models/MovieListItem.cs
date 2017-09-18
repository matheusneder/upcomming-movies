using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcommingMovies.Core.Domain.Models;

namespace UpcommingMovies.UI.Models
{
    public class MovieListItem
    {
        public MovieListItem(Movie movie)
        {
            Movie = movie;
            Genres = string.Join(", ", movie.Genres.Select(g => g.Name));
            HasGenres = movie.Genres.Any();
            Image = movie.BuildBackdropImageUri(150) ?? movie.BuildPosterImageUri(150);
        }

        public string Genres { get; set; }
        public bool HasGenres { get; set; }
        public Uri Image { get; set; }
        public Movie Movie { get; set; }
    }
}
