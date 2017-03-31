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

		public static NoItemsView Create(UIView view)
		{
			var arr = NSBundle.MainBundle.LoadNib("NoItemsView", null, null);
			var v = Runtime.GetNSObject<NoItemsView>(arr.ValueAt(0));
			v.Frame = new CoreGraphics.CGRect(view.Frame.X, view.Frame.Y, view.Bounds.Width, view.Bounds.Height);
			return v;
		}
	}
}