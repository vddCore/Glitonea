using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace Glitonea.Extensions
{
    public static class ApplicationExtensions
    {
        public static Window GetMainWindow(this Application app)
        {
            if (!(app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicLifetime))
                return null;

            return classicLifetime.MainWindow;
        }
    }
}