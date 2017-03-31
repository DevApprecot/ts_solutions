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
using Ts_Solutions.IView;
using Android.Net;
using Android.Graphics;
using Android.Views.Animations;

namespace Ts_Solutions.Droid.Receivers
{
    public class ConnectionReceiver : BroadcastReceiver
    {
        private readonly RelativeLayout _connectionView;
        private readonly IBaseView _view;

        public ConnectionReceiver(RelativeLayout connectionView, IBaseView view)
        {
            _connectionView = connectionView;
            _view = view;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            using(var cm = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService))
            {
                var currentNetworkInfo = cm.ActiveNetworkInfo;

                if (currentNetworkInfo != null)
                    if (currentNetworkInfo.IsConnectedOrConnecting)
                    {
                        var textView = _connectionView.FindViewById<TextView>(Resource.Id.connection);
                        textView.SetText(Resource.String.connection_receiver_correct);
                        _connectionView.SetBackgroundColor(Color.ParseColor("#009900"));

                        var fadeOutAnimation = new AlphaAnimation(1f, 0f);
                        fadeOutAnimation.FillAfter = true;
                        fadeOutAnimation.Duration = 2000;
                        fadeOutAnimation.Interpolator = new AccelerateDecelerateInterpolator();
                        fadeOutAnimation.AnimationEnd += delegate
                        {
                            _connectionView.Alpha = 0;
                            if (_connectionView.Visibility != ViewStates.Gone)
                            {
                                _connectionView.Visibility = ViewStates.Gone;
                                _view.OnConnected();
                            }
                        };
                        _connectionView.StartAnimation(fadeOutAnimation);
                    }
                    else
                    {
                        var textView = _connectionView.FindViewById<TextView>(Resource.Id.connection);
                        textView.SetText(Resource.String.connection_receiver_error);
                        _connectionView.SetBackgroundColor(Color.ParseColor("#EF6C00"));
                        _connectionView.Visibility = ViewStates.Visible;

                        var fadeInAnimation = new AlphaAnimation(0f, 1f)
                        {
                            FillAfter = true,
                            Duration = 1000,
                            Interpolator = new AccelerateDecelerateInterpolator()
                        };

                        _connectionView.StartAnimation(fadeInAnimation);
                        _connectionView.Alpha = 1;
                    }
                else
                {
                    var textView = _connectionView.FindViewById<TextView>(Resource.Id.connection);
                    textView.SetText(Resource.String.connection_receiver_error);
                    _connectionView.SetBackgroundColor(Color.ParseColor("#EF6C00"));

                    _connectionView.Visibility = ViewStates.Visible;

                    var fadeInAnimation = new AlphaAnimation(0f, 1f)
                    {
                        FillAfter = true,
                        Duration = 1000,
                        Interpolator = new AccelerateDecelerateInterpolator()
                    };

                    _connectionView.StartAnimation(fadeInAnimation);
                    _connectionView.Alpha = 1;
                }
            }
            
            
        }
    }
}