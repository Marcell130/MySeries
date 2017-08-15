using MySeries.UniversalClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class MasterDetailDetailPage : Page
    {
        private MasterDetailDetailViewModel ViewModel => DataContext as MasterDetailDetailViewModel;

        public MasterDetailDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var episode = e.Parameter as Episode;
            ViewModel.Initialize(episode);
        }
    }
}
