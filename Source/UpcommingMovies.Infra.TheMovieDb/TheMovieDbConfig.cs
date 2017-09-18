using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcommingMovies.Infra.TheMovieDb
{
    public class TheMovieDbConfig
    {
        public string ApiKey { get; set; }
        public Uri ApiBaseUri { get; set; }
    }
}
