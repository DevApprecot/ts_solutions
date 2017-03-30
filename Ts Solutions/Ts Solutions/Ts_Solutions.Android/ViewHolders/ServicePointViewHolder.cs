using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace Ts_Solutions.Droid
{
    public class ServicePointViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; set; }
        public TextView Address { get; set; }
        public TextView Phone { get; set; }

        public ServicePointViewHolder(View view) : base (view)
        {
            Name = view.FindViewById<TextView>(Resource.Id.tv_name);
            Address = view.FindViewById<TextView>(Resource.Id.tv_address);
            Phone = view.FindViewById<TextView>(Resource.Id.tv_phone);

            Phone.Click += (sender, args) =>
            {
                try
                {
                    var intent = new Intent(Intent.ActionDial);
                    intent.SetData(Android.Net.Uri.Parse($"tel:{Phone.Text}"));
                    view.Context.StartActivity(intent);
                }
                catch (ActivityNotFoundException)
                {
                    Console.WriteLine("Activity not found");
                }
            };
        }
        
    }
}