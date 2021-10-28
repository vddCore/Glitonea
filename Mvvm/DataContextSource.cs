using System;
using Avalonia.Markup.Xaml;

namespace Glitonea.Mvvm
{
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
            return ViewModelResolver.Instance.Resolve(ViewModelType);
        }
    }
}