using System;
using Foundation;
using Ts_Solutions.Model;
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

		public void Update(ServicePoint point, BaseController owner)
		{
			ImageStore.Image = UIImage.FromBundle("NavBar");
			ImageStore.Layer.CornerRadius = ImageStore.Frame.Size.Width / 2;
			ImageStore.Layer.BorderColor = UIColor.FromRGB(237, 237, 237).CGColor;
			ImageStore.Layer.BorderWidth = 1f;
			ImageStore.ClipsToBounds = true;
			LabelName.Text = point.Name;
			LabelAddress.Text = point.Address;
			LabelTelephone.Text = point.Phone;
			LabelTelephone.TextColor = UIColor.FromRGB(0, 122, 255);
			LabelName.Font = UIFont.BoldSystemFontOfSize(18);
			LabelAddress.TextColor = UIColor.FromRGB(100, 100, 100);
			LabelTelephone.UserInteractionEnabled = true;
			LabelTelephone.AddGestureRecognizer(new UITapGestureRecognizer(() =>
			{
				owner.Call(LabelTelephone.Text);
			}));

		}
	}
}
