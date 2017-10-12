using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Practices.ServiceLocation;

using MySeries.UniversalClient.Helpers;
using MySeries.UniversalClient.Services;
using MySeries.UniversalClient.Views;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MySeries.UniversalClient.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private const string PanoramicStateName = "PanoramicState";
        private const string WideStateName = "WideState";
        private const string NarrowStateName = "NarrowState";
        private const double WideStateMinWindowWidth = 640;
        private const double PanoramicStateMinWindowWidth = 1024;

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        private bool _isPaneOpen;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => Set(ref _isPaneOpen, value);
        }

        private SplitViewDisplayMode _displayMode = SplitViewDisplayMode.CompactInline;
        public SplitViewDisplayMode DisplayMode
        {
            get => _displayMode;
            set => Set(ref _displayMode, value);
        }

        private ShellNavigationItem _lastSelectedItem;

        private ObservableCollection<ShellNavigationItem> _primaryItems = new ObservableCollection<ShellNavigationItem>();
        public ObservableCollection<ShellNavigationItem> PrimaryItems
        {
            get => _primaryItems;
            set => Set(ref _primaryItems, value);
        }

        private ObservableCollection<ShellNavigationItem> _secondaryItems = new ObservableCollection<ShellNavigationItem>();
        public ObservableCollection<ShellNavigationItem> SecondaryItems
        {
            get => _secondaryItems;
            set => Set(ref _secondaryItems, value);
        }

        private ICommand _openPaneCommand;
        public ICommand OpenPaneCommand
        {
            get { return this._openPaneCommand ?? (this._openPaneCommand = new RelayCommand(() => IsPaneOpen = !this._isPaneOpen)); }
        }

        private ICommand _itemSelected;
        public ICommand ItemSelectedCommand => this._itemSelected ?? (this._itemSelected = new RelayCommand<ItemClickEventArgs>(ItemSelected));

        private ICommand _stateChangedCommand;
        public ICommand StateChangedCommand
        {
            get { return this._stateChangedCommand ?? (this._stateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(args => GoToState(args.NewState.Name))); }
        }

        private void GoToState(string stateName)
        {
            switch (stateName)
            {
                case PanoramicStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    break;
                case WideStateName:
                    DisplayMode = SplitViewDisplayMode.CompactInline;
                    IsPaneOpen = false;
                    break;
                case NarrowStateName:
                    DisplayMode = SplitViewDisplayMode.Overlay;
                    IsPaneOpen = false;
                    break;
            }
        }

        public void Initialize(Frame frame)
        {
            NavigationService.Frame = frame;
            NavigationService.Frame.Navigated += NavigationService_Navigated;
            PopulateNavItems();

            InitializeState(Window.Current.Bounds.Width);
        }

        private void InitializeState(double windowWith)
        {
            if (windowWith < WideStateMinWindowWidth)
            {
                GoToState(NarrowStateName);
            }
            else if (windowWith < PanoramicStateMinWindowWidth)
            {
                GoToState(WideStateName);
            }
            else
            {
                GoToState(PanoramicStateName);
            }
        }

        private void PopulateNavItems()
        {
            _primaryItems.Clear();
            _secondaryItems.Clear();

            // TODO WTS: Change the symbols for each item as appropriate for your app
            // More on Segoe UI Symbol icons: https://docs.microsoft.com/windows/uwp/style/segoe-ui-symbol-font
            // Or to use an IconElement instead of a Symbol see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/projectTypes/navigationpane.md
            // Edit String/en-US/Resources.resw: Add a menu item title for each page

            //_primaryItems.Add( new ShellNavigationItem( "Shell_Main".GetLocalized(), Symbol.Document, typeof( MainViewModel ).FullName ) );
            _primaryItems.Add( new ShellNavigationItem( "Shell_MySeries".GetLocalized(), Symbol.Document, typeof( MySeriesViewModel ).FullName ) );
            _primaryItems.Add( new ShellNavigationItem( "Shell_Tabbed".GetLocalized(), Symbol.Document, typeof( SeriesListViewModel ).FullName ) );
            _primaryItems.Add( new ShellNavigationItem( "Shell_MasterDetail".GetLocalized(), Symbol.Document, typeof( CalendarViewModel ).FullName ) );
            _secondaryItems.Add( new ShellNavigationItem( "Shell_Settings".GetLocalized(), Symbol.Setting, typeof( SettingsViewModel ).FullName ) );
        }

        private void ItemSelected(ItemClickEventArgs args)
        {
            if (DisplayMode == SplitViewDisplayMode.CompactOverlay || DisplayMode == SplitViewDisplayMode.Overlay)
            {
                IsPaneOpen = false;
            }
            Navigate(args.ClickedItem);
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            if (e != null)
            {
                var vm = NavigationService.GetNameOfRegisteredPage(e.SourcePageType);
                var navigationItem = PrimaryItems?.FirstOrDefault(i => i.ViewModelName == vm) ?? SecondaryItems?.FirstOrDefault(i => i.ViewModelName == vm);

                if (navigationItem != null)
                {
                    ChangeSelected(_lastSelectedItem, navigationItem);
                    _lastSelectedItem = navigationItem;
                }
            }
        }

        private void ChangeSelected( ShellNavigationItem oldValue, ShellNavigationItem newValue )
        {
            if (oldValue != null)
            {
                oldValue.IsSelected = false;
            }
            if (newValue != null)
            {
                newValue.IsSelected = true;
            }
        }

        private void Navigate(object item)
        {
            var navigationItem = item as ShellNavigationItem;
            if (navigationItem != null)
            {
                NavigationService.Navigate(navigationItem.ViewModelName);
            }
        }
    }
}
