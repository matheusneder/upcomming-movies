using System;
using System.Net.Http;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    public class ResourceUnauthorizedException : RemoteServiceException
    {
        public ResourceUnauthorizedException(Uri uri, HttpResponseMessage response) : base(uri, response)
        { 

        }
    }
}
