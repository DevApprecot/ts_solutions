using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;

namespace Ts_Solutions.Droid
{
    public abstract class BaseActivity : AppCompatActivity
    {
        protected abstract int LayoutResource { get; }

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
	}
}


