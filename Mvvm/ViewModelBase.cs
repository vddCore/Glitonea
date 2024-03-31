using System;
using System.ComponentModel;
using Glitonea.Mvvm.Messaging;

namespace Glitonea.Mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected void Subscribe<T>(Action<T> handler) where T : Message
            => Message.Subscribe(this, handler);

        protected void Unsubscribe<T>() where T : Message
            => Message.Unsubscribe<T>(this);
        
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}