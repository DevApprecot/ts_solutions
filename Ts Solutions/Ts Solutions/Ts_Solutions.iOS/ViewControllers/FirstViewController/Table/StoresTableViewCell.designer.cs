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
	[Register ("StoresTableViewCell")]
	partial class StoresTableViewCell
	{
		[Outlet]
		UIKit.UIImageView ImageStore { get; set; }

		[Outlet]
		UIKit.UILabel LabelAddress { get; set; }

		[Outlet]
		UIKit.UILabel LabelName { get; set; }

		[Outlet]
		UIKit.UILabel LabelTelephone { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageStore != null) {
				ImageStore.Dispose ();
				ImageStore = null;
			}

			if (LabelName != null) {
				LabelName.Dispose ();
				LabelName = null;
			}

			if (LabelAddress != null) {
				LabelAddress.Dispose ();
				LabelAddress = null;
			}

			if (LabelTelephone != null) {
				LabelTelephone.Dispose ();
				LabelTelephone = null;
			}
		}
	}
}
