using Microsoft.Practices.Unity;
using System;
using UpcommingMovies.Core.Domain.Services;
using UpcommingMovies.Infra.TheMovieDb;
using UpcommingMovies.Infra.TheMovieDb.Services;

namespace UpcommingMovies.Infra.IoC
{
    /// <summary>
    /// Takes care of services registration. Services contracts describled by Core.Domain project 
    /// will be binding to his respective implementation here.
    /// </summary>
    public class ApplicationInstaller
    {
        /// <summary>
        /// Register the types and instances necessary to execute the application.
        /// </summary>
        /// <param name="container">IoC container</param>
        public static void Install(IUnityContainer container)
        {
            var theMovieDbConfigInstance = new TheMovieDbConfig()
            {
                ApiKey = "1f54bd990f1cdfb230adb312546d765d",
                ApiBaseUri = new Uri("https://api.themoviedb.org/3/")
            };

            container.RegisterInstance(typeof(TheMovieDbConfig), theMovieDbConfigInstance);
            container.RegisterType<IMovieDiscoverService, MovieDiscoverService>();
        }
    }
}
