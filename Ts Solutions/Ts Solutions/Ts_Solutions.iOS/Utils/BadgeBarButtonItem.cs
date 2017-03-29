using System;
using System.Collections.Generic;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Ts_Solutions.iOS
{
    public sealed class BadgeBarButtonItem : UIBarButtonItem
    {
        private UILabel _badge;
        private string _badgeValue;
        private UIColor _badgeBGColor;
        private UIColor _badgeTextColor;
        private UIFont _badgeFont;
        private nfloat _badgePadding;
        private nfloat _badgeMinSize;
        private nfloat _badgeOriginX;
        private nfloat _badgeOriginY;
        private bool _shouldHideBadgeAtZero;
        private bool _shouldAnimateBadge;
		private float _cornerRadius;
		private UIColor _borderColor;
		private float _borderWidth;
		private float _alpha;

        public string BadgeValue
        {
            get
            {
                return _badgeValue;
            }
            set
            {
                _badgeValue = value;
                SetBadgeValue(value);
            }
        }

        public UIColor BadgeBGColor
        {
            get
            {
                return _badgeBGColor;
            }
            set
            {
                _badgeBGColor = value;

                if (_badge != null)
                {
                    RefreshBadge();
                }
            }
        }

        public UIColor BadgeTextColor
        {
            get
            {
                return _badgeTextColor;
            }
            set
            {
                _badgeTextColor = value;

                if (_badge != null)
                {
                    RefreshBadge();
                }
            }
        }

        public UIFont BadgeFont
        {
            get
            {
                return _badgeFont;
            }
            set
            {
                _badgeFont = value;

                if (_badge != null)
                {
                    RefreshBadge();
                }
            }
        }

        public nfloat BadgePadding
        {
            get
            {
                return _badgePadding;
            }
            set
            {
                _badgePadding = value;

                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

        public nfloat BadgeMinSize
        {
            get
            {
                return _badgeMinSize;
            }
            set
            {
                _badgeMinSize = value;

                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

        public nfloat BadgeOriginX
        {
            get
            {
                return _badgeOriginX;
            }
            set
            {
                _badgeOriginX = value;

                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

        public nfloat BadgeOriginY
        {
            get
            {
                return _badgeOriginY;
            }
            set
            {
                _badgeOriginY = value;

                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

        public bool ShouldHideBadgeAtZero
        {
            get
            {
                return _shouldHideBadgeAtZero;
            }
            set
            {
                _shouldHideBadgeAtZero = value;
                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

        public bool ShouldAnimateBadge
        {
            get
            {
                return _shouldAnimateBadge;
            }
            set
            {
                _shouldAnimateBadge = value;

                if (_badge != null)
                {
                    UpdateBadgeFrame();
                }
            }
        }

		public float CornerRadius
		{
			get
			{
				return _cornerRadius;
			}

			set
			{
				_cornerRadius = value;
				if (_badge != null)
				{
					UpdateBadgeFrame();
				}
			}
		}

		public UIColor BorderColor
		{
			get
			{
				return _borderColor;
			}

			set
			{
				_borderColor = value;
				if (_badge != null)
				{
					UpdateBadgeFrame();
				}
			}
		}

		public float BorderWidth
		{
			get
			{
				return _borderWidth;
			}

			set
			{
				_borderWidth = value;
				if (_badge != null)
				{
					UpdateBadgeFrame();
				}
			}
		}

		public float Alpha
		{
			get
			{
				return _alpha;
			}

			set
			{
				_alpha = value;
				if (_badge != null)
				{
					UpdateBadgeFrame();
				}
			}
		}

		public BadgeBarButtonItem(UIButton customButton)
        {
			BorderWidth = 0;
			BorderColor = UIColor.White;
			CornerRadius = 0;
			Alpha = 1;
            CustomView = customButton;
            if (CustomView != null)
            {
                Initializer();
            }
        }

        private static UILabel DuplicateLabel(UILabel labelToCopy)
        {
            var duplicateLabel = new UILabel(labelToCopy.Frame)
            {
                Text = labelToCopy.Text,
                Font = labelToCopy.Font
            };

            return duplicateLabel;
        }

        private void Initializer()
        {
			
			BadgeBGColor = UIColor.FromRGB(116, 154, 189);
            BadgeTextColor = UIColor.White;
			BadgeFont = UIFont.SystemFontOfSize(12, UIFontWeight.Regular);
            BadgePadding = 6;
            BadgeMinSize = 8;
            BadgeOriginX = 10;
            BadgeOriginY = -9;
            ShouldHideBadgeAtZero = true;
            ShouldAnimateBadge = true;
            CustomView.ClipsToBounds = false;

        }

        private void RefreshBadge()
        {
            _badge.TextColor = BadgeTextColor;
            _badge.BackgroundColor = BadgeBGColor;
            _badge.Font = BadgeFont;
			UpdateBadgeFrame();
        }

        private void UpdateBadgeFrame()
        {
            var frameLabel = DuplicateLabel(_badge);

            frameLabel.SizeToFit();

            var expectedLabelSize = frameLabel.Frame.Size;

            var minHeight = expectedLabelSize.Height;

            minHeight = (minHeight < BadgeMinSize) ? BadgeMinSize : expectedLabelSize.Height;
            var minWidth = expectedLabelSize.Width;
            var padding = BadgePadding;

            minWidth = (minWidth < minHeight) ? minHeight : expectedLabelSize.Width;
            _badge.Frame = new CGRect(BadgeOriginX, BadgeOriginY, minWidth + padding, minHeight + padding);
			//_badge.Frame.Inset(-2, -2);
			_badge.Layer.MasksToBounds = true;
			if (CornerRadius == 0)
			{
				_badge.Layer.CornerRadius = (minHeight + padding) / 2;
				CAShapeLayer border = new CAShapeLayer();
				border.Path = UIBezierPath.FromRoundedRect(_badge.Bounds, _badge.Layer.CornerRadius).CGPath;
				border.StrokeColor = UIColor.White.CGColor;
				border.FillColor = UIColor.Clear.CGColor;
				_badge.Layer.AddSublayer(border);
			}
			else
				_badge.Layer.CornerRadius = CornerRadius;
			_badge.Layer.BorderColor = BorderColor.CGColor;
			_badge.Layer.BorderWidth = BorderWidth;
			_badge.Alpha = _alpha;
        }

        private void UpdateBadgeValueAnimated(bool animated)
        {
            if (animated && ShouldAnimateBadge && _badge.Text != BadgeValue)
            {
                var animation = new CABasicAnimation
                {
                    KeyPath = @"transform.scale",
                    From = NSObject.FromObject(1.5),
                    To = NSObject.FromObject(1),
                    Duration = 0.2,
                    TimingFunction = new CAMediaTimingFunction(0.4f, 1.3f, 1f, 1f)
                };
                _badge.Layer.AddAnimation(animation, @"bounceAnimation");
            }

            _badge.Text = BadgeValue;

            var duration = animated ? 0.2 : 0;
            UIView.Animate(duration, UpdateBadgeFrame);
        }

        private void RemoveBadge()
        {
            if (_badge != null)
            {
                UIView.AnimateNotify(0.15f, 0.0F,
                UIViewAnimationOptions.CurveEaseIn,
                () =>
                {
                    _badge.Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                },
                completed =>
                {
                    _badge.RemoveFromSuperview();
                    _badge = null;
                }
                );
            }
        }

        private void SetBadgeValue(string badgeValue)
        {
            _badgeValue = badgeValue;

            if (string.IsNullOrEmpty(badgeValue) || (badgeValue == @"0" && ShouldHideBadgeAtZero))
            {
                RemoveBadge();
            }
            else if (_badge == null)
            {
                _badge = new UILabel(new CGRect(BadgeOriginX, BadgeOriginY, 20, 20))
                {
                    TextColor = BadgeTextColor,
                    BackgroundColor = BadgeBGColor,
                    Font = BadgeFont,
                    TextAlignment = UITextAlignment.Center,
                    Layer = { CornerRadius = 10, MasksToBounds = true }


                };

                CustomView.AddSubview(_badge);
                UpdateBadgeValueAnimated(false);
            }
            else
            {
                UpdateBadgeValueAnimated(true);
            }
        }

    }
}