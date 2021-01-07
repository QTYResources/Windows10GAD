using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _3DRotationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ShowCenterOfRotationValue();
        }

        private void xSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (centerOfRotationRadioButton.IsChecked == false)
            {
                double xValue = e.NewValue * 360 / 100;
                planeProjection.RotationX = xValue;
            }
            else
            {
                planeProjection.CenterOfRotationX = e.NewValue / 100;
                ShowCenterOfRotationValue();
            }
        }

        private void ySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (centerOfRotationRadioButton.IsChecked == false)
            {
                double yValue = e.NewValue * 360 / 100;
                planeProjection.RotationY = yValue;
            }
            else
            {
                planeProjection.CenterOfRotationY = e.NewValue / 100;
                ShowCenterOfRotationValue();
            }
        }

        private void zSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (centerOfRotationRadioButton.IsChecked == false)
            {
                double zValue = e.NewValue * 360 / 100;
                planeProjection.RotationZ = zValue;
            }
            else
            {
                planeProjection.CenterOfRotationZ = e.NewValue / 100;
                ShowCenterOfRotationValue();
            }
        }

        private void rotationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            xTextBlock.Text = "沿着X轴旋转";
            yTextBlock.Text = "沿着Y轴旋转";
            zTextBlock.Text = "沿着Z轴旋转";
        }

        private void centerOfRotationRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            xTextBlock.Text = "设置CenterOfRotationX的值";
            yTextBlock.Text = "设置CenterOfRotationY的值";
            zTextBlock.Text = "设置CenterOfRotationZ的值";
        }

        private void ShowCenterOfRotationValue()
        {
            infoTextBlock.Text = "CenterOfRotationX:" + planeProjection.CenterOfRotationX +
                " Y:" + planeProjection.CenterOfRotationY +
                " Z:" + planeProjection.CenterOfRotationZ;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
