using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MySeries.Client.Android.Adapters;
using MySeries.Client.Core.ViewModels;

namespace MySeries.Client.Android.Fragments
{
    [MvxFragment( typeof( MainViewModel ), Resource.Id.content_frame, AddToBackStack = true )]
    [Register( "myseries.client.android.fragments.ExploreFragment" )]
    public class ExploreFragment : MvxFragment<ExploreViewModel>
    {
        public override View OnCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState )
        {
            base.OnCreateView( inflater, container, savedInstanceState );
            var view = this.BindingInflate( Resource.Layout.ExploreLayout, null );

            var recyclerView = view.FindViewById<MvxRecyclerView>( Resource.Id.series_list );

            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;

                var layoutManager = new LinearLayoutManager( view.Context );

                var onScrollListener = new XamarinRecyclerViewOnScrollListener( layoutManager );
                onScrollListener.LoadMoreEvent += async ( s, e ) =>
                {
                    await ViewModel.LoadMoreAsync();
                };

                recyclerView.AddOnScrollListener( onScrollListener );

                recyclerView.SetLayoutManager( layoutManager );

                recyclerView.GetRecycledViewPool().Clear();
            }

            return view;
        }
    }
}
