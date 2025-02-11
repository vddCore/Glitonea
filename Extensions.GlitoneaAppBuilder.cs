namespace Glitonea;

using System.Reflection;
using Avalonia;

public static partial class Extensions
{
    public static AppBuilder UseGlitoneaFramework(this AppBuilder appBuilder, params Assembly[] sourceAssemblies)
    {
        Glitonea.Initialize(sourceAssemblies);
        return appBuilder;
    }
}