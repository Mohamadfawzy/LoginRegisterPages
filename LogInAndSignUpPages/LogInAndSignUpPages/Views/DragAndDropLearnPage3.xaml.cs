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
    public partial class DragAndDropLearnPage3 : ContentPage
    {
        public DragAndDropLearnPage3()
        {
            InitializeComponent();
        }

        async void OnDropGestureRecognizerDrop(object sender, DropEventArgs e)
        {
            await DisplayAlert("Correct", "Congratulations!", "OK");
            //await DisplayAlert("Incorrect", "Better luck next time.", "OK");
        }

        void OnDropGestureRecognizerDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
    }
}