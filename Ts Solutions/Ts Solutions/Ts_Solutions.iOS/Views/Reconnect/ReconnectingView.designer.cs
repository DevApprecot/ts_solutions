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
    [Register ("ReconnectingView")]
    partial class ReconnectingView
    {
        [Outlet]
        UIKit.UIActivityIndicatorView ActivitySpinner { get; set; }


        [Outlet]
        UIKit.UILabel LabelInternet { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivitySpinner != null) {
                ActivitySpinner.Dispose ();
                ActivitySpinner = null;
            }

            if (LabelInternet != null) {
                LabelInternet.Dispose ();
                LabelInternet = null;
            }
        }
    }
}