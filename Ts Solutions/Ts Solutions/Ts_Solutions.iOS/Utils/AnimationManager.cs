using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace Ts_Solutions.iOS
{
		public static class AnimationManager
		{
			public static void FadeIn(this UIView view)
			{
				Fade(view, true);
			}

			public static void Disable(this UIView view)
			{
				FadeHalf(view, false);
			}

			public static void Enable(this UIView view)
			{
				FadeHalf(view, true);
			}

			public static void FadeOut(this UIView view)
			{
				Fade(view, false);
			}

			public static void RotateIn(this UIView view)
			{
				Rotate(view, true);
			}

			public static void RotateOut(this UIView view)
			{
				Rotate(view, false);
			}

			public static void FlipInVerticaly(this UIView view)
			{
				FlipVerticaly(view, true);
			}

			public static void FlipOutVerticaly(this UIView view)
			{
				FlipVerticaly(view, false);
			}

			public static void FlipInHorizontaly(this UIView view)
			{
				FlipHorizontaly(view, true);
			}

			public static void FlipOutHorizontaly(this UIView view)
			{
				FlipHorizontaly(view, false);
			}

			public static void SlideInFromTop(this UIView view)
			{
				SlideVerticaly(view, true, true);
			}

			public static void SlideInFromBottom(this UIView view)
			{
				SlideVerticaly(view, true, false);
			}

			public static void SlideInFromLeft(this UIView view)
			{
				SlideHorizontaly(view, true, true);
			}

			public static void SlideInFromRight(this UIView view)
			{
				SlideHorizontaly(view, true, false);
			}

			public static void SlideOutFromTop(this UIView view)
			{
				SlideVerticaly(view, false, true);
			}

			public static void SlideOutFromBottom(this UIView view)
			{
				SlideVerticaly(view, false, false);
			}

			public static void SlideOutFromLeft(this UIView view)
			{
				SlideHorizontaly(view, false, true);
			}

			public static void SlideOutFromRight(this UIView view)
			{
				SlideHorizontaly(view, false, false);
			}

			public static void ZoomIn(this UIView view)
			{
				Zoom(view, true);
			}

			public static void ZoomOut(this UIView view)
			{
				Zoom(view, false);
			}

			public static void ScaleIn(this UIView view)
			{
				Scale(view, true);
			}

			public static void ScaleOut(this UIView view)
			{
				Scale(view, false);
			}

			public static void PointsEffect(this UIView view, double duration)
			{
				PointsSlide(view, false, true, duration: duration);
			}

			private static void PointsSlide(UIView view, bool isIn, bool fromTop, double duration = 0.3, Action onFinished = null)
			{
			var minAlpha = (nfloat)0.0f;
			var maxAlpha = (nfloat)1.0f;
			var minTransform = CGAffineTransform.MakeTranslation(0, (fromTop ? -1 : 1) * view.Bounds.Height);
			var maxTransform = CGAffineTransform.MakeIdentity();

			view.Alpha = isIn ? minAlpha : maxAlpha;
			view.Transform = isIn ? minTransform : maxTransform;
			UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
				() =>
				{
					view.Alpha = isIn ? maxAlpha : minAlpha;
					view.Transform = isIn ? maxTransform : minTransform;
				},
				onFinished
			);
			}

			private static void SlideVerticaly(UIView view, bool isIn, bool fromTop, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;
				var minTransform = CGAffineTransform.MakeTranslation(0, (fromTop ? -1 : 1) * view.Bounds.Height);
				var maxTransform = CGAffineTransform.MakeIdentity();

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						view.Transform = isIn ? maxTransform : minTransform;
					},
					onFinished
				);
			}

			private static void FlipHorizontaly(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var m34 = (nfloat)(-1 * 0.001);

				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;

				view.Alpha = (nfloat)1.0;

				var minTransform = CATransform3D.Identity;
				minTransform.m34 = m34;
				minTransform = minTransform.Rotate((nfloat)((isIn ? 1 : -1) * Math.PI * 0.5), (nfloat)0.0f, (nfloat)1.0f, (nfloat)0.0f);
				var maxTransform = CATransform3D.Identity;
				maxTransform.m34 = m34;

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Layer.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Layer.AnchorPoint = new CGPoint((nfloat)0.5, (nfloat)0.5f);
						view.Layer.Transform = isIn ? maxTransform : minTransform;
						view.Alpha = isIn ? maxAlpha : minAlpha;
					},
					onFinished
				);
			}

			private static void Zoom(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;
				var minTransform = CGAffineTransform.MakeScale((nfloat)2.0, (nfloat)2.0);
				var maxTransform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						view.Transform = isIn ? maxTransform : minTransform;
					},
					onFinished
				);
			}

			private static void SlideHorizontaly(UIView view, bool isIn, bool fromLeft, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;
				var minTransform = CGAffineTransform.MakeTranslation((fromLeft ? -1 : 1) * view.Bounds.Width, 0);
				var maxTransform = CGAffineTransform.MakeIdentity();

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						view.Transform = isIn ? maxTransform : minTransform;
					},
					onFinished
				);
			}

			private static void Scale(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;
				var minTransform = CGAffineTransform.MakeScale((nfloat)0.1, (nfloat)0.1);
				var maxTransform = CGAffineTransform.MakeScale((nfloat)1, (nfloat)1);

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						view.Transform = isIn ? maxTransform : minTransform;
					},
					onFinished
				);
			}

			private static void Rotate(UIView view, bool isIn, bool fromLeft = true, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;
				var minTransform = CGAffineTransform.MakeRotation((nfloat)((fromLeft ? -1 : 1) * 720));
				var maxTransform = CGAffineTransform.MakeRotation((nfloat)0.0);

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						view.Transform = isIn ? maxTransform : minTransform;
					},
					onFinished
				);
			}

			private static void FlipVerticaly(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var m34 = (nfloat)(-1 * 0.001);

				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;

				var minTransform = CATransform3D.Identity;
				minTransform.m34 = m34;
				minTransform = minTransform.Rotate((nfloat)((isIn ? 1 : -1) * Math.PI * 0.5), (nfloat)1.0f, (nfloat)0.0f, (nfloat)0.0f);
				var maxTransform = CATransform3D.Identity;
				maxTransform.m34 = m34;

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Layer.Transform = isIn ? minTransform : maxTransform;
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Layer.AnchorPoint = new CGPoint((nfloat)0.5, (nfloat)0.5f);
						view.Layer.Transform = isIn ? maxTransform : minTransform;
						view.Alpha = isIn ? maxAlpha : minAlpha;
					},
					onFinished
				);
			}

			private static void Fade(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.0f;
				var maxAlpha = (nfloat)1.0f;

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = CGAffineTransform.MakeIdentity();
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
					},
					onFinished
				);
			}

			private static void FadeHalf(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
			{
				var minAlpha = (nfloat)0.5f;
				var maxAlpha = (nfloat)1.0f;

				view.Alpha = isIn ? minAlpha : maxAlpha;
				view.Transform = CGAffineTransform.MakeIdentity();
				UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
					() =>
					{
						view.Alpha = isIn ? maxAlpha : minAlpha;
						var tmp = view as UIButton;
						if (tmp != null)
						{
							tmp.Enabled = isIn;
						}


					},
					onFinished
				);
			}
		}
	}

