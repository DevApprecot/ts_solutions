using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Ts_Solutions.Model;
using Ts_Solutions.Presenter;
using System.Threading.Tasks;
using Ts_Solutions.IView;

namespace Ts_Solutions.Droid.Activities
{
    [Activity(Theme = "@style/TsTheme")]
    public class MainActivity : BaseActivity, IMainView
    {
        MainPresenter _presenter;

        protected override int LayoutResource => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreatePresenter();

            Task.Run(async()=> await _presenter.LoadServicePoints());
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
            Console.WriteLine("Points Fetched: " + points.Count);
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

     
    }
}