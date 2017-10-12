using System;
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
    public class SeriesListViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public ObservableCollection<TvShow> TvShows { get; } = new ObservableCollection<TvShow>();

        public void ItemClickCommand( object sender, object parameter )
        {
            var arg = parameter as ItemClickEventArgs;
            var tvShow = arg.ClickedItem as TvShow;
            NavigationService.Navigate( typeof( SeriesDetailViewModel ).FullName, tvShow );
        }


        public async void Initialize()
        {
            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );

            TvShows.Clear();

            //TODO
            var page = 1;
            var perPage = 20;

            var tvShows = await MySeriesDataService.GetAllSeriesAsync( token, page, perPage );
            foreach( var tvShow in tvShows )
            {
                TvShows.Add( tvShow );
            }
        }
    }
}
