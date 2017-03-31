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
    [Register ("StoresTableViewCell")]
    partial class StoresTableViewCell
    {
        [Outlet]
        UIKit.UIButton ButtonDirections { get; set; }


        [Outlet]
        UIKit.UIImageView ImageStore { get; set; }


        [Outlet]
        UIKit.UILabel LabelAddress { get; set; }


        [Outlet]
        UIKit.UILabel LabelName { get; set; }


        [Outlet]
        UIKit.UILabel LabelTelephone { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonDirections != null) {
                ButtonDirections.Dispose ();
                ButtonDirections = null;
            }

            if (ImageStore != null) {
                ImageStore.Dispose ();
                ImageStore = null;
            }

            if (LabelAddress != null) {
                LabelAddress.Dispose ();
                LabelAddress = null;
            }

            if (LabelName != null) {
                LabelName.Dispose ();
                LabelName = null;
            }

            if (LabelTelephone != null) {
                LabelTelephone.Dispose ();
                LabelTelephone = null;
            }
        }
    }
}