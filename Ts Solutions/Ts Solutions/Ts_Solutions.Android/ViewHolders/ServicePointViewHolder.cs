using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using Ts_Solutions.IView;
using Ts_Solutions.Model;

namespace Ts_Solutions.Droid
{
    public class ServicePointViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; set; }
        public TextView Address { get; set; }
        public TextView Phone { get; set; }
        public LinearLayout Call { get; set; }
        public AppCompatImageView Directions { get; set; }
        public ServicePoint ServicePoint { get; set; }

        public ServicePointViewHolder(View itemView, IMainView view) : base (itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
            Address = itemView.FindViewById<TextView>(Resource.Id.tv_address);
            Phone = itemView.FindViewById<TextView>(Resource.Id.tv_phone);
            Call = itemView.FindViewById<LinearLayout>(Resource.Id.ll_call);
            Directions = itemView.FindViewById<AppCompatImageView>(Resource.Id.iv_directions);

            Directions.Click += (sender, args) =>
            {
                view.DirectionsClicked(ServicePoint);
            };

            Call.Click += (sender, args) =>
            {
                view.CallClicked(Phone.Text);
            };
        }
        
    }
}