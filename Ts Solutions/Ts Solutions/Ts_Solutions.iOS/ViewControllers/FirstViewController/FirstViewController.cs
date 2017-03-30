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
			View.BackgroundColor = UIColor.FromRGB(237, 237, 237);
			ConstTopText.Constant = 8 + NavigationController.NavigationBar.Frame.Height + 20;
			ButtonCheck.BackgroundColor = UIColor.FromRGB(239, 60, 57);
			ButtonCheck.SetTitle("Check", UIControlState.Normal);
			ButtonCheck.SetTitleColor(UIColor.White, UIControlState.Normal);
			ButtonCheck.Layer.CornerRadius = 5;
			ButtonCheck.ClipsToBounds = true;
			TextCode.Placeholder = "Write your work order here";
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
			var rightIcons = new UIBarButtonItem[1]
			{
				new UIBarButtonItem(UIImage.FromBundle("Icons/ic_list").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
						, UIBarButtonItemStyle.Plain
						, (sender, args) =>
						{

				})
				{
					TintColor=UIColor.FromRGB(239, 60, 57)
				}
			};
			NavigationItem.RightBarButtonItems = rightIcons;
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

