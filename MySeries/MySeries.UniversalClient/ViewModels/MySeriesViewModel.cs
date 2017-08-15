using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class MySeriesViewModel : ViewModelBase
    {
        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public ObservableCollection<TvShow> TvShows { get; } = new ObservableCollection<TvShow>();

        public void ItemClickCommand( object sender, object parameter )
        {
            var arg = parameter as ItemClickEventArgs;
            var tvShow = arg.ClickedItem as TvShow;
            NavigationService.Navigate( typeof( SeriesDetailViewModel ).FullName, tvShow );
        }


        public async void Initialize( UserInfo userInfo )
        {
            //TODO do something with user info (like Welcome, XY!), or ask for custom offers
            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );

            TvShows.Clear();
            var tvShows = await MySeriesDataService.GetUserSeriesAsync( token );
            foreach( var tvShow in tvShows )
            {
                TvShows.Add( tvShow );
            }
        }
    }
}
