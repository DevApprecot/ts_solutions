using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(MainLauncher = true, Theme = "@style/TsTheme")]
    public class SplashActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_splash;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MobileCenter.Start(GetString(Resource.String.mobile_center_api_key),
                   typeof(Analytics), typeof(Crashes));

            StrictMode.SetVmPolicy(new StrictMode.VmPolicy.Builder().DetectActivityLeaks().PenaltyLog().Build());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            System.GC.Collect();
        }
    }
}