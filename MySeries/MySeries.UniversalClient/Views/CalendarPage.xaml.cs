using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MySeries.UniversalClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        private CalendarViewModel ViewModel => DataContext as CalendarViewModel;
        private bool isInitialized;

        public CalendarPage()
        {
            this.InitializeComponent();

        }

        protected override async void OnNavigatedTo( NavigationEventArgs e )
        {

            await ViewModel.Initialize();
            this.isInitialized = true;
        }

        private void CalendarView_OnCalendarViewDayItemChanging( CalendarView sender, CalendarViewDayItemChangingEventArgs args )
        {
            if( isInitialized )
            {
                CalendarViewDayItem localItem = args.Item;
                if( localItem != null && !args.InRecycleQueue && localItem.DataContext == null )
                {
                    localItem.DataContext = ViewModel.GetEpisodesOnDate( localItem.Date.Date );
                }
            }
        }

        private void CalendarView_OnSelectedDatesChanged( CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args )
        {
            throw new NotImplementedException();
        }
    }
}
