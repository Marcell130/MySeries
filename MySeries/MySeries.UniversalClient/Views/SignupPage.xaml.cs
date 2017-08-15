using Windows.UI.Xaml.Controls;
using MySeries.UniversalClient.ViewModels;

namespace MySeries.UniversalClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignupPage : Page
    {
        private SignupViewModel ViewModel => DataContext as SignupViewModel;

        public SignupPage()
        {
            InitializeComponent();
        }
    }
}
