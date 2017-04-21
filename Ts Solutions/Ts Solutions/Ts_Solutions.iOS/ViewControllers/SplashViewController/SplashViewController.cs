using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ts_Solutions.IViews;
using Ts_Solutions.Presenters;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class SplashViewController : BaseController, ISplashView
	{
		SplashPresenter _presenter;

		public SplashViewController() : base("SplashViewController")
		{
		}

		public override async void ViewWillAppear(bool animate)
		{
			base.ViewWillAppear(animate);
			NavigationController.NavigationBarHidden = true;
			Reachability.ResetInternetEvents();
			Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;
			CreatePresenter();
			TranslationExtension.SetLanguage("en");
			await _presenter.LoadUrlServices();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ToggleConnectionIndicator(IsOnline());
		}

		private void CreatePresenter()
		{
			if (_presenter == null)
				_presenter = new SplashPresenter(this);
		}

		public async void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			await OnConnected();
		}

		public void NavigateToMainScreen()
		{
			InvokeOnMainThread(() =>
			{
				SetLoading(false);
				NavigationController.PushViewController(new FirstViewController(), false);
			});
		}

		public override async Task OnConnected()
		{
			ToggleConnectionIndicator(IsOnline());
			if (IsOnline())
				await _presenter.LoadUrlServices();
		}
	}
}

