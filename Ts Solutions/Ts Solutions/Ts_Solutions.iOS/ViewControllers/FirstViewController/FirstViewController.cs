using System;
using System.Diagnostics;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : BaseController, IViewController
	{
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
			ConstTopText.Constant = 16 + NavigationController.NavigationBar.Frame.Height + 20;
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
		}

		public void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			Debug.WriteLine("net changed");
		}
	}
}

