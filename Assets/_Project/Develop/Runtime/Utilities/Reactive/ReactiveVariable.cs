using System;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.Reactive
{
    public class ReactiveVariable<T> : IReadOnlyVariable<T> where T : IEquatable<T>
    {
        private readonly List<Subscriber<T, T>> _subscribers = new();

        private T _value;

        public ReactiveVariable() => _value = default;

        public ReactiveVariable(T value) => _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if(_value.Equals(oldValue) == false) 
                    foreach(Subscriber<T, T> subscriber in _subscribers)
                        subscriber.Invoke(oldValue, value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
        {
            Subscriber<T, T> subscribe = new Subscriber<T, T>(action, Remove);
            _subscribers.Add(subscribe);
            return subscribe;
        }

        private void Remove(Subscriber<T, T> subscriber) => _subscribers.Remove(subscriber);
    }
}
