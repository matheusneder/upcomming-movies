using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcommingMovies.Infra.TheMovieDb.Test
{
    public class TheMovieDbMockResponseData
    {
        public static string ConfigurationData => File.ReadAllText("MockData/configuration.json") ;
        public static string GenresData => File.ReadAllText("MockData/genres.json");
        public static string MoviesData => File.ReadAllText("MockData/movies.json");
    }
}
