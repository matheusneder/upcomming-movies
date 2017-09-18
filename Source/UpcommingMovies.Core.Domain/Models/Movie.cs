using System;
using System.Collections.Generic;

namespace UpcommingMovies.Core.Domain.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public Func<int, Uri> BuildPosterImageUri { get; set; }
        public Func<int, Uri> BuildBackdropImageUri { get; set; }
    }
}
