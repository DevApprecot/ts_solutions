using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using System;
using Ts_Solutions.IView;

namespace Ts_Solutions.Droid
{
    public abstract class BaseActivity : AppCompatActivity, IBaseView
    {
        protected abstract int LayoutResource { get; }

        public bool IsOnline()
        {
            using (var manager = GetSystemService(ConnectivityService) as Android.Net.ConnectivityManager)
            {
                var netInfo = manager.ActiveNetworkInfo;

                if (netInfo != null && netInfo.IsConnectedOrConnecting)
                    return true;

                return false;
            }
        }

        public virtual void SetLoading(bool isLoading)
        {
            Console.WriteLine(isLoading);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        protected override void OnCreate(Bundle bundle)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

                Window.SetStatusBarColor(new Color(ContextCompat.GetColor(ApplicationContext, Resource.Color.primary)));
            }

            base.OnCreate(bundle);

            SetContentView(LayoutResource);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Window.DecorView.Dispose();
            Window.Dispose();
            base.Dispose();
        }
    }
}


