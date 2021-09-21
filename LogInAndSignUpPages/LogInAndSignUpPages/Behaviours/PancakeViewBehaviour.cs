using LogInAndSignUpPages.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace LogInAndSignUpPages.Behaviours
{
    public class PancakeViewBehaviour: Behavior<PancakeView>
    {
        CustomEntry entry = new CustomEntry();
        PancakeView frame = new PancakeView();
        protected override void OnAttachedTo(PancakeView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SizeChanged += Bindable_SizeChanged;
        }

        private void Bindable_SizeChanged(object sender, EventArgs e)
        {
            frame = sender as PancakeView;
            if (frame.Content.GetType() == typeof(CustomEntry))
            {
                var entry = frame.Content as CustomEntry;
                entry.TextChanged += Bindable_TextChanged;
            }
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            string EamilRegax = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            var entry = sender as Entry;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                if (Regex.IsMatch(entry.Text, EamilRegax, RegexOptions.IgnoreCase))
                {
                    //entry.TextColor = Color.Black;
                    frame.Border = new Border() { Color = Color.LightBlue, Thickness = 2 };
                }
                else
                {
                    //entry.TextColor = Color.DarkRed;
                    frame.Border = new Border() { Color = Color.Red, Thickness = 2 };
                }
            }
            else
            {
                frame.Border = new Border() { Color = Color.LightBlue, Thickness = 2 };
               // entry.TextColor = Color.DarkRed;
            }
        }


        protected override void OnDetachingFrom(PancakeView bindable)
        {
            base.OnDetachingFrom(bindable);
            entry.TextChanged -= Bindable_TextChanged;
            bindable.SizeChanged -= Bindable_SizeChanged;

        }
    }
}
