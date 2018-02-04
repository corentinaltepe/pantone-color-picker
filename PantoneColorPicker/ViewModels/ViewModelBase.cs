using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PantoneColorPicker.ViewModels
{
    /// <summary>
    /// Implements INotifyPropertyChanged for all ViewModel
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
