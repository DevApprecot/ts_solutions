using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

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
        }
    }
}