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
using Ts_Solutions.Model;

namespace Ts_Solutions.Droid.Adapters
{
    public class ServicePointsAdapter : RecyclerView.Adapter
    {
        private List<ServicePoint> _servicePoints;

        public ServicePointsAdapter(List<ServicePoint> servicePoints)
        {
            _servicePoints = servicePoints;
        }

        public override int ItemCount => _servicePoints.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as ServicePointViewHolder;
            if (vh == null) return;

            vh.Name.Text = _servicePoints[position].Name;
            vh.Address.Text = _servicePoints[position].Address;
            vh.Phone.Text = _servicePoints[position].Phone;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.element_rv_servicepoints, parent, false);

            var vh = new ServicePointViewHolder(view);
            return vh;
        }
    }
}