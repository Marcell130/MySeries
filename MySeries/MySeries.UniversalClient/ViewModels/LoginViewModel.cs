using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        private string _username;

        public string Username
        {
            get => _username;
            set => Set( ref _username, value );
        }

        [DataType( DataType.Password )]
        private string _password;

        [DataType( DataType.Password )]
        public string Password
        {
            get => _password;
            set => Set( ref _password, value );
        }

        public ICommand LoginCommand { get; }
        public ICommand SignupCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand( Login );
            SignupCommand = new RelayCommand( NavigateToSignupPage );
        }

        private async void Login()
        {
            var token = await MySeriesDataService.LoginAsync( Username, Password );
            await ApplicationData.Current.LocalSettings.SaveAsync<Token>( "MySeriesApiToken", token );
            var userInfo = await MySeriesDataService.GetUserInfoAsync( token );

            NavigationService.Navigate( typeof( MySeriesViewModel ).FullName, userInfo );
        }

        private void NavigateToSignupPage()
        {
            NavigationService.Navigate( typeof( SignupViewModel ).FullName );
        }

        public void Initialize()
        {
            //TODO
            Username = "gyory.marcell@gmail.com";
            Password = "MyPassword!";
        }
    }
}
