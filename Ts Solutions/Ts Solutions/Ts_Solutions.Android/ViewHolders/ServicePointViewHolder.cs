using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using Ts_Solutions.IView;

namespace Ts_Solutions.Droid
{
    public class ServicePointViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; set; }
        public TextView Address { get; set; }
        public TextView Phone { get; set; }

        public ServicePointViewHolder(View itemView, IMainView view) : base (itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.tv_name);
            Address = itemView.FindViewById<TextView>(Resource.Id.tv_address);
            Phone = itemView.FindViewById<TextView>(Resource.Id.tv_phone);

            Phone.Click += (sender, args) =>
            {
                view.CallClicked(Phone.Text);
            };
        }
        
    }
}