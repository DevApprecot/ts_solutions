using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Graphics;

namespace Ts_Solutions.Droid.ItemDecorators
{
    public class ItemDecorator : RecyclerView.ItemDecoration
    {
        private readonly int _space;

        public ItemDecorator(int space)
        {
            _space = space;
        }


        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);

            outRect.Bottom = _space;
        }
    }
}