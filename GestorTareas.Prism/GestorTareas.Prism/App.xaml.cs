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

            await NavigationService.NavigateAsync("NavigationPage/ChangePassword");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<ManagerHomePage, ManagerHomePageViewModel>();
            containerRegistry.RegisterForNavigation<ScholarHomePage, ScholarHomePageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterSchedule, RegisterScheduleViewModel>();
            containerRegistry.RegisterForNavigation<RegisterTask, RegisterTaskViewModel>();
            containerRegistry.RegisterForNavigation<AssignedScholarInfo, AssignedScholarInfoViewModel>();
            containerRegistry.RegisterForNavigation<RegisteredScholarsList, RegisteredUsersListViewModel>();
            containerRegistry.RegisterForNavigation<AddNewScholar, AddUserViewModel>();
            containerRegistry.RegisterForNavigation<TaskDetail, ActivityDetailViewModel>();

            containerRegistry.RegisterForNavigation<AdminHomePage, AdminHomePageViewModel>();
            containerRegistry.RegisterForNavigation<ScholarsAssigned, ScholarsAssignedViewModel>();
            containerRegistry.RegisterForNavigation<RegisteredAreaManagersList, RegisteredAreaManagersListViewModel>();
            containerRegistry.RegisterForNavigation<AddNewAreaManager, AddNewAreaManagerViewModel>();
            containerRegistry.RegisterForNavigation<AssignedTasks, AssignedTasksViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ChangePassword, ChangePasswordViewModel>();
            containerRegistry.RegisterForNavigation<ScholarsPaidHours, ScholarsPaidHoursViewModel>();
            containerRegistry.RegisterForNavigation<ScholarHoursDetail, ScholarHoursDetailViewModel>();
            containerRegistry.RegisterForNavigation<ScholarsRequests, ScholarsRequestsViewModel>();
            containerRegistry.RegisterForNavigation<NewScholarRequest, NewScholarRequestViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetail, RequestDetailViewModel>();
            containerRegistry.RegisterForNavigation<TaskProgress, TaskProgressViewModel>();
            containerRegistry.RegisterForNavigation<ReassingnTask, ReassingnTaskViewModel>();
            containerRegistry.RegisterForNavigation<AreaManagersScholarsRequests, AreaManagersScholarsRequestsViewModel>();
            containerRegistry.RegisterForNavigation<RequestValidation, RequestValidationViewModel>();
            containerRegistry.RegisterForNavigation<RequestDenegationReasons, RequestDenegationReasonsViewModel>();
            
        }
    }
}
