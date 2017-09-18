using System;

namespace UpcommingMovies.Core.Domain.Exceptions
{
    /// <summary>
    /// Base exception class for core expected errors. 
    /// This kind of error is the ones which will be handle at UI layer in order to friendly inform 
    /// the user about what happened.
    /// </summary>
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message)
        {
        }

        public CoreException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Message to the end user. Don't forget i18n.
        /// </summary>
        public virtual string FriendlyMessage { get; }
    }
}
