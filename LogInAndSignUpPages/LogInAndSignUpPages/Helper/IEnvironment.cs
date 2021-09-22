using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LogInAndSignUpPages.Helper
{
    public interface IEnvironment
    {
        void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint);
        void SetNavigationBarColor(System.Drawing.Color color);
        void ShowStatusBar(System.Drawing.Color color);
        void HideStatusBar();

    }
}
