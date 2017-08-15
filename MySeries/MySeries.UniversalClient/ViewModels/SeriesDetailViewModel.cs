using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class SeriesDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        //public ObservableCollection<Season> Seasons { get; } = new ObservableCollection<Season>();

        private TvShow _tvShow;

        public TvShow TvShow
        {
            get => _tvShow;
            set => Set( ref _tvShow, value );
        }

        public void Initialize( TvShow tvShow )
        {
            TvShow = tvShow;
        }

        public void ItemClickCommand( object sender, object parameter )
        {
            var arg = parameter as ItemClickEventArgs;
            var season = arg.ClickedItem as Season;
            NavigationService.Navigate( typeof( MasterDetailViewModel ).FullName, season );
        }
    }
}
