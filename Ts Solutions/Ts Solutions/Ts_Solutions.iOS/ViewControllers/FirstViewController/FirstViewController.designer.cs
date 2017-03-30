// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Ts_Solutions.iOS
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        UIKit.UIButton ButtonCheck { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint ConstTopText { get; set; }


        [Outlet]
        MapKit.MKMapView MapPoints { get; set; }


        [Outlet]
        UIKit.UITableView TablePoints { get; set; }


        [Outlet]
        UIKit.UITextField TextCode { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonCheck != null) {
                ButtonCheck.Dispose ();
                ButtonCheck = null;
            }

            if (ConstTopText != null) {
                ConstTopText.Dispose ();
                ConstTopText = null;
            }

            if (MapPoints != null) {
                MapPoints.Dispose ();
                MapPoints = null;
            }

            if (TablePoints != null) {
                TablePoints.Dispose ();
                TablePoints = null;
            }

            if (TextCode != null) {
                TextCode.Dispose ();
                TextCode = null;
            }
        }
    }
}