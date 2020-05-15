using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public Observable(Func<IEnumerable<T>, T> valueFactory, params Observable<T>[] sources)
        {
            foreach (var source in sources)
            {
                source.PropertyChanged += (_0, _1) =>
                {
                    Value = valueFactory(sources.Select(s => s.Value));
                };
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
