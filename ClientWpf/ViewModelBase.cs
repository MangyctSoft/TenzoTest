using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientWpf
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
