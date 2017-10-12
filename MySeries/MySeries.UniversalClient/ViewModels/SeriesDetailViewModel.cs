using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class SeriesDetailViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        private TvShow _tvShow;

        public TvShow TvShow
        {
            get => _tvShow;
            set => Set( ref _tvShow, value );
        }

        public ObservableCollection<Season> Seasons { get; set; }

        private string _isAddedForUser;
        public string IsAddedLabel
        {
            get => this._isAddedForUser;
            set => Set( ref this._isAddedForUser, value );
        }

        private bool IsAddedForUser
        {
            get => IsAddedLabel == "+";
            set => IsAddedLabel = value ? "+" : "-";
        }

        public ICommand AddRemoveCommand { get; set; }

        public async Task Initialize( TvShow tvShow )
        {
            Seasons.Clear();

            TvShow = tvShow;
            AddRemoveCommand = new RelayCommand<ItemClickEventArgs>( AddRemoveTvShow );

            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );
            var seasons = await MySeriesDataService.GetSeasonsInTvShowAsync( token, tvShow.Id );
            foreach( var season in seasons )
            {
                Seasons.Add( season );
            }

            IsAddedForUser = await MySeriesDataService.IsTvShowForUserAsync( token, tvShow.Id );
        }

        private async void AddRemoveTvShow( ItemClickEventArgs args )
        {
            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );

            if( IsAddedForUser )
            {
                await MySeriesDataService.RemoveTvShowForUserAsync( token, TvShow.Id );
                IsAddedForUser = false;

            }
            else
            {
                await MySeriesDataService.AddTvShowForUserAsync( token, TvShow.Id );
                IsAddedForUser = true;

            }
        }


        public void ItemClickCommand( object sender, object parameter )
        {
            var arg = parameter as ItemClickEventArgs;
            var season = arg.ClickedItem as Season;
            NavigationService.Navigate( typeof( MasterDetailViewModel ).FullName, season );
        }


    }
}
