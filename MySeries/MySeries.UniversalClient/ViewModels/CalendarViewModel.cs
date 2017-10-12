using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.Services;

namespace MySeries.UniversalClient.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private List<DateTime> addedDates = new List<DateTime>();

        public ObservableCollection<Episode> Episodes { get; set; } = new ObservableCollection<Episode>();
        //public ICommand Command { get; set; }

        //public CalendarViewModel()
        //{
        //    Command = new RelayCommand<CalendarViewSelectedDatesChangedEventArgs>( CalendarDateChanged );
        //}

        public async Task<List<Episode>> GetEpisodesOnDate( DateTime date )
        {
            if( addedDates.Contains( date ) )
            {
                return Episodes.Where( e => e.AirDate == date ).ToList();
            }

            return await GetEpisodesInInterval( date, date );
        }


        public async Task<List<Episode>> GetEpisodesInInterval( DateTime startDate, DateTime endDate )
        {
            var token = await ApplicationData.Current.LocalSettings.ReadAsync<Token>( "MySeriesApiToken" );
            var episodes = await MySeriesDataService.GetEpisodesInInterval( token, startDate, endDate );

            for( DateTime d = startDate; d <= endDate; d = d.AddDays( 1 ) )
            {
                var episodesOnDay = episodes.Where( e => e.AirDate == d );
                foreach( var episode in episodesOnDay )
                {
                    Episodes.Add( episode );
                }
                this.addedDates.Add( d );
            }
            return episodes;
        }

        public async Task Initialize()
        {
            //TODO
            var Username = "gyory.marcell@gmail.com";
            var Password = "MyPassword!";
            var token = await MySeriesDataService.GetTokenAsync( Username, Password );
            await ApplicationData.Current.LocalSettings.SaveAsync<Token>( "MySeriesApiToken", token );
            //TODO


            var date = DateTime.Today;
            var firstDayOfMonth = new DateTime( date.Year, date.Month, 1 );
            var lastDayOfMonth = firstDayOfMonth.AddMonths( 1 ).AddDays( -1 );

            await GetEpisodesInInterval( firstDayOfMonth, lastDayOfMonth );
        }
    }
}
