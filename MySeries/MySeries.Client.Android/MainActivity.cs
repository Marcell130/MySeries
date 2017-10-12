using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using MvvmCross.Droid.Support.V4;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Android
{
    [Activity( LaunchMode = LaunchMode.SingleTop, MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar" )]
    public class MainActivity : MvxCachingFragmentActivity<MainViewModel>
    {
        BottomNavigationView bottomNavigation;

        protected override void OnCreate( Bundle bundle )
        {
            try
            {
                base.OnCreate( bundle );
                SetContentView( Resource.Layout.MainLayout );

                //var toolbar = FindViewById<MvvmCross.Droid.Support.V7. Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                //if (toolbar != null)
                //{
                //    SetSupportActionBar(toolbar);
                //    SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                //    SupportActionBar.SetHomeButtonEnabled(false);
                //}

                this.bottomNavigation = FindViewById<BottomNavigationView>( Resource.Id.bottom_navigation );

                //TODO MvxBind NavigationItemSelected a ViewModelhez
                this.bottomNavigation.NavigationItemSelected += async ( s, e ) => await LoadFragment( e.Item.ItemId );

                //LoadFragment(Resource.Id.menu_upcoming);
            }
            catch (BadParcelableException ex)
            {

            }
        }

        private async Task LoadFragment( int id )
        {
            switch (id)
            {
                case Resource.Id.menu_upcoming:
                    await ViewModel.ShowExploreFragment();
                    break;
                case Resource.Id.menu_explore:
                    await ViewModel.ShowExploreFragment();
                    break;
                case Resource.Id.menu_profile:
                    await ViewModel.ShowProfileFragment();
                    break;
            }
        }

        //public bool Show(MvxViewModelRequest request, Bundle bundle, Type fragmentType, MvxFragmentAttribute fragmentAttribute)
        //{
        //   var f =  FragmentManager.FindFragmentById(0);
        //    ShowFragment("Upcoming", Resource.Id.content_frame, bundle);
        //    return true;
        //}

        //public bool Close(IMvxViewModel viewModel)
        //{
        //    return true;
        //}
    }
}
