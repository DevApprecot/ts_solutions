using System;
using CoreLocation;
using MapKit;
using Ts_Solutions.Model;

namespace Ts_Solutions.iOS
{
	public class StoreAnnotation : MKAnnotation
	{
		string title;
		CLLocationCoordinate2D coord;
		public ServicePoint Point { get; set; }

		public StoreAnnotation(string title, CLLocationCoordinate2D coord, ServicePoint st)
		{
            this.title = title;
			this.coord = coord;
			Point = st;
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
