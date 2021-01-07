using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SliderDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            redSlider.Value = 128;
            greenSlider.Value = 128;
            blueSlider.Value = 128;
        }

        void OnSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            Color clr = Color.FromArgb(255, (byte)redSlider.Value,
                                            (byte)greenSlider.Value,
                                            (byte)blueSlider.Value);
            ellipse1.Fill = new SolidColorBrush(clr);
            textBlock1.Text = clr.ToString();
            redText.Text = clr.R.ToString("X2");
            greenText.Text = clr.G.ToString("X2");
            blueText.Text = clr.B.ToString("X2");
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
