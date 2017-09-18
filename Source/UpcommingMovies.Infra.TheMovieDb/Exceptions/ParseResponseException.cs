using System;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    public class ParseResponseException : RemoteServiceException
    {
        public ParseResponseException(string message) : base(message)
        {
        }

        public ParseResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
