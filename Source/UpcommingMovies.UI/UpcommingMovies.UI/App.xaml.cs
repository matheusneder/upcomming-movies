using Prism.Unity;
using UpcommingMovies.Infra.IoC;
using UpcommingMovies.UI.Views;
using Xamarin.Forms;

namespace UpcommingMovies.UI
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MovieDetailPage>();
            ApplicationInstaller.Install(Container);
            //var theMovieDbConfigInstance = new TheMovieDbConfig()
            //{
            //    ApiKey = "1f54bd990f1cdfb230adb312546d765d",
            //    ApiBaseUri = new Uri("https://xapi.themoviedb.org/3/")
            //};

            //var movieDiscoverServiceInstance = new MovieDiscoverService(theMovieDbConfigInstance);

            //Container.RegisterInstance(typeof(TheMovieDbConfig), theMovieDbConfigInstance);
            //Container.RegisterType<IMovieDiscoverService, MovieDiscoverService>();// (typeof(IMovieDiscoverService), movieDiscoverServiceInstance);
        }
    }
}
