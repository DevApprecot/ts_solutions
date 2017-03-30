// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Ts_Solutions.iOS
{
	[Register ("FirstViewController")]
	partial class FirstViewController
	{
		[Outlet]
		UIKit.UIButton ButtonCheck { get; set; }

		[Outlet]
		UIKit.UIButton ButtonClose { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ConstTopText { get; set; }

		[Outlet]
		UIKit.UILabel LableStatus { get; set; }

		[Outlet]
		MapKit.MKMapView MapPoints { get; set; }

		[Outlet]
		UIKit.UITableView TablePoints { get; set; }

		[Outlet]
		UIKit.UITextField TextCode { get; set; }

		[Outlet]
		UIKit.UIView ViewStatus { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonCheck != null) {
				ButtonCheck.Dispose ();
				ButtonCheck = null;
			}

			if (ConstTopText != null) {
				ConstTopText.Dispose ();
				ConstTopText = null;
			}

			if (LableStatus != null) {
				LableStatus.Dispose ();
				LableStatus = null;
			}

			if (MapPoints != null) {
				MapPoints.Dispose ();
				MapPoints = null;
			}

			if (TablePoints != null) {
				TablePoints.Dispose ();
				TablePoints = null;
			}

			if (TextCode != null) {
				TextCode.Dispose ();
				TextCode = null;
			}

			if (ViewStatus != null) {
				ViewStatus.Dispose ();
				ViewStatus = null;
			}

			if (ButtonClose != null) {
				ButtonClose.Dispose ();
				ButtonClose = null;
			}
		}
	}
}
