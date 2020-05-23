using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace Twins.Models.Properties
{
    public class Observable<T> : INotifyPropertyChanged, IObservable<T>
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

        private readonly IList<IObserver<T>> observers = new List<IObserver<T>>();

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
            observers.ForEach(observer => observer.OnNext(Value));
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }
    }

    class Unsubscriber<T> : IDisposable
    {
        readonly ICollection<IObserver<T>> observers;
        readonly IObserver<T> observer;

        public Unsubscriber(ICollection<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }
}
