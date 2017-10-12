//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using MvvmCross.Binding.Droid.BindingContext;
//using MvvmCross.Binding.Droid.Views;
//using MvvmCross.Droid.Support.V7.RecyclerView;
//using MySeries.Core.ViewModels;

//namespace MySeries.Android.Adapters
//{
//    public class EndlessListAdapter : MvxRecyclerAdapter
//    {
//        private int _loadedItemCount;

//        public EndlessListAdapter(IMvxAndroidBindingContext context) : base(context)
//        {
//            LoadMore();
//        }

//        public override object GetItem(int position)
//        {
//            if (_loadedItemCount <= position + 1)
//            {
//                LoadMore();
//            }

//            return base.GetItem(position);
//        }

//        private void LoadMore()
//        {
//            Task.Run(async () =>
//            {
//                var loader = (IEndlessLoader)BindingContext.DataContext;
//                _loadedItemCount = await loader.LoadMore();
//            }).Wait();
//        }
//    }
//}