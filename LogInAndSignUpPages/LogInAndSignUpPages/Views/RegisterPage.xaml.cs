using LogInAndSignUpPages.Controllers;
using LogInAndSignUpPages.Fonts;
using LogInAndSignUpPages.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace LogInAndSignUpPages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        //Easing myEasing = Easing.SinIn;
        public RegisterPage()
        {
            InitializeComponent();
            //previousOwner = StackOwnerNameEmail;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            AbsoluteLayout.SetLayoutBounds(StackEntrys, new Rectangle(0, 0.2, 1, AbsoluteLayout.AutoSize));
            _ = RegisterPage_SizeChanged();
        }
        StackLayout stackCurrent = new StackLayout();
        StackLayout stackPrevious = new StackLayout();
        private async Task RegisterPage_SizeChanged()
        {
            await Task.Delay(100);
           // await FormForward(null, StackOwnerNameEmail);

            stackCurrent = StackOwnerNameEmail;
            var tasks = new List<Task<bool>>();
            tasks.Add(AnimateTitleForward(null,LblIntroduce));
            tasks.Add(FormForward(null, StackOwnerNameEmail));
            var results = await Task.WhenAll(tasks);
            stackPrevious = StackOwnerNameEmail;

            //List<Grid> grids = new List<Grid>();
            //grids.Add(StackOwnerNameEmail.Children[0] as Grid);
            //grids.Add(StackOwnerNameEmail.Children[1] as Grid);
            //Parallel.ForEach<Grid>(grids, async (grid) =>
            //{
            //    await SinInEntry(StackOwnerNameEmail, grid);
            //});
            //await SinInEntry(StackOwnerNameEmail, StackOwnerNameEmail.Children[0] as Grid);
        }


        VisualElement previousOwner;
        int form = 0;
        private async void Button_Forward(object sender, EventArgs e)
        {
            
            if (form == 0)
            {
                LblNameUserWrite.Text = EntryName.Text;
                await FormForward(StackOwnerNameEmail, StackOwnerPassword);
                stackCurrent = StackOwnerPassword;
                stackPrevious = StackOwnerNameEmail;
                form = 1;
            }
            else if (form == 1)
            {
                _ = EdgeAlert("dont found form = 1 ");
            }
        }

        private async void Button_Rearward(object sender, EventArgs e)
        {
            if(form == 1)
            {
                await FormRearward(stackPrevious, stackCurrent);
                form = 0;
            }
            else if(form == 0)
            {
                _ = EdgeAlert("dont found form = 0 ");
            }
        }
        // Focused
        private async void CustomEntry_Focused(object sender, FocusEventArgs e)
        {
            var entry = sender as CustomEntry;
            var grid = entry.Parent as Grid;
            var lineBlack = grid.Children[2] as BoxView;
            lineBlack.AnchorX = 0;
            if (lineBlack.ScaleX == 0)
                await lineBlack.ScaleXTo(1, 300, Easing.Linear);
        }
        // UnFocused
        private async void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as CustomEntry;
            var grid = entry.Parent as Grid;
            var lineBlack = grid.Children[2] as BoxView;
            var lineGray = grid.Children[1] as BoxView;
            if (string.IsNullOrEmpty(entry.Text))
            {
                lineGray.ScaleX = 1;
                await lineBlack.ScaleXTo(0, 250, Easing.Linear);
            }

        }
        // Notification
        private async Task EdgeAlert(string text, string title = "Alarm")
        {
            var alertImage = new Label { Text = IconFont.BellRing, Style = Application.Current.Resources["ImageBellRing"] as Style };
            var stack = new StackLayout
            {
                Style = Application.Current.Resources["StackNotification"] as Style,
                Children =
                {
                    alertImage,
                    new StackLayout()
                    {
                        Padding =new Thickness (0,10,0,0),
                        Children =
                        {
                            new Label { Text = title,FontSize=22, Style = Application.Current.Resources["LabelNotification"] as Style },
                            new Label { Text = text, Style = Application.Current.Resources["LabelNotification"] as Style },

                        }

                    }
                }
            };
            MainGrid.Children.Add(stack);
            var an = new Animation();
            var taskCompletionSource = new TaskCompletionSource<bool>();

            an.Add(0, 0.1, new Animation(v => stack.TranslationY = v, -80, 0));

            an.Add(0.1, 0.2, new Animation(v => alertImage.Rotation = v, 0, 5));
            an.Add(0.2, 0.3, new Animation(v => alertImage.Rotation = v, 5, 0));
            an.Add(0.3, 0.4, new Animation(v => alertImage.Rotation = v, 0, -5));
            an.Add(0.4, 0.5, new Animation(v => alertImage.Rotation = v, -5, 0));
            an.Add(0.5, 0.6, new Animation(v => alertImage.Rotation = v, 0, 5));
            an.Add(0.6, 0.7, new Animation(v => alertImage.Rotation = v, 5, 0));

            an.Add(0.9, 1, new Animation(v => stack.TranslationY = v, 0, -81));
            an.Commit(owner: stack, "alert", length: 700 + 2000 + 300, easing: Easing.SinInOut,
             finished: (x, c) =>
             {
                 taskCompletionSource.SetResult(c);
                 MainGrid.Children.Remove(stack);
             });

            //await stack.TranslateTo(0, 0,700 ,easing: Easing.SinOut);
            //await Task.Delay(2000);
            //await stack.TranslateTo(0, -81,300, easing: Easing.SinOut);
        }


        private async Task<bool> FormForward(StackLayout sinOut, StackLayout sinIN)
        {
            List<Task<bool>> tasks = new List<Task<bool>>();
            bool[] results;
            if (sinOut != null)
            {
                tasks.Add(AnimationEntry.SinOutEntry(sinOut, sinOut.Children[0] as Grid));
                tasks.Add(AnimationEntry.SinOutEntry(sinOut, sinOut.Children[1] as Grid));
                tasks.Add(AnimateTitleForward(LblIntroduce, LblNameUserWrite));
                results = await Task.WhenAll(tasks);
            }
            tasks = new List<Task<bool>>();
            tasks.Add(AnimationEntry.SinInEntry(sinIN, sinIN.Children[0] as Grid));
            tasks.Add(AnimationEntry.SinInEntry(sinIN, sinIN.Children[1] as Grid));
            results = await Task.WhenAll(tasks);
            return await Task.FromResult(true);
        }
        private async Task<bool> FormRearward(StackLayout Previous, StackLayout Current, Easing myEasing  = null)
        {
            myEasing = myEasing ?? Easing.SinIn;
            List<Task<bool>> tasks = new List<Task<bool>>();
            tasks.Add(AnimationEntry.SinOutEntry(Current, Current.Children[0] as Grid));
            tasks.Add(AnimationEntry.SinOutEntry(Current, Current.Children[1] as Grid));
            tasks.Add(AnimateTitleRearward(LblNameUserWrite, LblIntroduce));
            var results = await Task.WhenAll(tasks);

            tasks.Add(AnimationEntry.SinInEntry(Previous, Previous.Children[0] as Grid));
            tasks.Add(AnimationEntry.SinInEntry(Previous, Previous.Children[1] as Grid));
            results = await Task.WhenAll(tasks);
            return await Task.FromResult(true);
        }
        
        private async Task<bool> AnimateTitleForward(Label CurrentView, Label newView, Easing myEasing = null)
        {
            myEasing = myEasing ?? Easing.SinIn;
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var ann = new Animation();
            newView.TranslationX = 250;
            newView.Opacity = 0;
            
            if (CurrentView != null)
            {
                ann.Add(0, 1, new Animation(v => CurrentView.TranslationX = v, 0, -250, myEasing));
                ann.Add(0.5, 1, new Animation(v => CurrentView.Opacity = v, 1, 0, myEasing));
            }
            //
            ann.Add(0, 1, new Animation(v => newView.TranslationX = v, 250, 0, myEasing));
            ann.Add(0, 0.5, new Animation(v => newView.Opacity = v, 0, 1, myEasing));

            ann.Commit(owner: newView, "view1", length: 800,
                 finished: (x, c) =>
                 {
                     taskCompletionSource.SetResult(c);
                 });
            return await taskCompletionSource.Task;
        }
        //
        private async Task<bool> AnimateTitleRearward(Label CurrentView, Label PreviousView, Easing myEasing = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var ann = new Animation();
            PreviousView.TranslationX = -250;
            PreviousView.Opacity = 0;
            if (PreviousView != null)
            {
                ann.Add(0, 1, new Animation(v => PreviousView.TranslationX = v, -250, 0, myEasing));
                ann.Add(0, 1, new Animation(v => PreviousView.Opacity = v, 0, 1, myEasing));
            }
            //
            ann.Add(0, 1, new Animation(v => CurrentView.TranslationX = v, 0, 250, myEasing));
            ann.Add(0, 0.3, new Animation(v => CurrentView.Opacity = v, 1, 0, myEasing));

            ann.Commit(owner: CurrentView, "view", length: 600,
                 finished: (x, c) =>
                 {
                     taskCompletionSource.SetResult(c);
                 });
            return await taskCompletionSource.Task;
        }
        
        #region animated entry old
        private async void Forward(object sender, EventArgs e)
        {
            var result = await OldSinOut(StackOwnerNameEmail, EntryName, EntryEmail, LineEntryNameGray, LineEntryEmailGray, LineEntryNameBlack, LineEntryEmailBlack);
            if (result)
                await OldSinIn(StackOwnerPassword, EntryPassword, EntryRepeatPassword, LineEntryPasswordGray, LineEntryRepeatPasswordGray, LineEntryPasswordBlack, LineEntryRepeatPasswordBlack);
            else
            {
                await EdgeAlert("Please fill in the blank fields");
            }
        }
        private async void Rearward(object sender, EventArgs e)
        {
            await OldSinOut(StackOwnerPassword, EntryPassword, EntryRepeatPassword, LineEntryPasswordGray, LineEntryRepeatPasswordGray, LineEntryPasswordBlack, LineEntryRepeatPasswordBlack);
            await OldSinIn(StackOwnerNameEmail, EntryName, EntryEmail, LineEntryNameGray, LineEntryEmailGray, LineEntryNameBlack, LineEntryEmailBlack);

        }
        private async Task<bool> OldSinOut(VisualElement owner, CustomEntry entry1, CustomEntry entry2, VisualElement lineGray1, VisualElement lineGray2, BoxView lineBlack1, BoxView lineBlack2, Easing myEasing = null)
        {
            var an = new Animation();
            var taskCompletionSource = new TaskCompletionSource<bool>();

            if (previousOwner != null)
                previousOwner.IsVisible = false;

            lineGray1.ScaleX = 0;
            lineGray2.ScaleX = 0;
            //lineBlack1.AnchorX = 1;
            //lineBlack2.AnchorX = 1;
            owner.IsVisible = true;

            an.Add(0, 1, new Animation(v => entry1.TranslationY = v, 0, -15, myEasing));
            an.Add(0, 1, new Animation(v => entry2.TranslationY = v, 0, -15, myEasing));
            an.Add(0, 1, new Animation(v => entry1.Opacity = v, 1, 0, myEasing));
            an.Add(0, 1, new Animation(v => entry2.Opacity = v, 1, 0, myEasing));

            if (lineBlack1.ScaleX == 1)
            {
                an.Add(0, 1, new Animation(v => lineBlack1.ScaleX = v, 1, 0, myEasing));
                an.Add(0, 1, new Animation(v => lineBlack2.ScaleX = v, 1, 0, myEasing));
            }
            else
            {
                an.Add(0, 1, new Animation(v => lineGray1.ScaleX = v, 1, 0, myEasing));
                an.Add(0, 1, new Animation(v => lineGray2.ScaleX = v, 1, 0, myEasing));
            }

            an.Commit(owner: owner, name: "anim1", length: 600,
             finished: (x, c) =>
             {
                 taskCompletionSource.SetResult(true);
             });
            previousOwner = owner;
            return await taskCompletionSource.Task;
        }
        private async Task<bool> OldSinIn(VisualElement owner, VisualElement entry1, VisualElement entry2, VisualElement lineGray1, VisualElement lineGray2, BoxView lineBlack1, BoxView lineBlack2, Easing myEasing = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var ann = new Animation();

            if (previousOwner != null)
                previousOwner.IsVisible = false;

            lineGray1.ScaleX = 0;
            lineGray2.ScaleX = 0;
            lineGray1.AnchorX = 1;
            lineGray2.AnchorX = 1;
            owner.IsVisible = true;

            ann.Add(0, 1, new Animation(v => entry1.TranslationY = v, 15, 0, myEasing));
            ann.Add(0, 1, new Animation(v => entry2.TranslationY = v, 15, 0, myEasing));
            ann.Add(0, 1, new Animation(v => entry1.Opacity = v, 0, 1, myEasing));
            ann.Add(0, 1, new Animation(v => entry2.Opacity = v, 0, 1, myEasing));
            if (lineBlack1.ScaleX == 1 || lineBlack1.AnchorX == 1)
            {
                ann.Add(0, 1, new Animation(v => lineBlack1.ScaleX = v, 0, 1, myEasing));
                ann.Add(0, 1, new Animation(v => lineBlack2.ScaleX = v, 0, 1, myEasing));
                lineBlack1.AnchorX = 1;
            }
            else
            {
                ann.Add(0, 1, new Animation(v => lineGray1.ScaleX = v, 0, 1, myEasing));
                ann.Add(0, 1, new Animation(v => lineGray2.ScaleX = v, 0, 1, myEasing));
                lineBlack1.AnchorX = 0;
            }


            ann.Commit(owner: owner, name: "anim1", length: 600,
                 finished: (x, c) =>
                 {
                     taskCompletionSource.SetResult(c);
                 });
            previousOwner = owner;
            return await taskCompletionSource.Task;
        }
        private async Task<bool> SinOutNameEmail(Easing myEasing)
        {
            LineEntryNameGray.IsVisible = false;
            LineEntryEmailGray.IsVisible = false;
            LineEntryNameBlack.AnchorX = 1;
            LineEntryEmailBlack.AnchorX = 1;

            var an = new Animation();
            var taskCompletionSource = new TaskCompletionSource<bool>();

            an.Add(0, 1, new Animation(v => EntryName.TranslationY = v, 0, -15, myEasing));
            an.Add(0, 1, new Animation(v => EntryEmail.TranslationY = v, 0, -15, myEasing));
            an.Add(0, 1, new Animation(v => EntryName.Opacity = v, 1, 0, myEasing));
            an.Add(0, 1, new Animation(v => EntryEmail.Opacity = v, 1, 0, myEasing));

            an.Add(0, 1, new Animation(v => LineEntryEmailBlack.ScaleX = v, 1, 0, myEasing));
            an.Add(0, 1, new Animation(v => LineEntryNameBlack.ScaleX = v, 1, 0, myEasing));
            an.Commit(owner: StackOwnerNameEmail, name: "anim1", length: 600,
                 finished: (x, c) =>
                 {
                     taskCompletionSource.SetResult(c);
                     StackOwnerNameEmail.IsVisible = false;
                     StackOwnerPassword.IsVisible = true;
                 });
            return await taskCompletionSource.Task;
        }
        private async Task<bool> SinInPassword(Easing myEasing)
        {
            // Password animate
            //StackOwnerPassword.IsVisible = true;
            var ann = new Animation();
            LineEntryPasswordGray.AnchorX = 1;
            LineEntryRepeatPasswordGray.AnchorX = 1;
            LineEntryPasswordGray.IsVisible = true;
            LineEntryRepeatPasswordGray.IsVisible = true;
            ann.Add(0, 1, new Animation(v => EntryPassword.TranslationY = v, 15, 0, myEasing));
            ann.Add(0, 1, new Animation(v => EntryRepeatPassword.TranslationY = v, 15, 0, myEasing));
            ann.Add(0, 1, new Animation(v => EntryPassword.Opacity = v, 0, 1, myEasing));
            ann.Add(0, 1, new Animation(v => EntryRepeatPassword.Opacity = v, 0, 1, myEasing));

            ann.Add(0, 1, new Animation(v => LineEntryPasswordGray.ScaleX = v, 0, 1, myEasing));
            ann.Add(0, 1, new Animation(v => LineEntryRepeatPasswordGray.ScaleX = v, 0, 1, myEasing));
            ann.Commit(owner: StackOwnerPassword, name: "anim1", length: 600,
                 finished: (x, y) =>
                 {
                     //StackOwnerNameEmail.IsVisible = false;
                     //StackOwnerPassword.IsVisible = true;
                 });
            return await Task.FromResult(true);
        }
        #endregion

    }
}