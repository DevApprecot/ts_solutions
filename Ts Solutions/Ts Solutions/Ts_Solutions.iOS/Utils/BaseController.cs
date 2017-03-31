using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Ts_Solutions.IView;
using UIKit;

namespace Ts_Solutions.iOS
{

	public abstract class BaseController : UIViewController, IBaseView
	{

		protected UIView ViewToCenterOnKeyboardShown;
		protected UIScrollView ScrollToCenterOnKeyboardShown;
		ReconnectingView _reconnect;
		LoadingOverlay _loadingOverlay;
		NSObject _keyboardShowObserver;
		NSObject _keyboardHideObserver;

		/// <summary>
		/// Required constructor for Storyboard to work
		/// </summary>
		/// <param name='handle'>
		/// Handle to Obj-C instance of object
		/// </param>
		BaseController(IntPtr handle) : base(handle)
		{
			//Only do this if required
			//if (HandlesKeyboardNotifications())
			//{
			//	NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			//	NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
			//}
		}

		public BaseController(string controller) : base(controller, null)
		{
		}


		public override void ViewDidLoad()
		{
		}


		public override void ViewWillAppear(bool animate)
		{
			base.ViewWillAppear(animate);
			RegisterForKeyboardNotifications();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			int count = NavigationController.ViewControllers.Length;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			UnregisterForKeyboardNotifications();
		}

		public virtual bool HandlesKeyboardNotifications()
		{
			return true;
		}

		protected virtual void RegisterForKeyboardNotifications()
		{
			if (_keyboardShowObserver == null)
				_keyboardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
			if (_keyboardHideObserver == null)
				_keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
		}

		protected virtual void UnregisterForKeyboardNotifications()
		{
			if (_keyboardShowObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
				_keyboardShowObserver.Dispose();
				_keyboardShowObserver = null;
			}

			if (_keyboardHideObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
				_keyboardHideObserver.Dispose();
				_keyboardHideObserver = null;
			}
		}

		protected virtual UIView KeyboardGetActiveView()
		{
			return View.FindFirstResponder();
		}

		private void OnKeyboardNotification(NSNotification notification)
		{
			if (!IsViewLoaded) return;

			//Check if the keyboard is becoming visible
			var visible = notification.Name == UIKeyboard.WillShowNotification;

			//Start an animation, using values from the keyboard
			UIView.BeginAnimations("AnimateForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState(true);
			UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
			UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

			//Pass the notification, calculating keyboard height, etc.
			var keyboardFrame = visible
				? UIKeyboard.FrameEndFromNotification(notification)
				: UIKeyboard.FrameBeginFromNotification(notification);

			OnKeyboardChanged(visible, keyboardFrame);

			//Commit the animation
			UIView.CommitAnimations();
		}

		protected virtual void OnKeyboardChanged(bool visible, CGRect keyboardFrame)
		{

			var activeView = ViewToCenterOnKeyboardShown ?? KeyboardGetActiveView();
			if (activeView == null)
				return;

			var scrollView = ScrollToCenterOnKeyboardShown ??
				activeView.FindTopSuperviewOfType(View, typeof(UIScrollView)) as UIScrollView;
			//if (scrollView == null)
			//    return;

			if (scrollView != null)
			{
				if (!visible)
					scrollView.RestoreScrollPosition(keyboardFrame, NavigationController);
				else
					scrollView.CenterView(activeView, NavigationController, keyboardFrame.Size.Height);
			}
			else
			{
				var noScrollView = activeView.FindTopSuperviewOfType(View, typeof(UIView)) as UIView;
				if (!visible)
					noScrollView.RestoreNormalPosition(keyboardFrame, NavigationController);
				else
					scrollExtension.CenterNormalView(noScrollView, activeView, NavigationController, keyboardFrame.Size.Height);

			}
		}

		protected virtual void DismissKeyboardOnBackgroundTap()
		{
			// Add gesture recognizer to hide keyboard
			var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
			tap.AddTarget(() => View.EndEditing(true));
			tap.ShouldReceiveTouch = (recognizer, touch) =>
				!(touch.View is UIControl || touch.View.FindSuperviewOfType(View, typeof(UITableViewCell)) != null);
			View.AddGestureRecognizer(tap);
		}

		public bool IsOnline()
		{
			return Reachability.IsHostReachable();
		}

		void InitKeyboardHandling()
		{
			//Only do this if required
			if (HandlesKeyboardNotifications())
			{
				RegisterForKeyboardNotifications();
			}
		}

		/// <summary>
		/// This is how orientation is setup on iOS 6
		/// </summary>
		public override bool ShouldAutorotate()
		{
			return true;
		}

		/// <summary>
		/// This is how orientation is setup on iOS 6
		/// </summary>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}



		public void ToggleConnectionIndicator(bool internetState)
		{
			if (!internetState)
			{
				if (_reconnect == null)
				{
					_reconnect = ReconnectingView.Create(NavigationController.NavigationBarHidden);
					_reconnect.Alpha = 0;
				}
				if (_reconnect.Alpha == 1) return;

				View.Add(_reconnect);
				_reconnect.FadeIn();
			}
			else
				_reconnect?.Hide();
		}

		public void SetLoading(bool isLoading)
		{
			InvokeOnMainThread(() =>
			{
				if (isLoading)
				{
					var bounds = UIScreen.MainScreen.Bounds;
					if (_loadingOverlay == null)
						_loadingOverlay = new LoadingOverlay(bounds);
					View.Add(_loadingOverlay);
				}
				else
					_loadingOverlay?.Hide();
				
			});
		}

		public void ShowMessage(string message)
		{
			Debug.WriteLine(message);
		}

		//old one
		public void ShowToast(string message, bool success = false, bool withButton = false, BaseController owner = null, int delay = 2000)
		{

		}

		public Task OnConnected()
		{
			throw new NotImplementedException();
		}
	}
}
