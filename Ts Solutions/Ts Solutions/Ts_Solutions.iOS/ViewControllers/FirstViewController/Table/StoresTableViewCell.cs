using System;

using Foundation;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class StoresTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("StoresTableViewCell");
		public static readonly UINib Nib;

		static StoresTableViewCell()
		{
			Nib = UINib.FromName("StoresTableViewCell", NSBundle.MainBundle);
		}

		protected StoresTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
