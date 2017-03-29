using System;

using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : BaseController
	{
		public FirstViewController() : base("FirstViewController")
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			var l = NavigationController.ViewControllers.Length;
			Console.WriteLine("test");
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

