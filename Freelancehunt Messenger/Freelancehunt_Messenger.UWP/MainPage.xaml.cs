using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Freelancehunt_Messenger.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Freelancehunt_Messenger.App());
            

            if (Xamarin.Forms.Device.Idiom != Xamarin.Forms.TargetIdiom.Desktop)
            {
                _originalHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
                _originalWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                input = InputPane.GetForCurrentView();
                input.Showing += MainPage_Showing;
                input.Hiding += MainPage_Hiding;
                DisplayInformation.GetForCurrentView().OrientationChanged += MainPage_OrientationChanged;
            }
        }


        private void MainPage_OrientationChanged(DisplayInformation sender, object args)
        {
            input.Visible = false;
            MainPage_Hiding(null, null);
        }


        InputPane input;
        private double _originalHeight, _originalWidth;
        private void MainPage_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            this.Margin = new Thickness(0, 0, 0, 0);
            var or = DisplayInformation.GetForCurrentView().CurrentOrientation;
            if (or == DisplayOrientations.Portrait || or == DisplayOrientations.PortraitFlipped)
            {
                this.Height = _originalHeight;
                this.Width = _originalWidth;
                this.VerticalAlignment = VerticalAlignment.Bottom;
                this.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
            else
            {
                this.Height = _originalWidth;
                this.Width = _originalHeight - 10;
                this.VerticalAlignment = VerticalAlignment.Stretch;
                this.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }

        private void MainPage_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            var or = DisplayInformation.GetForCurrentView().CurrentOrientation;
            if (or == DisplayOrientations.Portrait || or == DisplayOrientations.PortraitFlipped)
            {
                this.Height = _originalHeight - args.OccludedRect.Height;
                this.Width = _originalWidth;
                this.Margin = new Thickness(0, 24, 0, 0);
                this.VerticalAlignment = VerticalAlignment.Top;
                this.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
            else
            {
                this.Height = _originalWidth - args.OccludedRect.Height;
                this.Width = _originalHeight - 10;
                this.Margin = new Thickness(0, 0, 0, 0);
                this.VerticalAlignment = VerticalAlignment.Top;
                this.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }
    }
}
