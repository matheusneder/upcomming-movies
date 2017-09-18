using System;
using System.Net.Http;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Used for 401 http status.
    /// </summary>
    public class ResourceUnauthorizedException : RemoteServiceException
    {
        public ResourceUnauthorizedException(Uri uri, HttpResponseMessage response) : base(uri, response)
        { 
        }

        public override string FriendlyMessage =>
            Expressions.ResourceUnauthorizedExceptionFriendlyMessage;
    }
}
