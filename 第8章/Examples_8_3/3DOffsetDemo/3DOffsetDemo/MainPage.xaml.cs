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

namespace _3DOffsetDemo
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
            if (globalRadioButton.IsChecked == true)
            {
                planeProjection.GlobalOffsetX = e.NewValue;
            }
            else
            {
                planeProjection.LocalOffsetX = e.NewValue;
            }
            ShowCenterOfRotationValue();
        }

        private void ySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (globalRadioButton.IsChecked == true)
            {
                planeProjection.GlobalOffsetY = e.NewValue;
            }
            else
            {
                planeProjection.LocalOffsetY = e.NewValue;
            }
            ShowCenterOfRotationValue();
        }

        private void zSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (globalRadioButton.IsChecked == true)
            {
                planeProjection.GlobalOffsetZ = e.NewValue;
            }
            else
            {
                planeProjection.LocalOffsetZ = e.NewValue;
            }
            ShowCenterOfRotationValue();
        }

        private void ShowCenterOfRotationValue()
        {
            infoTextBlock.Text = "GlobalOffsetX:" + planeProjection.GlobalOffsetX +
                " Y:" + planeProjection.GlobalOffsetY +
                " Z:" + planeProjection.GlobalOffsetZ +
            " LocalOffsetX:" + planeProjection.LocalOffsetX +
                " Y:" + planeProjection.LocalOffsetY +
                " Z:" + planeProjection.LocalOffsetZ;
        }
    }
}
