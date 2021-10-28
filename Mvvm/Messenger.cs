using System;
using System.Collections.Generic;

namespace Glitonea.Mvvm
{
    public class Messenger
    {
        private Dictionary<object, Dictionary<Type, MulticastDelegate>> _recipients = new();

        public void Register<T>(object recipient, Action<T> handler)
        {
            if (!_recipients.ContainsKey(recipient))
            {
                _recipients.Add(recipient, new());
            }
            
            _recipients[recipient].Add(typeof(T), handler);
        }

        public void Unregister<T>(object recipient)
        {
            if (_recipients.ContainsKey(recipient))
            {
                if (_recipients[recipient].ContainsKey(typeof(T)))
                    _recipients[recipient].Remove(typeof(T));
            }
        }

        public void Send<T>(T message)
        {
            foreach (var kvp in _recipients)
            {
                if (kvp.Value.ContainsKey(typeof(T)))
                {
                    kvp.Value[typeof(T)].Method.Invoke(kvp.Key, new object[] {message});
                }
            }
        }
    }
}