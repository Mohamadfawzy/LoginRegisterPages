using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace LogInAndSignUpPages.Behaviours
{
    public class EntryBehaviour : Behavior<Entry> 
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;

        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            string EamilRegax = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            var entry = sender as Entry;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                if (Regex.IsMatch(entry.Text, EamilRegax, RegexOptions.IgnoreCase))
                {
                    entry.TextColor = Color.Black;
                }
                else
                    entry.TextColor = Color.Red;
            }
            else
                entry.TextColor = Color.Red;
        }


        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

    }
}
