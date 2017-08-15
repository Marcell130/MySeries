using System;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MySeries.UniversalClient.Services;

using Windows.UI.Xaml;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.ViewModels
{
    public class MasterDetailDetailViewModel : ViewModelBase
    {

        public NavigationServiceEx NavigationService => Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationServiceEx>();
        const string NarrowStateName = "NarrowState";
        const string WideStateName = "WideState";

        public ICommand StateChangedCommand { get; private set; }

        private Episode _episode;
        public Episode Episode
        {
            get => this._episode;
            set => Set(ref this._episode, value);
        }

        public MasterDetailDetailViewModel()
        {
            StateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(OnStateChanged);
        }
        
        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            if (args.OldState.Name == NarrowStateName && args.NewState.Name == WideStateName)
            {
                NavigationService.GoBack();
            }
        }

        public void Initialize(Episode episode)
        {

        }
    }
}
