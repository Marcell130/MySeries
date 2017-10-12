using System;
using Android.Support.V7.Widget;

namespace MySeries.Client.Android.Adapters
{
    public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;

        private readonly LinearLayoutManager _layoutManager;
        private int lastLoaded = 0;
        public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            this._layoutManager = layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var pastVisiblesItems = this._layoutManager.FindFirstVisibleItemPosition();
            var totalItemCount = recyclerView.GetAdapter().ItemCount;

            var lastItemPosition = visibleItemCount + pastVisiblesItems;

            if (this.lastLoaded != lastItemPosition && lastItemPosition >= totalItemCount)
            {
                LoadMoreEvent?.Invoke(this, null);
                this.lastLoaded = recyclerView.GetAdapter().ItemCount;
            }
        }
    }
}