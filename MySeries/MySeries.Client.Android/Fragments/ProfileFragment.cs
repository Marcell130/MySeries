using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Android.Fragments
{
    [MvxFragment( typeof( MainViewModel ), Resource.Id.content_frame, AddToBackStack = true )]
    [Register( "myseries.client.android.fragments.ProfileFragment" )]
    public class ProfileFragment : MvxFragment<ProfileViewModel>
    {
        public override View OnCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState )
        {
            base.OnCreateView( inflater, container, savedInstanceState );
            var view = this.BindingInflate( Resource.Layout.ProfileLayout, null );

            //TODO

            return view;
        }
    }
}
