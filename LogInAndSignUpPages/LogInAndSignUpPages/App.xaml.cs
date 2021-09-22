using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogInAndSignUpPages
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            //MainPage.FlowDirection = FlowDirection.RightToLeft;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
