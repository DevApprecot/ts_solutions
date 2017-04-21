using Android.App;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using System.Threading.Tasks;
using Ts_Solutions.IViews;
using System;
using Ts_Solutions.Presenters;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(MainLauncher = true, Theme = "@style/TsTheme")]
    public class SplashActivity : BaseActivity, ISplashView
    {
        protected override int LayoutResource => Resource.Layout.activity_splash;

        private SplashPresenter _presenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MobileCenter.Start(GetString(Resource.String.mobile_center_api_key),
                   typeof(Analytics), typeof(Crashes));

            StrictMode.SetVmPolicy(new StrictMode.VmPolicy.Builder().DetectActivityLeaks().PenaltyLog().Build());

            CreatePresenter();

            Task.Run(async () =>
            {
                await _presenter.LoadUrlServices();
            });
        }

        public void NavigateToMainScreen()
        {
            RunOnUiThread(() =>
            {
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
                Finish();
            });
        }

        private void CreatePresenter()
        {
            if (_presenter == null) _presenter = new SplashPresenter(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            System.GC.Collect();
        }
    }
}