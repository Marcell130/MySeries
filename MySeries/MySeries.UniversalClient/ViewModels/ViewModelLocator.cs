using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;

using MySeries.UniversalClient.Services;
using MySeries.UniversalClient.Views;

namespace MySeries.UniversalClient.ViewModels
{
    public class ViewModelLocator
    {
        readonly NavigationServiceEx navigationService = new NavigationServiceEx();

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider( () => SimpleIoc.Default );

            SimpleIoc.Default.Register( () => this.navigationService );
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<LoginViewModel, LoginPage>();
            Register<SignupViewModel, SignupPage>();
            //Register<MainViewModel, MainPage>();
            Register<MySeriesViewModel, MySeriesPage>();
            Register<SeriesDetailViewModel, SeriesDetailPage>();
            Register<MasterDetailViewModel, MasterDetailPage>();
            Register<MasterDetailDetailViewModel, MasterDetailDetailPage>();
            //Register<TabbedViewModel, TabbedPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public LoginViewModel LoginViewModel => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public SignupViewModel SignupViewModel => ServiceLocator.Current.GetInstance<SignupViewModel>();

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        //public TabbedViewModel TabbedViewModel => ServiceLocator.Current.GetInstance<TabbedViewModel>();

        public MasterDetailDetailViewModel MasterDetailDetailViewModel => ServiceLocator.Current.GetInstance<MasterDetailDetailViewModel>();

        public MasterDetailViewModel MasterDetailViewModel => ServiceLocator.Current.GetInstance<MasterDetailViewModel>();

        public MySeriesViewModel MySeriesViewModel => ServiceLocator.Current.GetInstance<MySeriesViewModel>();
        public SeriesDetailViewModel SeriesDetailViewModel => ServiceLocator.Current.GetInstance<SeriesDetailViewModel>();

        //public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public void Register<TViewModel, TView>() where TViewModel : class
        {
            SimpleIoc.Default.Register<TViewModel>();

            this.navigationService.Configure( typeof( TViewModel ).FullName, typeof( TView ) );
        }
    }
}
