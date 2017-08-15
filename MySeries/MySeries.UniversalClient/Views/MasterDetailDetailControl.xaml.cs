using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MySeries.UniversalClient.Model;

namespace MySeries.UniversalClient.Views
{
    public sealed partial class MasterDetailDetailControl : UserControl
    {
        public Episode MasterMenuItem
        {
            get => GetValue(MasterMenuItemProperty) as Episode;
            set => SetValue(MasterMenuItemProperty, value);
        }

        public static DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem",typeof(Episode),typeof(MasterDetailDetailControl),new PropertyMetadata(null));

        public MasterDetailDetailControl()
        {
            InitializeComponent();
        }
    }
}
