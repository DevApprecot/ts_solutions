using System;
using System.Linq;
using Ts_Solutions.Network;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : UIViewController
	{
		public FirstViewController() : base("FirstViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

