using System;
using UIKit;

namespace Ts_Solutions.iOS
{
	public class IMPORTANT_COPY_PASTE : UIViewController
	{
		public IMPORTANT_COPY_PASTE()
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
		}
	}
}
