using Avalonia;

namespace Glitonea
{
    public static partial class Extensions
    {
        public static AppBuilder UseGlitoneaFramework(this AppBuilder appBuilder)
        {
            Glitonea.Initialize();
            return appBuilder;
        }
    }
}