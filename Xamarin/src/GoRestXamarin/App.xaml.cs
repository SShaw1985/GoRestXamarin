using System.Threading.Tasks;
using GoRestXamarin.Resources;
using GoRestXamarin.Services;
using GoRestXamarin.Views;
using Plugin.Multilingual;
using Prism;
using Prism.Ioc;
using DryIoc;
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using GoRestXamarin.ViewModels;
using System;
using Prism.DryIoc;
using GoRestXamarin.Interfaces;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GoRestXamarin
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }


        protected override async void OnInitialized()
        {
            InitializeComponent();
            Akavache.Registrations.Start("GoRestXamarin");

            if (Current.Resources == null)
            {
                Current.Resources = new ResourceDictionary();
            }


            Log.Listeners.Add(new TraceLogListener());

            try
            {
                // https://stackoverflow.com/a/43563054/5752 - Capture DI/Prism Exceptions
                TaskScheduler.UnobservedTaskException += (sender, e) =>
                {
                    Console.WriteLine("DI PRISM EXCEPTION: " + e.Exception.Message);
                };

                await NavigationService.NavigateAsync($"{nameof(MasterPage)}/{nameof(BaseNavigationPage)}/{nameof(AppHome)}");
            }
            catch (Exception e)
            {
                Console.WriteLine("INITIALISE EXCEPTION: " + e.Message);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IAppNavigationService, AppNavigationService>();
            containerRegistry.Register<IRestService, RestService>();

            containerRegistry.Register<IGoRestService, GoRestService>();

            containerRegistry.RegisterForNavigation<BaseNavigationPage, BaseNavigationPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ViewUser, ViewUserViewModel>();
            containerRegistry.RegisterForNavigation<AddUser, AddUserViewModel>();
            containerRegistry.RegisterForNavigation<AppHome>();
        }

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Container.Resolve<ILoggerFacade>().Log(e.Exception.ToString(), Category.Exception, Priority.None);
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }

    }
}
