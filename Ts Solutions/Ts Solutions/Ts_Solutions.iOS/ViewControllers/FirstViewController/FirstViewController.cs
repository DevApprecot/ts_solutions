﻿using System;
using System.Collections.Generic;
using CoreLocation;
using Ts_Solutions.Model;
using Ts_Solutions.Presenter;
using Ts_Solutions.IView;
using UIKit;
using Foundation;
using System.Threading.Tasks;
using System.Globalization;

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
			LableStatus.Font = UIFont.SystemFontOfSize(21);
			View.BackgroundColor = UIColor.FromRGB(237, 237, 237);
			ConstTopText.Constant = 8 + NavigationController.NavigationBar.Frame.Height + 20;
			ButtonCheck.BackgroundColor = UIColor.FromRGB(239, 60, 57);
			ButtonCheck.SetTitle("Check", UIControlState.Normal);
			ButtonCheck.SetTitleColor(UIColor.White, UIControlState.Normal);
			ButtonCheck.Layer.CornerRadius = 5;
			ButtonCheck.ClipsToBounds = true;
			TextCode.Placeholder = "Write your work order here";
			//TextCode.ClearsOnBeginEditing = true;
			ButtonClose.SetImage(UIImage.FromBundle("CloseButton"), UIControlState.Normal);
			var noItemsView = NoItemsView.Create(TablePoints);
			TablePoints.BackgroundView = noItemsView;
			TablePoints.BackgroundView.Alpha = 0;
			TablePoints.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			var leftIcon = new UIBarButtonItem[1]
			 {
				new UIBarButtonItem(UIImage.FromBundle("NavBar").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal)
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
				new UIBarButtonItem(UIImage.FromBundle("List").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
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
			TextCode.ReturnKeyType = UIReturnKeyType.Done;
			TextCode.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				ButtonCheck.SendActionForControlEvents(UIControlEvent.TouchUpInside);
				return true;
			};
		}

		public override async void ViewWillAppear(bool animate)
		{
			base.ViewWillAppear(animate);
			NavigationController.SetNavigationBarHidden(false, true);
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
			await OnConnected();
		}

		public void SetMarkers(List<ServicePoint> points)
		{
			SetNavBar("List");
			TablePoints.Alpha = 0;
			MapPoints.Alpha = 1;
			var mapDelegate = new MapDelegate(points, this);//stores, this, owner);
			MapPoints.Delegate = mapDelegate;
			MapPoints.RemoveAnnotations(MapPoints.Annotations);
			if (points != null)
			{
				var annotations = new List<StoreAnnotation>();
				foreach (var st in points)
				{
					annotations.Add(new StoreAnnotation($"{st.Street} {st.StreetNumber}", new CLLocationCoordinate2D(st.Lat, st.Lon), st));
					MapPoints.AddAnnotations(new StoreAnnotation($"{st.Street} {st.StreetNumber}", new CLLocationCoordinate2D(st.Lat, st.Lon), st));
				};
			}
		}

		public void ShowStatus(WorkStatus status)
		{
			LableStatus.Text = status.Text;
			if (ViewStatus.Alpha == 0)
				ViewStatus.SlideInFromBottom();
		}

		public void SetList(List<ServicePoint> points)
		{
			if (points == null || points.Count == 0)
			{
				TablePoints.BackgroundView.Alpha = 1;
			}
			else
			{
				TablePoints.BackgroundView.Alpha = 0;
			}
			SetNavBar("Map");
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

		async void ButtonCheck_TouchUpInside(object sender, EventArgs e)
		{
			TextCode.ResignFirstResponder();
			if (CheckFields())
				await _presenter.ButtonCheckTapped(TextCode.Text);
		}

		void ButtonClose_TouchUpInside(object sender, EventArgs e)
		{
			_presenter.ButtonCloseTapped();
		}

		public void CallClicked(string phone)
		{
			_presenter.Call(phone);
		}

		public void CallNumber(string phone)
		{
			this.Call(phone);
		}

		public void HideStatus()
		{
			TextCode.Text = "";
			TextCode.Placeholder = "";
			TextCode.Placeholder = "Write your work order here";
			if (ViewStatus.Alpha == 1)
				ViewStatus.SlideOutFromBottom();
		}

		bool CheckFields()
		{
			var attributes = new UIStringAttributes
			{
				ForegroundColor = UIColor.Red
			};

			var text = "Write your work order here";
			if (string.IsNullOrEmpty(TextCode.Text))
				TextCode.AttributedPlaceholder = new NSAttributedString(text, attributes);
			else
			{
				TextCode.Placeholder = "Write your work order here";
				return true;
			}
			return false;
		}

		public void DirectionsClicked(ServicePoint point)
		{
			_presenter.DirectionsClicked(point);
		}

		public void OpenDirections(ServicePoint point)
		{
			var uri = new Uri($"http://maps.apple.com/?daddr={point.Lat.ToString(CultureInfo.InvariantCulture)},{point.Lon.ToString(CultureInfo.InvariantCulture)}");

			var url = new NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
			if (UIApplication.SharedApplication.CanOpenUrl(url))
				UIApplication.SharedApplication.OpenUrl(url);
		}

		public override void ShowMessage(string message)
		{
			LableStatus.Text = TranslationExtension.LanguageBundle.LocalizedString(message, "");
			if (ViewStatus.Alpha == 0)
				ViewStatus.SlideInFromBottom();
		}

		public override async Task OnConnected()
		{
            ToggleConnectionIndicator(IsOnline());
			if (IsOnline())
				await _presenter.LoadServicePoints();
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


