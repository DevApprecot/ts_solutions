using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using MapKit;

namespace Ts_Solutions.iOS
{
	public partial class StoreAnnotationView : MKAnnotationView
	{

		public StoreAnnotationView(IntPtr handle) : base(handle)
		{
		}

		public StoreAnnotationView()
		{
		}

		public static StoreAnnotationView Create()
		{

			var arr = NSBundle.MainBundle.LoadNib("StoreAnnotationView", null, null);
			var v = Runtime.GetNSObject<StoreAnnotationView>(arr.ValueAt(0));

			return v;
		}

		public void UpdateView()
		{
			BackgroundColor = UIColor.Clear;
			ImageStore.Image = UIImage.FromBundle("ic_navbar_icon");
			ImageStore.ContentMode = UIViewContentMode.ScaleAspectFit;
			ImageStore.Layer.CornerRadius = ImageStore.Frame.Size.Width / 2;
			ImageStore.Layer.MasksToBounds = true;
		}

		public override void AwakeFromNib()
		{
			///ImageMarker.Image = UIImage.FromBundle("Imag/offer2");	
		}
	}
}