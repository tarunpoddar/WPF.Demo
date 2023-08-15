using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.Common.Mvvm
{
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property values are changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        
        /// <summary>
        /// Must be called when a property value changed.
        /// </summary>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Helper method to set the property value and call OnPropertyChanged.
        /// </summary>
        /// <returns>True if the property changed, false otherwise</returns>
        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            if (property?.Equals(value) ?? false)
            {
                return false;
            }

            property = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}