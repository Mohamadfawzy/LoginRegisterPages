using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogInAndSignUpPages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanGestureLearnPage2 : ContentPage
    {
        public PanGestureLearnPage2()
        {
            InitializeComponent();
            // Set PanGestureRecognizer.TouchPoints to control the 
            // number of touch points needed to pan
        }


        double x, y;

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {

                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    image.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(image.Width - absolute.Width));
                    image.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(image.Height - absolute.Height));
                    Console.WriteLine($"x: {e.TotalX} y: {e.TotalY}");
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = image.TranslationX;
                    y = image.TranslationY;
                    break;
            }
        }

    }
}