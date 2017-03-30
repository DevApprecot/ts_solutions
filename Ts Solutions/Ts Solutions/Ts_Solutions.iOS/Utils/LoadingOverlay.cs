using System;
using Airbnb.Lottie;
using CoreGraphics;
using UIKit;

namespace Ts_Solutions.iOS
{
	public class LoadingOverlay : UIView
	{
		// control declarations
		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;

		public LoadingOverlay(CGRect frame, string text = "") : base(frame)
		{
			// configurable bits
			var animation = LOTAnimationView.AnimationNamed("Animations/preloader");
			animation.ContentMode = UIViewContentMode.ScaleAspectFit;
			animation.PlayWithCompletion((animationFinished) =>
			{
				// Do Something
			});
			animation.Frame = frame;

			BackgroundColor = UIColor.FromWhiteAlpha(0, 0.2f);

			Alpha = 0.75f;
			
			AutoresizingMask = UIViewAutoresizing.All;

			this.Add(animation);
			//nfloat labelHeight = 22;
			//nfloat labelWidth = Frame.Width - 20;

			//// derive the center x and y
			//nfloat centerX = Frame.Width / 2;
			//nfloat centerY = Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			//activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			//activitySpinner.Color = ConstantsiOS.GeneralRedColor;
			//activitySpinner.Frame = new CGRect(
			//	centerX - (activitySpinner.Frame.Width / 2),
			//	centerY - activitySpinner.Frame.Height - 20,
			//	activitySpinner.Frame.Width,
			//	activitySpinner.Frame.Height);
			//activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
			//AddSubview(activitySpinner);
			//activitySpinner.StartAnimating();

			//// create and configure the "Loading Data" label
			//loadingLabel = new UILabel(new CGRect(
			//	centerX - (labelWidth / 2),
			//	centerY + 20,
			//	labelWidth,
			//	labelHeight
			//	));
			//loadingLabel.BackgroundColor = UIColor.Clear;
			////loadingLabel.TextColor = ConstantsiOS.LabelDarkGrayColor;
			//if (!string.IsNullOrEmpty(text))
			//	loadingLabel.Text = text;
			//else
			//{
			//	loadingLabel.Text = "Loading"; //TranslationExtension.LanguageBundle.LocalizedString("Mobile_Loading", "");
			//}
			//loadingLabel.TextAlignment = UITextAlignment.Center;
			//loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
			//AddSubview(loadingLabel);
		}

		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide()
		{
			UIView.Animate(
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); Alpha = 0.75f; }
			);
		}
	}
}
