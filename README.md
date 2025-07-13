## Glitonea
Glitonea is a minimal MVVM framework aiming to reduce boilerplate and improve project
manageability as it grows in size. Built on top of Autofac. For best development experience
using Glitonea make sure to install `PropertyChanged.Fody` package in your project.

## Usage

### Initialization

To use the MVVM capabilities:
```csharp
// Program.cs

using Avalonia;
using System;
using System.Reflection;
using Glitonea; // <-- Add this...

namespace AvaloniaApp1
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseGlitoneaFramework(Assembly.GetExecutingAssembly()) // <-- ...and this. Doesn't matter at which point.
                .WithInterFont()
                .LogToTrace();
    }
}
```

To use any resources provided by Glitonea:
```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:glitonea="clr-namespace:Glitonea;assembly=Glitonea"
             x:Class="AvaloniaApp1.App"
             RequestedThemeVariant="Default">
    <Application.Resources>
      <glitonea:Glitonea />
    </Application.Resources>
  
    <Application.Styles>
        <FluentTheme />
    </Application.Styles>
</Application>
```

### Integration
```csharp
using Glitonea.Mvvm;

namespace AvaloniaApp1
{
    public interface IGreeterService : IService
    {
        void Greet(string name);
    }
    
    public class GreeterService : IGreeterService
    {
        public void Greet(string name)
        {
            Console.WriteLine($"Hello, {name}!");
        }
    }
    
    public MainWindowViewModel : ViewModelBase
    {
        private readonly IGreeterService _greeterService;
        
        public string Name { get; set; }
        
        public MainWindowViewModel(IGreeterService greeterService)
        {
            _greeterService = greeterService;
        }
        
        public void DoGreet(object? parameter)
        {
            _greeterService.Greet(Name);
        }
    }
}
```

Data context initialization comes with a bit of a twist, though.
```xml
<Window x:Class="AvaloniaApp1.MainWindow"
        xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:mvvm="clr-namespace:Glitonea.Mvvm;Assembly=Glitonea"
        xmlns:local="clr-namespace:AvaloniaApp1;Assembly=AvaloniaApp1"
        Title="AvaloniaApp1"
        SizeToContent="WidthAndHeight"
        DataContext="{mvvm:DataContextSource local:MainWindowViewModel}">
  <!-- Like this. ^ -->
  
  <Border Padding="4">
    <StackPanel Spacing="4">
      <TextBox Width="200"
               Text="{Binding Name}" />

      <Button Content="Greet me!"
              Command="{Binding DoGreet}"
              HorizontalAlignment="Center" />
    </StackPanel>
  </Border>
</Window>
```

### Messaging
Only ViewModels are allowed to subscribe to messages:

```csharp
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;

namespace AvaloniaApp1
{
    public record SomethingHappenedMessage : Message;
    public record SomethingElseHappenedMessage(string What) : Message;
    
    /**
     * If you want to exclude a class from being analyzed
     * you can decorate your ViewModel with this attrtibute. 
     **/
    [DisableMessageHandlerAutoRegistration]
    public MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            /* Can do this like a caveman. */
            Subscribe<SomethingHappenedMessage>(SomethingHappenedHandler);
        }
        
        private void SomethingHappenedHandler(SomethingHappenedMessage m)
        {
            Console.WriteLine("unga bunga");
        }
        
        public void DoSomething()
        {
            new SomethingHappenedMessage()
                .Broadcast();
        }
        
        /* Or you can use the shiny, civilized way. */
        [MessageHandler]
        private void SomethingElseHappenedHandler(SomethingElseHappenedMessage m)
        {
        }
    }
}
```