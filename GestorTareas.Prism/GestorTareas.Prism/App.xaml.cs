using GestorTareas.Prism.ViewModels;
using GestorTareas.Prism.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GestorTareas.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<ManagerHomePage, ManagerHomePageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterSchedule, RegisterScheduleViewModel>();
            containerRegistry.RegisterForNavigation<RegisterTask, RegisterTaskViewModel>();
            containerRegistry.RegisterForNavigation<AssignedScholarInfo, AssignedScholarInfoViewModel>();
            containerRegistry.RegisterForNavigation<RegisteredUsersList, RegisteredUsersListViewModel>();
            containerRegistry.RegisterForNavigation<AddUser, AddUserViewModel>();
            containerRegistry.RegisterForNavigation<HomePageScholar, HomePageScholarViewModel>();
            containerRegistry.RegisterForNavigation<ActivityDetail, ActivityDetailViewModel>();
        }
    }
}
