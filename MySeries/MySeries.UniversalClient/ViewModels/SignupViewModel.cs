using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Model.BindingModels;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class SignupViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        private SignupBindingModel _signupModel;

        public SignupBindingModel SignupModel
        {
            get => this._signupModel;
            set => Set( ref this._signupModel, value );
        }


        public ICommand CancelCommand { get; }
        public ICommand SignupCommand { get; }

        public SignupViewModel()
        {
            CancelCommand = new RelayCommand( NavigationService.GoBack );
            SignupCommand = new RelayCommand( Signup );
        }

        private async void Signup()
        {
            await MySeriesDataService.SignupAsync( SignupModel );
            NavigationService.Navigate( typeof( LoginViewModel ).FullName );
        }
    }
}
