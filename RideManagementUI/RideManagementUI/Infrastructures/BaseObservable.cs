using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RideManagementUI.Infrastructures
{
    public class BaseObservable : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(ref T privateProperty, T value,
           [CallerMemberName] string propertyName = "",
           Action? onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(privateProperty, value))
                return false;

            privateProperty = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
