using LogInAndSignUpPages.Fonts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LogInAndSignUpPages.ViewModels
{
    public class MainVM : ViewModelBase
    {
        string name;
        double age;
        string skills;
        private string keepMeLogged = IconFont.CheckCircle;
        public string KeepMeLogged
        {
            get => keepMeLogged;
            set
            {
                if (keepMeLogged == value) return;
                keepMeLogged = value;
                OnPropertyChanged(nameof(KeepMeLogged));
            }
        }
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        // Constructor ----------------------------------------
        public MainVM()
        {

        }

        public ICommand KeepMeLoggedCommand => new Command<string>(KeepMeLoggedExcuted);

        //Methods
        private void KeepMeLoggedExcuted(string label)
        {
            
            if (KeepMeLogged == IconFont.CheckCircle)
            {
                KeepMeLogged = IconFont.CheckboxBlankCircle;
                Preferences.Set("KeepLogged", false);
            }
            else
            {
                KeepMeLogged = IconFont.CheckCircle;
                Preferences.Set("KeepLogged", true);
            }
        }



    } // end main class
}
