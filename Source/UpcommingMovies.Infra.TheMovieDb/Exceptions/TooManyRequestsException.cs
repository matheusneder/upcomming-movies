using System;
using System.Net.Http;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Used for 429 http status.
    /// </summary>
    public class TooManyRequestsException : RemoteServiceException
    {
        public TooManyRequestsException(Uri uri, HttpResponseMessage response) : base(uri, response)
        {
        }

        public override string FriendlyMessage =>
            Expressions.TooManyRequestsExceptionFriendlyMessage;
    }
}
