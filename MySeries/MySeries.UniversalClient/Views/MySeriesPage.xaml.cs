using MySeries.UniversalClient.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class MySeriesPage : Page
    {
        private MySeriesViewModel ViewModel => DataContext as MySeriesViewModel;

        public MySeriesPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            var userInfo = e.Parameter as UserInfo;
            ViewModel.Initialize( userInfo );
        }

    }
}
