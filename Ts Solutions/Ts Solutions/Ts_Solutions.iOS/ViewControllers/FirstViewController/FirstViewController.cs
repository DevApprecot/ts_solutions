using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreLocation;
using Ts_Solutions.Model;
using Ts_Solutions.Presenter;
using Ts_Solutions.IView;
using UIKit;

namespace Ts_Solutions.iOS
{
	public partial class FirstViewController : BaseController, IMainView
	{
		MainPresenter _presenter;
		UIBarButtonItem[] _rightIcons;

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
			_rightIcons = new UIBarButtonItem[1]
			{
				new UIBarButtonItem(UIImage.FromBundle("Icons/ic_list").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
						, UIBarButtonItemStyle.Plain
						, (sender, args) =>
						{
					_presenter?.ChangeViewTypeClicked();
					TextCode.ResignFirstResponder();
				})
				{
					TintColor=UIColor.FromRGB(239, 60, 57)
				}
			};
			NavigationItem.RightBarButtonItems = _rightIcons;
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
            AddHandlers();
			await _presenter.LoadServicePoints();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
            ToggleConnectionIndicator(IsOnline());
		}

		public override void ViewWillLayoutSubviews()
		{
			base.ViewWillLayoutSubviews();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			RemoveHandlers();
		}

		private void CreatePresenter()
		{
			if (_presenter == null)
				_presenter = new MainPresenter(this);
		}

		public async void Reachability_ReachabilityChanged(object sender, EventArgs e)
		{
			ToggleConnectionIndicator(IsOnline());
			if (IsOnline())
				await _presenter.LoadServicePoints();
		}

		public void SetMarkers(List<ServicePoint> points)
		{
			SetNavBar("Icons/ic_list");
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

		public void ShowStatus(string status)
		{
			LableStatus.Text = status;
			if (ViewStatus.Alpha == 0)
				ViewStatus.SlideInFromBottom();
		}

		public void SetList(List<ServicePoint> points)
		{
            SetNavBar("Icons/ic_map");
			TablePoints.Alpha = 1;
			MapPoints.Alpha = 0;
			var source = new StoresTableSource(points, this);
			TablePoints.Source = source;
			TablePoints.ReloadData();
		}

		void SetNavBar(string imageName)
		{
			_rightIcons[0].Image = UIImage.FromBundle(imageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
		}

		void ButtonCheck_TouchUpInside(object sender, EventArgs e)
		{
			TextCode.ResignFirstResponder();
			_presenter.ButtonCheckTapped(TextCode.Text);
		}

		void ButtonClose_TouchUpInside(object sender, EventArgs e)
		{
			TextCode.Text = "";
			if (ViewStatus.Alpha == 1)
				ViewStatus.SlideOutFromBottom();
		}

		public void CallClicked(string phone)
		{
			_presenter.Call(phone);
		}

		public void CallNumber(string phone)
		{
			this.Call(phone);
		}

		public void AddHandlers()
		{
			ButtonCheck.TouchUpInside += ButtonCheck_TouchUpInside;
			ButtonClose.TouchUpInside += ButtonClose_TouchUpInside;
		}

		public void RemoveHandlers()
		{
			ButtonCheck.TouchUpInside -= ButtonCheck_TouchUpInside;
			ButtonClose.TouchUpInside -= ButtonClose_TouchUpInside;
		}
	}
}


