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
using Com.Airbnb.Lottie;
using Android.Content;
using Ts_Solutions.Droid.Receivers;
using Android.Net;

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
        private Button _checkBtn;
        LottieAnimationView _animationView;
        private RelativeLayout _connection, _resultsView;
        private ConnectionReceiver _receiver;
        private ImageView _close;


        protected override int LayoutResource => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreatePresenter();

            InitViews();
            AddEventHandlers();

            Task.Run(async () => await _presenter.LoadServicePoints());
        }

        protected override void OnResume()
        {
            base.OnResume();

            _receiver = new ConnectionReceiver(_connection, this);
            RegisterReceiver(_receiver, new IntentFilter(ConnectivityManager.ConnectivityAction));
        }

        protected override void OnPause()
        {
            base.OnPause();

            UnregisterReceiver(_receiver);
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
            _connection = FindViewById<RelativeLayout>(Resource.Id.rl_connection);
            _animationView = FindViewById<LottieAnimationView>(Resource.Id.animation_view);
            _viewIcon = FindViewById<ImageView>(Resource.Id.iv_map);
            _mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.frm_map) as SupportMapFragment;
            _spRecyclerView = FindViewById<RecyclerView>(Resource.Id.rv_service_points);
            _spRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            _spRecyclerView.AddItemDecoration(new ItemDecorator(1));
            _checkBtn = FindViewById<Button>(Resource.Id.btn_check);
        }

        private void AddEventHandlers()
        {
            _viewIcon.Click += ChangeViewTypeClicked;
            _checkBtn.Click += _checkBtn_Click;
        }

        private void _checkBtn_Click(object sender, EventArgs e)
        {
            _presenter.ButtonCheckTapped();
        }

        private void DisposeItems()
        {
            _checkBtn.Dispose();
            _checkBtn = null;
            _presenter = null;
            _servicePoints = null;
            _spRecyclerView.Dispose();
            _spRecyclerView = null;
            _mapFragment.Dispose();
            _mapFragment = null;
            _viewIcon.SetImageDrawable(null);
            _viewIcon.Dispose();
            _animationView.Dispose();
            _receiver.Dispose();
            _receiver = null;
            _connection.Dispose();
            _connection = null;
        }

        private void RemoveEventHandlers()
        {
            _viewIcon.Click -= ChangeViewTypeClicked;
        }

        private void ChangeViewTypeClicked(object sender, EventArgs e)
        {
            _presenter.ChangeViewTypeClicked();
        }


        public override void SetLoading(bool isLoading)
        {
            RunOnUiThread(() =>
            {
                if (isLoading)
                {
                    _animationView.Visibility = ViewStates.Visible;
                    _spRecyclerView.Visibility = ViewStates.Gone;
                    _mapFragment.View.Visibility = ViewStates.Gone;
                 }
             else
                 _animationView.Visibility = ViewStates.Gone;
            });
        }

        public void SetList(List<ServicePoint> points)
        {
            _spRecyclerView.Visibility = ViewStates.Visible; 
            _mapFragment.View.Visibility = ViewStates.Gone;
            _servicePoints = points;
            RunOnUiThread(() =>
            {
                _mapFragment.View.Visibility = ViewStates.Gone;
                _spRecyclerView.Visibility = ViewStates.Visible;
                _viewIcon.SetImageDrawable(ContextCompat.GetDrawable(ApplicationContext, Resource.Drawable.ic_map));
                var adapter = new ServicePointsAdapter(points, this);
                _spRecyclerView.SetAdapter(adapter);
            });
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

        public override Task OnConnected()
        {
            return Task.Run(async () => await _presenter.LoadServicePoints());
        }

        public void SetMarkers(List<ServicePoint> points)
        {
            _servicePoints = points;
            RunOnUiThread(() =>
            {
                _spRecyclerView.Visibility = ViewStates.Gone;
                _mapFragment.View.Visibility = ViewStates.Visible;
                _viewIcon.SetImageDrawable(ContextCompat.GetDrawable(ApplicationContext, Resource.Drawable.ic_list));
                _mapFragment?.GetMapAsync(this);
            });
        }

        public void CallClicked(string phone)
        {
            _presenter.Call(phone);
        }

        public void ShowStatus(string status)
        {
        }

        public void CallNumber(string phone)
        {
            try
            {
                var intent = new Intent(Intent.ActionDial);
                intent.SetData(Android.Net.Uri.Parse($"tel:{phone}"));
                StartActivityForResult(intent, 7000);
            }
            catch (ActivityNotFoundException)
            {
                Console.WriteLine("Activity not found");
            }
        }
    }
}