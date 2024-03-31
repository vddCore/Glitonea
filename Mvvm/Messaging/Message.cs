using System;
using System.Collections.Generic;
using System.Linq;

namespace Glitonea.Mvvm.Messaging
{
    public abstract record Message
    {
        private static Dictionary<object, Dictionary<Type, MulticastDelegate>> _recipients = new();

        public static void Broadcast<T>(T message) where T : Message
            => PushToSubscribers(message);
        
        public static void Broadcast<T>() where T : Message, new()
            => PushToSubscribers(new T());

        public static void BroadcastToType<T, U>(T message) where T : Message
            => PushToSubscribers(message, typeof(U));
        
        public static void BroadcastToType<T, U>() where T : Message, new()
            => PushToSubscribers(new T(), typeof(U));

        public void Broadcast()
            => PushToSubscribers(this);

        public void BroadcastToType<U>()
            => PushToSubscribers(this, typeof(U));
        
        internal static void Subscribe<T>(object recipient, Action<T> handler)
        {
            if (!_recipients.ContainsKey(recipient))
            {
                _recipients.Add(recipient, new());
            }

            _recipients[recipient].Add(typeof(T), handler);
        }

        internal static void Unsubscribe<T>(object recipient)
        {
            if (_recipients.ContainsKey(recipient))
            {
                if (_recipients[recipient].ContainsKey(typeof(T)))
                    _recipients[recipient].Remove(typeof(T));
            }
        }

        private static void PushToSubscribers(object message)
        {
            var t = message.GetType();

            var invocationList = new Dictionary<object, Dictionary<Type, MulticastDelegate>>(_recipients);
            
            foreach (var kvp in invocationList)
            {
                if (kvp.Value.TryGetValue(t, out var value))
                {
                    value.Method.Invoke(kvp.Key, new[] { message });
                }
            }
        }
        
        private static void PushToSubscribers(object message, Type subscriberType)
        {
            var t = message.GetType();

            foreach (var kvp in _recipients.Where(x => subscriberType.IsAssignableFrom(x.GetType())))
            {
                if (kvp.Value.TryGetValue(t, out var value))
                {
                    value.Method.Invoke(kvp.Key, new[] { message });
                }
            }
        }
    }
}