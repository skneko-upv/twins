using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twins.Models.Properties
{
    public class Observable<T> : INotifyPropertyChanged
    {
        public T Value {
            get => value;
            set {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public T value;

        public event PropertyChangedEventHandler PropertyChanged;

        public Observable(T value)
        {
            Value = value;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
