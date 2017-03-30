using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreLocation;
using Ts_Solutions.Model;
using Ts_Solutions.Presenter;
using Ts_Solutions.View;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : BaseController, IViewController, IMainView
	{
		ReconnectingView _reconnect;
		MainPresenter _presenter;

		public FirstViewController() : base("FirstViewController")
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.FromRGB(237, 237, 237);
			ConstTopText.Constant = 8 + NavigationController.NavigationBar.Frame.Height + 20;
			ButtonCheck.BackgroundColor = UIColor.FromRGB(239, 60, 57);
			ButtonCheck.SetTitle("Check", UIControlState.Normal);
			ButtonCheck.SetTitleColor(UIColor.White, UIControlState.Normal);
			ButtonCheck.Layer.CornerRadius = 5;
			ButtonCheck.ClipsToBounds = true;
			TextCode.Placeholder = "Write your work order here";
			var leftIcon = new UIBarButtonItem[1]
			{
				new UIBarButtonItem(UIImage.FromBundle("Icons/ic_navbar_icon").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal)
						, UIBarButtonItemStyle.Plain
						, (sender, args) =>
						{

				})
				{
					Enabled=false
				}
			};
			var rightIcons = new UIBarButtonItem[1]
			{
				new UIBarButtonItem(UIImage.FromBundle("Icons/ic_list").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
						, UIBarButtonItemStyle.Plain
						, (sender, args) =>
						{

				})
				{
					TintColor=UIColor.FromRGB(239, 60, 57)
				}
			};
			NavigationItem.RightBarButtonItems = rightIcons;
			NavigationItem.LeftBarButtonItems = leftIcon;
			DismissKeyboardOnBackgroundTap();
			TextCode.ShouldReturn += (textField) => textField.ResignFirstResponder();
		}

		public override async void ViewWillAppear(bool animate)
		{
			base.ViewWillAppear(animate);
			Reachability.ResetInternetEvents();
			Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;
            CreatePresenter();
			await _presenter.GetServicePoints();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			ToggleConnectionIndicator(IsOnline());
		}

		private void CreatePresenter()
		{
			if (_presenter == null)
				_presenter = new MainPresenter(this);
		}

		public void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			ToggleConnectionIndicator(IsOnline());
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

		public void SetMarkers(List<ServicePoint> points)
		{
			//SetNavBar("Icons/ic_list", searchedKeywords);
			TablePoints.Alpha = 0;
			MapPoints.Alpha = 1;
			var mapDelegate = new MapDelegate(points);//stores, this, owner);
			MapPoints.Delegate = mapDelegate;
			MapPoints.RemoveAnnotations(MapPoints.Annotations);
			if (points != null)
			{
				var annotations = new List<StoreAnnotation>();
				foreach (var st in points)
				{
					annotations.Add(new StoreAnnotation(st.Name, new CLLocationCoordinate2D(st.Lat, st.Lon), st));
					MapPoints.AddAnnotations(new StoreAnnotation(st.Name, new CLLocationCoordinate2D(st.Lat, st.Lon), st));
				};
			}
		}

		public void ShowStatus()
		{
			Debug.WriteLine("show status ");
		}

		public void SwitchView()
		{
			Debug.WriteLine("switching ");
		}

		public void ShowNoNet()
		{
			throw new NotImplementedException();
		}

		public void SetLoading(bool isLoading)
		{
			Debug.WriteLine("loading " + isLoading);
		}

		public void ShowMessage(string message)
		{
			Debug.WriteLine("message " + message);
		}
	}
}


