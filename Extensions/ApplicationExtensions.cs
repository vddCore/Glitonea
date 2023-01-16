using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Glitonea.Extensions
{
    public static class ApplicationExtensions
    {
        public static IClassicDesktopStyleApplicationLifetime GetDesktopLifetime(this Application app)
            => app.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        
        public static Window GetMainWindow(this Application app)
        {
            var classicLifetime = app.GetDesktopLifetime();
            
            if (classicLifetime == null)
                return null;

            return classicLifetime.MainWindow;
        }
    }
}