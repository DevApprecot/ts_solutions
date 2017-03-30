using System;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using Ts_Solutions.Model;
using UIKit;
using System.Linq;

namespace Ts_Solutions.iOS
{
	public class MapDelegate : MKMapViewDelegate
	{
		static string annotationId = "StoreAnnotation";
		List<ServicePoint> _servicePoints;

		public MapDelegate(List<ServicePoint> points)//List<Store> stores, BaseController owner, StoresParentController g = null)
		{
			_servicePoints = points;
		}

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;
			StoreAnnotationView myView = null;
			if (annotation is MKUserLocation)
				return null;
			var tmpPoint = _servicePoints?.Where(x => (x.Id == ((StoreAnnotation)annotation).Point.Id)).ToList().FirstOrDefault();// .Coordinate.Latitude) && (x.Lon == annotation.Coordinate.Longitude)).ToList().Fir;
			if (annotation is StoreAnnotation)
			{
				annotation = annotation as StoreAnnotation;
				annotationView = mapView.DequeueReusableAnnotation(annotationId);
				if (annotationView == null)
					annotationView = new StoreAnnotationView();
				myView = StoreAnnotationView.Create();
				myView.Frame = new CGRect(0, 0, 30, 30);
				myView.UpdateView(tmpPoint);
				myView.Annotation = annotation;
				annotationView.Annotation = annotation;
				annotationView = myView;
				annotationView.ContentMode = UIViewContentMode.ScaleAspectFit;
				annotationView.CanShowCallout = true;
			}
			return myView;
		}
	}
}
