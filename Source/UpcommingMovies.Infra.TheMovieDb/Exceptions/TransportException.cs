using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    public class TransportException : RemoteServiceException
    {
        public TransportException(Uri uri, Exception e) : 
            base($"TransportException while reading response for {uri.ToString()}. See innerException for detail.", e)
        {

        }
    }
}
