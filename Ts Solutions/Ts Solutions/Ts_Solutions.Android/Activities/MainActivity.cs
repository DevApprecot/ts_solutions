using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Ts_Solutions.Model;
using Ts_Solutions.Presenter;
using System.Threading.Tasks;
using Ts_Solutions.IView;
using Android.Gms.Maps;
using Android.Support.V7.Widget;
using Ts_Solutions.Droid.ItemDecorators;
using Ts_Solutions.Droid.Adapters;
using Android.Views;
using Android.Gms.Maps.Model;
using Java.Lang;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(Theme = "@style/TsTheme")]
    public class MainActivity : BaseActivity, IMainView, IOnMapReadyCallback
    {
        MainPresenter _presenter;
        private SupportMapFragment _mapFragment;
        private RecyclerView _spRecyclerView;
        private List<ServicePoint> _servicePoints;

        protected override int LayoutResource => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreatePresenter();

            InitViews();

            Task.Run(async () => await _presenter.LoadServicePoints());
        }

        private void InitViews()
        {
            _mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.frm_map) as SupportMapFragment;

            _spRecyclerView = FindViewById<RecyclerView>(Resource.Id.rv_service_points);
            _spRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            _spRecyclerView.AddItemDecoration(new ItemDecorator(1));
            _spRecyclerView.Visibility = ViewStates.Gone;
        }

        protected override void OnDestroy()
        {
            _presenter = null;
            base.OnDestroy();
        }

        public void CreatePresenter()
        {
            if (_presenter == null) _presenter = new MainPresenter(this);
        }

        public void SetLoading(bool isLoading)
        {
            Console.WriteLine("Loading " + isLoading);
        }

        public void SetMarkers(List<ServicePoint> points)
        {
          // _servicePoints = points;
          // RunOnUiThread(() =>
          // {
          //     //var adapter = new ServicePointsAdapter(points);
          //     //_spRecyclerView.SetAdapter(adapter);
          //     _mapFragment?.GetMapAsync(this);
          // });
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowNoNet()
        {
        }

        public void ShowStatus()
        {
        }

        public void SwitchView()
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

        public void SetList(List<ServicePoint> points)
        {
        }
    }
}