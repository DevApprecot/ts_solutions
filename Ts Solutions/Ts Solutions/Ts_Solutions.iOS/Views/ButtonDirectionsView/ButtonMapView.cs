using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace Ts_Solutions.iOS
{
    public partial class ButtonMapView : UIView
    {
        public ButtonMapView (IntPtr handle) : base (handle)
        {
        }

		public static ButtonMapView Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("ButtonDirectionsView", null, null);
			var v = Runtime.GetNSObject<ButtonMapView>(arr.ValueAt(0));
			v.ButtonDirections.TintColor = UIColor.FromRGB(239, 60, 57);
			return v;
		}
    }
}