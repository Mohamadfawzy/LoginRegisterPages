using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LogInAndSignUpPages.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        public  bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //protected void OnPropertyChanged(string proprty)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proprty));
        //}

        protected async Task DisplayAlert(string Messege, string Title = "Title", string c = "Cancel", string ok = "OK")
        {
            await App.Current.MainPage.DisplayAlert(Title, Messege, c, ok);
        }
    }
}
