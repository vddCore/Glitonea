namespace Glitonea.Mvvm;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using global::Glitonea.Mvvm.Messaging;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
        
    protected void Subscribe<T>(Action<T> handler) where T : Message
        => Message.Subscribe(this, handler);

    protected void Unsubscribe<T>() where T : Message
        => Message.Unsubscribe<T>(this);
        
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual void OnPropertiesChanged(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames) 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}