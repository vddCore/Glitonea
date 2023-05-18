using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Platform;
using Avalonia.Styling;

namespace Glitonea.Controls
{
    public class FluentWindow : WindowEx, IStyleable
    {
        Type IStyleable.StyleKey => typeof(FluentWindow);

        private Control? _titleContainer;

        public static readonly StyledProperty<object> TitleBarLeftSideContentProperty =
            AvaloniaProperty.Register<FluentWindow, object>(
                nameof(TitleBarLeftSideContent)
            );

        public static readonly StyledProperty<HorizontalAlignment> HorizontalTitleBarLeftSideContentAlignmentProperty =
            AvaloniaProperty.Register<FluentWindow, HorizontalAlignment>(
                nameof(HorizontalTitleBarLeftSideContentAlignment)
            );
        
        public static readonly StyledProperty<VerticalAlignment> VerticalTitleBarLeftSideContentAlignmentProperty =
            AvaloniaProperty.Register<FluentWindow, VerticalAlignment>(
                nameof(HorizontalTitleBarLeftSideContentAlignment)
            );

        public object TitleBarLeftSideContent
        {
            get => GetValue(TitleBarLeftSideContentProperty);
            set => SetValue(TitleBarLeftSideContentProperty, value);
        }

        public HorizontalAlignment HorizontalTitleBarLeftSideContentAlignment
        {
            get => GetValue(HorizontalTitleBarLeftSideContentAlignmentProperty);
            set => SetValue(HorizontalTitleBarLeftSideContentAlignmentProperty, value);
        }

        public VerticalAlignment VerticalTitleBarLeftSideContentAlignment
        {
            get => GetValue(VerticalTitleBarLeftSideContentAlignmentProperty);
            set => SetValue(VerticalTitleBarLeftSideContentAlignmentProperty, value);
        }
        
        public FluentWindow()
        {
            TransparencyLevelHint = WindowTransparencyLevel.AcrylicBlur;
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = 36;
            
            this.GetObservable(WindowStateProperty)
                .Subscribe(x =>
                {
                    PseudoClasses.Set(":normal", x == WindowState.Normal);
                    PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                    PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                    PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                });
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            _titleContainer = e.NameScope.Find<Grid>("PART_TitleContainer");
            _titleContainer!.PointerPressed += OnTitleContainerPointerPressed;

            e.NameScope.Find<CaptionButtons>(
                "PART_CaptionButtons"
            )?.Attach(this);
        }

        private void OnTitleContainerPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var properties = e.GetCurrentPoint(null).Properties;

            if (properties.IsLeftButtonPressed)
            {
                if (e.ClickCount == 2)
                {
                    WindowState = WindowState == WindowState.Maximized 
                        ? WindowState.Normal 
                        : WindowState.Maximized;
                }
                else
                {
                    BeginMoveDrag(e);
                }
            }
        }
    }
}