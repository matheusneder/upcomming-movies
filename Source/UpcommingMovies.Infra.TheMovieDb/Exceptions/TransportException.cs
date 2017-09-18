using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Comunication broken while streamming something.
    /// </summary>
    public class TransportException : RemoteServiceException
    {
        public TransportException(Uri uri, Exception e) : 
            base($"TransportException while reading response for {uri.ToString()}. See innerException for detail.", e)
        {
        }

        public override string FriendlyMessage =>
            Expressions.TransportExceptionFriendlyMessage;
    }
}
