// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Ts_Solutions.iOS
{
    [Register ("ButtonMapView")]
    partial class ButtonMapView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonDirections { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonDirections != null) {
                ButtonDirections.Dispose ();
                ButtonDirections = null;
            }
        }
    }
}