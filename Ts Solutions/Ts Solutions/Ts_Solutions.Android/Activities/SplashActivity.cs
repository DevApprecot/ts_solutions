using Android.App;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using System.Threading.Tasks;

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

            Task.Run(async () =>
            {
                await Task.Delay(1000);
            }).ContinueWith((t)=> {
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            System.GC.Collect();
        }
    }
}