using Windows.UI.Xaml;
using MySeries.UniversalClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class MasterDetailPage : Page
    {
        private MasterDetailViewModel ViewModel => DataContext as MasterDetailViewModel;

        public MasterDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo( NavigationEventArgs e )
        {
            //var currentState = e.Parameter as VisualState;
            //await ViewModel.Initialize( currentState );

            var season = e.Parameter as Season;
            await ViewModel.Initialize(season);
        }
    }
}
