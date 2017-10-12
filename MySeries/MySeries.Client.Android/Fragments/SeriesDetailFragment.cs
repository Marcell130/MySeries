using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Android.Fragments
{
    [MvxFragment( typeof( MainViewModel ), Resource.Id.content_frame, AddToBackStack = true )]
    [Register( "myseries.client.android.fragments.SeriesDetailFragment" )]
    public class SeriesDetailFragment : MvxFragment<SeriesDetailViewModel>
    {

        public override View OnCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState )
        {
            base.OnCreateView( inflater, container, savedInstanceState );
            var view = this.BindingInflate( Resource.Layout.SeriesDetailLayout, null );

            var recyclerView = view.FindViewById<MvxRecyclerView>( Resource.Id.series_list );
            var layoutManager = new LinearLayoutManager( Context, LinearLayoutManager.Horizontal, false );
            recyclerView.SetLayoutManager( layoutManager );

            return view;
        }
    }
}
