﻿using System;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using Ts_Solutions.Model;
using UIKit;
using System.Linq;
using System.Diagnostics;

namespace Ts_Solutions.iOS
{
	public class MapDelegate : MKMapViewDelegate
	{
		static string annotationId = "StoreAnnotation";
		List<ServicePoint> _servicePoints;
		BaseController _owner;

		public MapDelegate(List<ServicePoint> points, BaseController owner)//List<Store> stores, BaseController owner, StoresParentController g = null)
		{
			_servicePoints = points;
			_owner = owner;
		}

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;
			StoreAnnotationView myView = null;
			if (annotation is MKUserLocation)
				return null;
			//var tmpPoint = _servicePoints?.Where(x => (x.Id == ((StoreAnnotation)annotation).Point.Id)).ToList().FirstOrDefault();// .Coordinate.Latitude) && (x.Lon == annotation.Coordinate.Longitude)).ToList().Fir;
			if (annotation is StoreAnnotation)
			{
				annotation = annotation as StoreAnnotation;
				annotationView = mapView.DequeueReusableAnnotation(annotationId);
				if (annotationView == null)
					annotationView = new StoreAnnotationView();
				myView = StoreAnnotationView.Create();
				myView.Frame = new CGRect(0, 0, 30, 30);
				myView.UpdateView(((StoreAnnotation)annotation).Point);
				myView.Annotation = annotation;
				annotationView.Annotation = annotation;
				annotationView = myView;
				annotationView.ContentMode = UIViewContentMode.ScaleAspectFit;
				annotationView.CanShowCallout = true;
				var button = new UIButton();
				button.Bounds = new CGRect(0, 0, 22, 22);
				button.SetImage(UIImage.FromBundle("Directions").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
				button.TintColor = UIColor.FromRGB(239, 60, 57);
				annotationView.RightCalloutAccessoryView = button;// UIButton.FromType(UIButtonType.InfoDark);
			}
			return myView;
		}

		public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
		{
			if (control == view.RightCalloutAccessoryView)
			{
				var tmp = mapView.SelectedAnnotations;
				if (tmp.Length == 1)
					(_owner as FirstViewController).DirectionsClicked((tmp[0] as StoreAnnotation).Point);
			}
		}
	}
}
