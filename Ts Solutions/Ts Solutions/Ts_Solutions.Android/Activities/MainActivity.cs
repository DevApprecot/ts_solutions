using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using System.Threading.Tasks;
using Android.Gms.Maps;
using Android.Support.V7.Widget;
using Ts_Solutions.Droid.ItemDecorators;
using Ts_Solutions.Droid.Adapters;
using Android.Views;
using Android.Gms.Maps.Model;
using Java.Lang;
using Ts_Solutions.IView;
using Ts_Solutions.Presenter;
using Ts_Solutions.Model;
using Android.Widget;
using Android.Support.V4.Content;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(Theme = "@style/TsTheme")]
    public class MainActivity : BaseActivity, IMainView, IOnMapReadyCallback
    {
        MainPresenter _presenter;
        SupportMapFragment _mapFragment;
        RecyclerView _spRecyclerView;
        List<ServicePoint> _servicePoints;
        ImageView _viewIcon;

        protected override int LayoutResource => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreatePresenter();

            InitViews();
            AddEventHandlers();

            Task.Run(async () => await _presenter.LoadServicePoints());
        }

        protected override void OnDestroy()
        {
            RemoveEventHandlers();
            DisposeItems();
            base.OnDestroy();
        }

        public void CreatePresenter()
        {
            if (_presenter == null) _presenter = new MainPresenter(this);
        }

        private void InitViews()
        {
            _viewIcon = FindViewById<ImageView>(Resource.Id.iv_map);
            _mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.frm_map) as SupportMapFragment;

            _spRecyclerView = FindViewById<RecyclerView>(Resource.Id.rv_service_points);
            _spRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            _spRecyclerView.AddItemDecoration(new ItemDecorator(1));
            _spRecyclerView.Visibility = ViewStates.Gone;
        }

        private void AddEventHandlers()
        {
            _viewIcon.Click += ChangeViewTypeClicked;
        }

        private void DisposeItems()
        {
            _presenter = null;
            _servicePoints = null;
            _spRecyclerView.Dispose();
            _spRecyclerView = null;
            _mapFragment.Dispose();
            _mapFragment = null;
            _viewIcon.SetImageDrawable(null);
            _viewIcon.Dispose();
        }

        private void RemoveEventHandlers()
        {
            _viewIcon.Click -= ChangeViewTypeClicked;
        }

        private void ChangeViewTypeClicked(object sender, EventArgs e)
        {
            _presenter.ChangeViewTypeClicked();
        }


        public void SetLoading(bool isLoading)
        {
            Console.WriteLine("Loading " + isLoading);
        }

        public void SetList(List<ServicePoint> points)
        {
            _servicePoints = points;
            RunOnUiThread(() =>
            {
                _viewIcon.SetImageDrawable(ContextCompat.GetDrawable(ApplicationContext, Resource.Drawable.ic_map));
                var adapter = new ServicePointsAdapter(points);
                _spRecyclerView.SetAdapter(adapter);
            });
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowStatus()
        {
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            using (var builder = new LatLngBounds.Builder())
            {

                for (var i = 0; i < _servicePoints.Count; i++)
                {
                    var coordinate1 = new LatLng(_servicePoints[i].Lat, _servicePoints[i].Lon);

                    builder.Include(coordinate1);
                    try
                    {
                        googleMap.AddMarker(new MarkerOptions()
                        .SetPosition(coordinate1)
                        .SetTitle((i + 1) + ". " + _servicePoints[i].Name)
                        .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_toolbar_logo))
                        .SetSnippet(_servicePoints[i].Address));
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    catch (System.Exception)
                    {
                    }
                }

                try
                {
                    await Task.Delay(500);
                    var bounds = builder.Build();
                    googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 80));
                }
                catch (IllegalStateException)
                {
                    //_myMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(_searchModel.UserLat, _searchModel.UserLon), 11));
                }
            }
        }

        public void SetMarkers(List<ServicePoint> points)
        {
            _servicePoints = points;
            RunOnUiThread(() =>
            {
                _viewIcon.SetImageDrawable(ContextCompat.GetDrawable(ApplicationContext, Resource.Drawable.ic_list));
                _mapFragment?.GetMapAsync(this);
            });
        }

        public void CallClicked(string phone)
        {
            throw new NotImplementedException();
        }

        public void CallNumber(string phone)
        {
            throw new NotImplementedException();
        }
    }
}