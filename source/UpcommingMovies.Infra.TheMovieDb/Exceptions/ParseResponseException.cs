using System;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Used when a response parsing fail. 
    /// </summary>
    public class ParseResponseException : RemoteServiceException
    {
        public ParseResponseException(string message) : base(message)
        {
        }

        public ParseResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string FriendlyMessage => 
            Expressions.ParseResponseExceptionFriendlyMessage;
    }
}
