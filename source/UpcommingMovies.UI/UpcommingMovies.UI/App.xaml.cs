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
            InitializeComponent();
            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MovieDetailPage>();

            // Register Core.Domain interfaces and it's implementation!
            ApplicationInstaller.Install(Container);
        }
    }
}
