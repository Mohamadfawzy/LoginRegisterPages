using LogInAndSignUpPages.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace LogInAndSignUpPages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnPage1 : ContentPage
    {
        Point center;
        double radius;
        public LearnPage1()
        {
            InitializeComponent();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.WhenAll(
            MyLabel.ColorTo(Color.Red, Color.Blue, c => MyLabel.TextColor = c, 5000),
            MyLabel.ColorTo(Color.Blue, Color.Red, c => MyLabel.BackgroundColor = c, 5000));
            await this.ColorTo(Color.FromRgb(0, 0, 0), Color.FromRgb(255, 255, 255), c => BackgroundColor = c, 5000);
            await boxView.ColorTo(Color.Blue, Color.Red, c => boxView.Color = c, 4000);
        }

        #region

        void OnSizeChanged(object sender, EventArgs args)
        {
            center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            radius = Math.Min(absoluteLayout.Width, absoluteLayout.Height) / 2;
            AbsoluteLayout.SetLayoutBounds(button,
                new Rectangle(center.X - button.Width / 2, center.Y - radius,
                              AbsoluteLayout.AutoSize,
                              AbsoluteLayout.AutoSize));
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            button.Rotation = 0;
            button.AnchorY = radius / button.Height;
            await button.RotateTo(360, 1000);
        }
        #endregion


    }
}