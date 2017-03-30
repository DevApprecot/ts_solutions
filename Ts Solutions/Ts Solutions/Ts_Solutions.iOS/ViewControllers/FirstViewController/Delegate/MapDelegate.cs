using System;
using CoreGraphics;
using MapKit;
using UIKit;

namespace Ts_Solutions.iOS
{
	public class MapDelegate : MKMapViewDelegate
	{
		static string annotationId = "StoreAnnotation";

		public MapDelegate()//List<Store> stores, BaseController owner, StoresParentController g = null)
		{
		}

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;
			StoreAnnotationView myView = null;
			if (annotation is MKUserLocation)
				return null;

			if (annotation is StoreAnnotation)
			{
				annotation = annotation as StoreAnnotation;
				annotationView = mapView.DequeueReusableAnnotation(annotationId);
				if (annotationView == null)
					annotationView = new StoreAnnotationView();
				myView = StoreAnnotationView.Create();
				myView.Frame = new CGRect(0, 0, 30, 30);
				myView.UpdateView();
				myView.Annotation = annotation;
				annotationView.Annotation = annotation;
				annotationView = myView;
				annotationView.ContentMode = UIViewContentMode.ScaleAspectFit;
			}
			return myView;
		}
	}
}
