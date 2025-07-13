namespace Glitonea;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

public static partial class Extensions
{
    public static IClassicDesktopStyleApplicationLifetime? GetDesktopLifetime(this Application app)
        => app.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        
    public static Window? GetMainWindow(this Application app)
        => app.GetDesktopLifetime()?.MainWindow;
}