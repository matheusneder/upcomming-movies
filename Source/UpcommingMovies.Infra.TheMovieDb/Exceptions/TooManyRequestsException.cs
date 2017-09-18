using System;
using System.Net.Http;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    public class TooManyRequestsException : RemoteServiceException
    {
        public TooManyRequestsException(Uri uri, HttpResponseMessage response) : base(uri, response)
        {

        }
    }
}
