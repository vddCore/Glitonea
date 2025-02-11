namespace Glitonea;

using Avalonia;
using Avalonia.Controls;

public static partial class Extensions
{
    public static void CenterOwner(this Window window)
    {
        var owner = window.Owner;
        var screen = window.Screens.ScreenFromVisual(window);
            
        if (owner == null || screen == null)
            return;
            
        var ownerPosition = ((Window)owner).Position;
        var ownerSize = owner.Bounds.Size;

        window.Position = new PixelPoint(
            (int)(ownerPosition.X + (ownerSize.Width / 2 - window.Bounds.Width / 2) * screen.Scaling),
            (int)(ownerPosition.Y + (ownerSize.Height / 2 - window.Bounds.Height / 2) * screen.Scaling)
        );
    }

    public static void CenterScreen(this Window window)
    {
        var screen = window.Screens.ScreenFromVisual(window);

        if (screen == null)
            return;

        window.Position = new PixelPoint(
            (int)((screen.Bounds.Width / 2 - window.Bounds.Width / 2) * screen.Scaling),
            (int)((screen.Bounds.Height / 2 - window.Bounds.Height / 2) * screen.Scaling)
        );
    }
}