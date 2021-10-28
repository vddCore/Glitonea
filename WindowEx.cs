using System;
using Avalonia;
using Avalonia.Controls;

namespace Glitonea
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

        private void CenterScreen()
        {
            var screen = Screens.ScreenFromVisual(this);

            Position = new PixelPoint(
                (int)(screen.Bounds.Width / 2 - Bounds.Width / 2),
                (int)(screen.Bounds.Height / 2 - Bounds.Height / 2)
            );
        }

        private void CenterOwner()
        {
            var ownerPosition = ((Window)Owner)!.Position;
            var ownerSize = Owner.Bounds.Size;

            Position = new PixelPoint(
                ownerPosition.X + (int)(ownerSize.Width / 2 - Bounds.Width / 2),
                ownerPosition.Y + (int)(ownerSize.Height / 2 - Bounds.Height / 2)
            );
        }
    }
}