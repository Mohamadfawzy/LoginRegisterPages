using LogInAndSignUpPages.Controllers;
using LogInAndSignUpPages.Fonts;
using LogInAndSignUpPages.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace LogInAndSignUpPages
{
    public partial class MainPage : ContentPage
    {
        Color PlacholderColor = Color.Default;
        public MainPage()
        {
            InitializeComponent();
        }
        // Method Override 
        protected override bool OnBackButtonPressed()
        {
            if (PopupForgotPass.IsVisible)
            {
                return true;
            }
            return base.OnBackButtonPressed();
        }
        // actions
        private void TapFocusOnEmail(object sender, EventArgs e)
        {
            var label = sender as Label;
            var grid = label.Parent as Grid;
            var frame = grid.Children[0] as PancakeView;
            if (frame.Content.GetType() == typeof(CustomEntry))
            {
                var entry = frame.Content as CustomEntry;
                entry.Focus();
                System.Console.WriteLine("OK");
            }
        }
        private async void Entry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            var frame = entry.Parent as PancakeView;
            var grid = frame.Parent as Grid;
            var label = grid.Children[1] as Label;
            PlacholderColor = label.TextColor;
            //
            var height = grid.Height;
            var point = height / -2;
            var widthLable = label.Width;
            var lablePading = ((widthLable * 3 / 10) / 2) - 3;

            double flowDirction = this.FlowDirection == FlowDirection.RightToLeft ? lablePading : -lablePading;

            frame.Border = new Border() { Color = Color.LightBlue, Thickness = 2 };
            var t1 = label.ScaleTo(0.7, 100, Easing.SpringIn);
            var t2 =  label.TranslateTo(flowDirction, point, 100, Easing.SinOut);
            await Task.WhenAll(t1, t2);
            label.TextColor = Color.LightBlue;

        }
        private async void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            var frame = entry.Parent as PancakeView;
            var grid = frame.Parent as Grid;
            var label = grid.Children[1] as Label;

            if (string.IsNullOrEmpty(entry.Text))
            {
                frame.Border = new Border() { Color = Color.LightGray, Thickness = 2 };
                var t1 = label.ScaleTo(1, 100, Easing.SpringIn);
                var t2 = label.TranslateTo(0, 0, 100, Easing.SinInOut);
                await Task.WhenAll(t1, t2);
                label.TextColor = PlacholderColor;
            }
            else
            {
                if (entry.Keyboard == Keyboard.Email)
                {
                    if (EmailValidation(entry.Text))
                    {
                        frame.Border = new Border() { Color = Color.LightGray, Thickness = 2 };
                    }
                    else
                    {
                        frame.Border = new Border() { Color = Color.Red, Thickness = 2 };
                    }
                }
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            IsASmallDevice();
            //switch (x)
            //{
            //    case 0:
            //        App.Current.UserAppTheme = OSAppTheme.Unspecified;
            //        break;
            //    case 1:
            //        App.Current.UserAppTheme = OSAppTheme.Light;
            //        break;
            //    case 2:
            //        App.Current.UserAppTheme = OSAppTheme.Dark;
            //        break;
            //}
            //x++;
            //if (x == 3) x = 0;
        }
        private void Tap_KeepMeLogged(object sender, EventArgs e)
        {

            if (CheckBoxCircal.Text == IconFont.CheckCircle)
            {
                CheckBoxCircal.Text = IconFont.CheckboxBlankCircle;
                Preferences.Set("KeepLogged", false);
            }
            else
            {
                CheckBoxCircal.Text = IconFont.CheckCircle;
                Preferences.Set("KeepLogged", true);
            }
        }
        private async void TapFlyingIn(object sender, EventArgs e)
        {
            ForgotPass.TextColor = Color.FromHex("#50FFFFFF");
            await FlyingIn();

        }
        private void Tap_GoTo_SignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());

        }
        private async void Tap_ReturnToLogIn(object sender, EventArgs e)
        {
            await FlyingOut();
            EntryEmail.Focus();
            EntryEmail.Text = EntryEmailPopup.Text;
        }
        private void Tap_EyePassword(object sender, EventArgs e)
        {
            var label = sender as Label;
            var grid = label.Parent as Grid;
            var frame = grid.Children[0] as PancakeView;
            var entry = new CustomEntry();
            if (frame.Content.GetType() == typeof(CustomEntry))
            {
                entry = frame.Content as CustomEntry;
            }

            if (label.Text == IconFont.EyeOff)
            {
                label.Text = IconFont.Eye;
                label.TextColor = Color.Black;
                entry.IsPassword = false;
            }
            else
            {
                label.Text = IconFont.EyeOff;
                label.TextColor = Color.Gray;
                entry.IsPassword = true;
            }
            //entry.Unfocus();
            //entry.Focus();

        }
        private async void TapFlyingOut(object sender, EventArgs e)
        {
            await FlyingOut();
        }
        // Methods -------------------------------
        private async Task FlyingIn()
        {
            ForgotPass.IsEnabled = false;
            PopupForgotPass.IsVisible = false;
            // position popup window
            AbsoluteLayout.SetLayoutFlags(PopupForgotPass, AbsoluteLayoutFlags.None);
            AbsoluteLayout.SetLayoutBounds(PopupForgotPass, ForgotPass.Bounds);
            AbsoluteLayout.SetLayoutBounds(new StackLayout(), ForgotPass.Bounds);

            PopupForgotPass.IsVisible = true;
            // Shadow behind the popup
            BackgrounWhenFlaing.ScaleY = 0;
            BackgrounWhenFlaing.Opacity = 1;
            BackgrounWhenFlaing.IsVisible = true;
            var t1 = await BackgrounWhenFlaing.ScaleYTo(1, 300, Easing.SinOut);
            // flying popup
            var bound = FlyingBox.Bounds;
            var t2 =  await PopupForgotPass.LayoutTo(bound, 250, Easing.SinOut);
            //await Task.WhenAll(t1, t2);
            StackInsideFlayingForgotPass.Scale = 1;


        }
        private async Task FlyingOut()
        {
            if (!PopupForgotPass.IsVisible) return;
            var bound = ForgotPass.Bounds;

            var t1 = await BackgrounWhenFlaing.ScaleYTo(0, 200, Easing.SinOut);
            StackInsideFlayingForgotPass.Scale = 0;
            var t2 = await PopupForgotPass .LayoutTo(bound, 400, Easing.SinOut);

            //await Task.WhenAll(t1, t2 );
            PopupForgotPass.IsVisible = false;
            BackgrounWhenFlaing.IsVisible = false;
            ForgotPass.TextColor = Color.FromHex("#FFFFFF");
            ForgotPass.IsEnabled = true;
        }
        private bool EmailValidation(string email)
        {
            string EamilRegax = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (Regex.IsMatch(email, EamilRegax, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MultiColor⁯Page());
        }
        // not used
        public void IsASmallDevice()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            DisplayAlert("Title", $"width: {width} height: {height}", "Cancel");
        }
    }// end main class
}
