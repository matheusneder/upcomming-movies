﻿using System;
using System.Net.Http;
using UpcommingMovies.Core.Domain.Exceptions;
using UpcommingMovies.Infra.TheMovieDb.I18n;

namespace UpcommingMovies.Infra.TheMovieDb.Exceptions
{
    /// <summary>
    /// Used when remote service fail.
    /// </summary>
    public class RemoteServiceException : CoreException
    {
        public RemoteServiceException(string message) : base(message)
        {
        }

        public RemoteServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Uri Uri { get; private set; }
        public int StatusCode { get; private set; }
        public string ReasonPhrase { get; private set; }

        public HttpResponseMessage Response { get; private set; }

        public RemoteServiceException(Uri uri, HttpResponseMessage response) : base("RemoteServiceException")
        {
            Uri = uri;
            Response = response;
        }

        public RemoteServiceException(Uri uri, Exception innerException) : base("RemoteServiceException", innerException)
        {
            Uri = uri;
        }

        public override string FriendlyMessage => 
            Expressions.RemoteServiceExceptionFriendlyMessage;
    }
}
