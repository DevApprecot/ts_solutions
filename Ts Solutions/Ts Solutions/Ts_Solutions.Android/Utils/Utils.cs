using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace Ts_Solutions.Droid.Utils
{
    public static class Utils
    {
        public static void HideKeyboard(this View view)
        {
            var inputMethodManager = (InputMethodManager)view.Context.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(view.WindowToken, 0);
        }
    }
}