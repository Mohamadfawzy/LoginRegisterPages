using LogInAndSignUpPages.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LogInAndSignUpPages.Helper
{
    public static class AnimationEntry
    {
        static VisualElement previousOwner;
        
        public  static  async Task<bool> SinInEntry(VisualElement visualElement, Grid name, Easing myEasing =null)
        {
            myEasing = myEasing ?? Easing.SinIn;
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var ann = new Animation();

            if (previousOwner != null)
                previousOwner.IsVisible = false;
            //
            var owner = visualElement as StackLayout;
            var grid = name as Grid;
            var entry = grid.Children[0] as CustomEntry;
            var lineGray = grid.Children[1] as BoxView;
            var lineBlack = grid.Children[2] as BoxView;
            //

            lineGray.ScaleX = 0;
            owner.IsVisible = true;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                ann.Add(0, 1, new Animation(v => lineBlack.ScaleX = v, 0, 1, myEasing));
            }
            else
            {
                ann.Add(0, 1, new Animation(v => lineGray.ScaleX = v, 0, 1, myEasing));
            }

            ann.Add(0, 1, new Animation(v => entry.TranslationY = v, 15, 0, myEasing));
            ann.Add(0, 1, new Animation(v => entry.Opacity = v, 0, 1, myEasing));

            ann.Commit(owner: owner, grid.Id.ToString(), length: 600,
                 finished: (x, c) =>
                 {
                     taskCompletionSource.SetResult(c);
                 });
            previousOwner = owner;
            return await taskCompletionSource.Task;
        }
        // out 
        public  static async Task<bool> SinOutEntry(VisualElement visualElement, Grid name, Easing myEasing = null)
        {
            myEasing = myEasing ?? Easing.SinIn;
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var an = new Animation();

            if (previousOwner != null)
                previousOwner.IsVisible = false;

            var owner = visualElement as StackLayout;
            var grid = name as Grid;
            var entry = grid.Children[0] as CustomEntry;
            var lineGray = grid.Children[1] as BoxView;
            var lineBlack = grid.Children[2] as BoxView;


            lineGray.ScaleX = 0;
            owner.IsVisible = true;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                lineBlack.AnchorX = 1;
                an.Add(0, 1, new Animation(v => lineBlack.ScaleX = v, 1, 0, myEasing));
            }
            else
            {
                an.Add(0, 1, new Animation(v => lineGray.ScaleX = v, 1, 0, myEasing));
            }

            an.Add(0, 1, new Animation(v => entry.TranslationY = v, 0, -15, myEasing));
            an.Add(0, 1, new Animation(v => entry.Opacity = v, 1, 0, myEasing));

            an.Commit(owner: owner, name: grid.Id.ToString(), length: 600,
             finished: (x, c) =>
             {
                 taskCompletionSource.SetResult(c);
             });
            previousOwner = owner;
            return await taskCompletionSource.Task;
        }






    }
}
