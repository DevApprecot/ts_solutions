using System;
using System.Diagnostics;

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

		public void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			Debug.WriteLine("net changed");
		}
	}
}

