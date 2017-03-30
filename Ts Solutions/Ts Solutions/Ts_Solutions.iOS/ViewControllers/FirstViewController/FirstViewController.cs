using System;
using UIKit;

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
			ConstTopText.Constant = 16 + NavigationController.NavigationBar.Frame.Height + 20;
			ButtonCheck.BackgroundColor = UIColor.FromRGB(239, 60, 57);
			ButtonCheck.SetTitleColor(UIColor.White, UIControlState.Normal);
			var leftIcon = new UIBarButtonItem[1]
			{
				new UIBarButtonItem(UIImage.FromBundle("Icons/ic_navbar_icon").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal)
						, UIBarButtonItemStyle.Plain
						, (sender, args) =>
						{

				})
				{
					Enabled=false
				}
			};
			NavigationItem.LeftBarButtonItems = leftIcon;
			DismissKeyboardOnBackgroundTap();
			TextCode.ShouldReturn += (textField) => textField.ResignFirstResponder();
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
				_reconnect?.Hide();
			
		}

	}
}


