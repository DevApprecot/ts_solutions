using System;
using CoreGraphics;
using UIKit;

namespace Ts_Solutions.iOS
{
	public static class scrollExtension
	{
		static nfloat _total_scroll = 0;

		public static void CenterView(this UIScrollView scrollView, UIView viewToCenter, CGRect keyboardFrame, UINavigationController navController, bool animated = false)
		{
			var scrollFrame = scrollView.Frame;

			var adjustedFrame = UIApplication.SharedApplication.KeyWindow.ConvertRectFromView(scrollFrame, scrollView.Superview);

			var intersect = CGRect.Intersect(adjustedFrame, keyboardFrame);

			var height = intersect.Height;
			if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0) && IsLandscape())
			{
				height = intersect.Width;
			}
			scrollView.CenterView(viewToCenter, navController, height, animated: animated);
		}

		public static void CenterView(this UIScrollView scrollView, UIView viewToCenter, UINavigationController navController, nfloat keyboardHeight = default(nfloat), bool adjustContentInsets = true, bool animated = false)
		{

			nfloat navBarAllowance;


			if (!navController.NavigationBarHidden)
				navBarAllowance = navController.NavigationBar.Bounds.Y + navController.NavigationBar.Bounds.Size.Height;
			else
				navBarAllowance = 0.0f;

			if (!UIApplication.SharedApplication.StatusBarHidden)
			{
				navBarAllowance += 20;
			}


			if (adjustContentInsets)
			{
				var contentInsets = new UIEdgeInsets(navBarAllowance, 0.0f, keyboardHeight, 0.0f);
				scrollView.ContentInset = contentInsets;
				scrollView.ScrollIndicatorInsets = contentInsets;
			}

			// Position of the active field relative isnside the scroll view


			CGRect relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);

			var spaceAboveKeyboard = scrollView.Frame.Height - keyboardHeight;

			// Move the active field to the center of the available space
			var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
			if (scrollView.ContentOffset.Y < offset)
			{
				scrollView.SetContentOffset(new CGPoint(0, offset), animated);
			}
		}

		public static void CenterNormalView(UIView parentView, UIView viewToCenter, UINavigationController navController, nfloat keyboardHeight = default(nfloat), bool animated = true)
		{



			nfloat navBarAllowance;

			if (!navController.NavigationBarHidden)
				navBarAllowance = navController.NavigationBar.Bounds.Y + navController.NavigationBar.Bounds.Size.Height;
			else
				navBarAllowance = 0.0f;
			
			if (!UIApplication.SharedApplication.StatusBarHidden)
			{
				navBarAllowance += 20;
			}



			// Position of the active field relative isnside the scroll view

			//CGRect relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, parentView);

			//var spaceAboveKeyboard = parentView.Frame.Height - keyboardHeight;

			// Move the active field to the center of the available space
			//var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
			var hides = keyboardHeight - (float)viewToCenter.Frame.Top - 20;

			//var offset = (float)(viewToCenter.Frame.Top - (spaceAboveKeyboard));


			if (hides <= 0)
			{
				var fr = parentView.Frame;
				fr.Y -= navBarAllowance - hides;
				_total_scroll = fr.Y;
				parentView.Frame = fr;
			}

			UIView.CommitAnimations();

		}


		public static void RestoreScrollPosition(this UIScrollView scrollView, CGRect keyboardFrame, UINavigationController navController)
		{
			nfloat navBarAllowance;

			if (!navController.NavigationBarHidden)
				navBarAllowance = navController.NavigationBar.Bounds.Y + navController.NavigationBar.Bounds.Size.Height;
			else
				navBarAllowance = 0.0f;

			if (!UIApplication.SharedApplication.StatusBarHidden)
			{
				navBarAllowance += 20;
			}

			UIEdgeInsets contentInsets = new UIEdgeInsets(0.0f, 0.0f, 0.0f, 0.0f);
			scrollView.ContentInset = contentInsets;
			scrollView.ScrollIndicatorInsets = contentInsets;
		}

		public static void RestoreNormalPosition(this UIView scrollView, CGRect keyboardFrame, UINavigationController navController)
		{
			nfloat navBarAllowance;

			if (!navController.NavigationBarHidden)
				navBarAllowance = navController.NavigationBar.Bounds.Y + navController.NavigationBar.Bounds.Size.Height;
			else
				navBarAllowance = 0.0f;

			if (!UIApplication.SharedApplication.StatusBarHidden)
			{
				navBarAllowance += 20;
			}



			// Move the active field to the center of the available space



			var fr = scrollView.Frame;
			fr.Y = 0;//navBarAllowance;
					 //fr.Y += _total_scroll;
			scrollView.Frame = fr;
		}


		public static bool IsLandscape()
		{
			var orientation = UIApplication.SharedApplication.StatusBarOrientation;
			bool landscape = orientation == UIInterfaceOrientation.LandscapeLeft
						|| orientation == UIInterfaceOrientation.LandscapeRight;
			return landscape;
		}
	}
}
