using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySeries.UniversalClient.Model;
using MySeries.UniversalClient.ViewModels;

namespace MySeries.UniversalClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SeriesDetailPage : Page
    {
        private SeriesDetailViewModel ViewModel => DataContext as SeriesDetailViewModel;

        public SeriesDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            var tvShow = e.Parameter as TvShow;
            ViewModel.Initialize( tvShow );
        }
    }
}
