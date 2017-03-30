using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace Ts_Solutions.iOS
{
	public partial class NoItemsView : UIView
	{
		public NoItemsView(IntPtr handle) : base(handle)
		{
		}

		public NoItemsView()
		{
		}

		public static NoItemsView Create(BaseController owner)
		{
			var arr = NSBundle.MainBundle.LoadNib("NoItemsView", null, null);
			var v = Runtime.GetNSObject<NoItemsView>(arr.ValueAt(0));
			v.Frame = new CoreGraphics.CGRect(0, 0, owner.View.Bounds.Width, owner.View.Bounds.Height);
			return v;
		}
	}
}