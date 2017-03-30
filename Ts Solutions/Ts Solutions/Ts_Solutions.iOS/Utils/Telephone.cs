using System;
using System.Diagnostics;
using CoreTelephony;
using Foundation;
using UIKit;

namespace Ts_Solutions.iOS
{
    public static class Telephone
    {
        public static void Call(this BaseController view, string tel)
        {
			var url = new NSUrl("tel:" + tel.Replace(" ",""));
            if (UIApplication.SharedApplication.CanOpenUrl(url))
            {
				//A dialer is installed, now let's check if we can actually make a call.
				try
				{
					var networkInfo = new CTTelephonyNetworkInfo();
					var carrier = networkInfo.SubscriberCellularProvider;
					var networkCode = carrier.MobileNetworkCode;
					if (string.IsNullOrEmpty(networkCode) || networkCode == "65535")
					{
						var av = UIAlertController.Create("Not supported",
										 "Dialing is not supported on this device",
														  UIAlertControllerStyle.Alert);
						av.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
						view.PresentViewController(av, true, null);
						Debug.WriteLine("Problem!");
						//return false;
					}
					else
						UIApplication.SharedApplication.OpenUrl(url);
				}
				catch (NullReferenceException)
				{
					var av = UIAlertController.Create("Not supported",
										 "Dialing is not supported on this device",
														  UIAlertControllerStyle.Alert);
					av.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
					view.PresentViewController(av, true, null);
					Debug.WriteLine("Problem!");
				}
            }
        }
    }
}
