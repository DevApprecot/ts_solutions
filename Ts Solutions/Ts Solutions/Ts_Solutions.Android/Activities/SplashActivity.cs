using Android.App;
using Android.OS;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(MainLauncher = true, Theme = "@style/TsTheme")]
    public class SplashActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_splash;
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}