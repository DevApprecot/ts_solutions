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
    [Register ("StoreAnnotationView")]
    partial class StoreAnnotationView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageStore { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImageStore != null) {
                ImageStore.Dispose ();
                ImageStore = null;
            }
        }
    }
}