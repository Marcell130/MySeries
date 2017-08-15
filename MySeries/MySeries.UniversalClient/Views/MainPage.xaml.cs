using MySeries.UniversalClient.ViewModels;

using Windows.UI.Xaml.Controls;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
