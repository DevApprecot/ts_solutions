using System;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : BaseController, IViewController
	{
		ReconnectingView _reconnect;

		public FirstViewController() : base("FirstViewController")
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void ViewWillAppear(bool animate)
		{
			base.ViewWillAppear(animate);
			Reachability.ResetInternetEvents();
			Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			ToggleConnectionIndicator(IsOnline());
		}

		public void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			ToggleConnectionIndicator(IsOnline());
		}


		public void ToggleConnectionIndicator(bool internetState)
		{
			if (!internetState)
			{
				if (_reconnect == null)
				{
					_reconnect = ReconnectingView.Create(NavigationController.NavigationBarHidden);
					_reconnect.Alpha = 0;
				}
				if (_reconnect.Alpha == 1) return;

				View.Add(_reconnect);
				_reconnect.FadeIn();
			}
			else
			{
				_reconnect?.Hide();
				//_reconnect?.RemoveFromSuperview();
				//_reconnect = null;
			}
		}

	}
}


