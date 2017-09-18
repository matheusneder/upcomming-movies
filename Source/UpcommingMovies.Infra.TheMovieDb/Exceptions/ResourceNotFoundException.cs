using System;
using System.Net.Http;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    public class ResourceNotFoundException : RemoteServiceException
    {
        public ResourceNotFoundException(Uri uri, HttpResponseMessage response) : base(uri, response)
        {

        }
    }
}
