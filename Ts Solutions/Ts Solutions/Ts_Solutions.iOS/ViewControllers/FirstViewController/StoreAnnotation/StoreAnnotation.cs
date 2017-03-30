using System;
using CoreLocation;
using MapKit;

namespace Ts_Solutions.iOS
{
	public class StoreAnnotation : MKAnnotation
	{
		string title;
		CLLocationCoordinate2D coord;

		public StoreAnnotation(string title, CLLocationCoordinate2D coord)//, Store st)
		{
			
		}


		public override string Title
		{
			get
			{
				return title;
			}
		}

		public override CLLocationCoordinate2D Coordinate
		{
			get
			{
				return coord;
			}
		}
	}
}
