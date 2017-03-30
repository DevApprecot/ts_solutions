using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace Ts_Solutions.iOS
{
    public partial class ReconnectingView : UIView
    {
        public ReconnectingView (IntPtr handle) : base (handle)
        {
        }

		public ReconnectingView()
		{

		}

		public static ReconnectingView Create(bool NavBarHidden)
		{
			var arr = NSBundle.MainBundle.LoadNib("ReconnectingView", null, null);
			var v = Runtime.GetNSObject<ReconnectingView>(arr.ValueAt(0));
			v.LabelInternet.Text = "Reconnecting...";//TranslationExtension.LanguageBundle.LocalizedString("reconnecting", "");
			//v.LabelInternet.Font = UIFont.SystemFontOfSize(ConstantsiOS.FontTextSize);
			v.LabelInternet.TextColor = UIColor.White;
			if(NavBarHidden)
				v.Frame = new CoreGraphics.CGRect(0, 20, UIScreen.MainScreen.Bounds.Width, 30);
			else
				v.Frame = new CoreGraphics.CGRect(0, 64, UIScreen.MainScreen.Bounds.Width, 30);
			v.BackgroundColor = UIColor.DarkGray;

			return v;
		}

		public void Hide()
		{
			UIView.Animate(
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview();}
			);
		}
    }
}