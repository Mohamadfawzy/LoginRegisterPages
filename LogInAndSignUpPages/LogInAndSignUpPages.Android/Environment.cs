using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LogInAndSignUpPages.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
[assembly:Dependency(typeof(LogInAndSignUpPages.Droid.Environment))]
namespace LogInAndSignUpPages.Droid
{
    public class Environment : IEnvironment
    {
        Android.App.Activity activity = Platform.CurrentActivity;
        Android.Views.Window window;
        public Environment()
        {
            window = activity.Window;
        }

        public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color.ToPlatformColor());

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
            }
        }

        public void SetNavigationBarColor(System.Drawing.Color color)
        {
            var activity = Platform.CurrentActivity;
            var window = activity.Window;
            window.SetNavigationBarColor(color.ToPlatformColor());
        }

        public void ShowStatusBar(System.Drawing.Color color)
        {
            throw new NotImplementedException();
        }

        public void HideStatusBar()
        {
            throw new NotImplementedException();
        }



    } // end main class
}