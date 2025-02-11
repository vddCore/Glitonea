namespace Glitonea.Mvvm;

using System;
using Avalonia.Markup.Xaml;

public class DataContextSource : MarkupExtension
{
    [ConstructorArgument("viewModelType")]
    public Type ViewModelType { get; set; }

    public DataContextSource(Type viewModelType)
    {
        ViewModelType = viewModelType;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return ViewModelResolver.Instance.Resolve(ViewModelType)
               ?? throw new InvalidOperationException($"Unable to resoolve type '{ViewModelType.FullName}' as a viable DataContext.");
    }
}