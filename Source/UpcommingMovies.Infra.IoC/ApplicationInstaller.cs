using Microsoft.Practices.Unity;
using System;
using UpcommingMovies.Core.Domain.Services;
using UpcommingMovies.Infra.TheMovieDb;
using UpcommingMovies.Infra.TheMovieDb.Services;

namespace UpcommingMovies.Infra.IoC
{
    public class ApplicationInstaller
    {
        public static void Install(IUnityContainer container)
        {
            var theMovieDbConfigInstance = new TheMovieDbConfig()
            {
                ApiKey = "1f54bd990f1cdfb230adb312546d765d",
                ApiBaseUri = new Uri("https://api.themoviedb.org/3/")
            };

            var movieDiscoverServiceInstance = new MovieDiscoverService(theMovieDbConfigInstance);

            container.RegisterInstance(typeof(TheMovieDbConfig), theMovieDbConfigInstance);
            container.RegisterType<IMovieDiscoverService, MovieDiscoverService>();
        }
    }
}
