using System;
using System.Net.Http;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Used for 404 http status.
    /// </summary>
    public class ResourceNotFoundException : RemoteServiceException
    {
        public ResourceNotFoundException(Uri uri, HttpResponseMessage response) : base(uri, response)
        {
        }

        public override string FriendlyMessage => 
            Expressions.ResourceNotFoundExceptionFriendlyMessage;
    }
}
