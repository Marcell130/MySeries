using MySeries.UniversalClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class SeriesListPage : Page
    {
        private SeriesListViewModel ViewModel => DataContext as SeriesListViewModel;

        public SeriesListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            ViewModel.Initialize();
        }
    }
}
