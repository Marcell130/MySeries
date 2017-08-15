using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MySeries.UniversalClient.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.ViewModels
{
    public class MasterDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        //const string NarrowStateName = "NarrowState";
        //const string WideStateName = "WideState";

        //private VisualState _currentState;

        private Episode _selected;
        public Episode Selected
        {
            get => _selected;
            set => Set( ref _selected, value );
        }

        public ICommand ItemClickCommand { get; private set; }
        //public ICommand StateChangedCommand { get; private set; }

        public ObservableCollection<Episode> Episodes { get; private set; } = new ObservableCollection<Episode>();

        public MasterDetailViewModel()
        {
            ItemClickCommand = new RelayCommand<ItemClickEventArgs>( OnItemClick );
            //StateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>( OnStateChanged );
        }

        //private void OnStateChanged( VisualStateChangedEventArgs args )
        //{
        //    _currentState = args.NewState;
        //}

        private void OnItemClick( ItemClickEventArgs args )
        {
            var episode = args?.ClickedItem as Episode;
            if( episode != null )
            {
                //if( _currentState.Name == NarrowStateName )
                //{
                //    NavigationService.Navigate( typeof( MasterDetailDetailViewModel ).FullName, episode );
                //}
                //else
                //{
                    Selected = episode;
                //}
            }
        }

        public async Task Initialize( Season season )
        {
            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );

            var episodes = await MySeriesDataService.GetEpisodesInSeasonAsync( token, season.Id );
            Episodes.Clear();
            foreach( var episode in episodes )
            {
                Episodes.Add( episode );
            }
            Selected = Episodes.FirstOrDefault();
        }
    }
}
