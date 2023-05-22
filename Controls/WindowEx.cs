using System;
using Avalonia;
using Avalonia.Controls;

namespace Glitonea.Controls
{
    public class WindowEx : Window
    {
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            switch (WindowStartupLocation)
            {
                case WindowStartupLocation.CenterOwner:
                    CenterOwner();
                    break;
                
                case WindowStartupLocation.CenterScreen:
                    CenterScreen();
                    break;
            }
        }

        protected void CenterScreen()
        {
            var screen = Screens.ScreenFromVisual(this)!;

            Position = new PixelPoint(
                (int)((screen.Bounds.Width / 2 - Bounds.Width / 2) * screen.Scaling),
                (int)((screen.Bounds.Height / 2 - Bounds.Height / 2) * screen.Scaling)
            );
        }

        protected void CenterOwner()
        {
            if (Owner == null)
                return;
            
            var screen = Screens.ScreenFromVisual(this)!;
            var ownerPosition = ((Window)Owner).Position;
            var ownerSize = Owner.Bounds.Size;

            Position = new PixelPoint(
                (int)(ownerPosition.X + (ownerSize.Width / 2 - Bounds.Width / 2) * screen.Scaling),
                (int)(ownerPosition.Y + (ownerSize.Height / 2 - Bounds.Height / 2) * screen.Scaling)
            );
        }
    }
}