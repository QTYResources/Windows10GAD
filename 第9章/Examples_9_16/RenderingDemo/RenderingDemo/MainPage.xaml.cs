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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RenderingDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point mouseLocation;
        TranslateTransform translateTransform = new TranslateTransform();
        DateTime preTime = DateTime.Now;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            this.rectangle.RenderTransform = translateTransform;
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            var currentTime = DateTime.Now;
            double elapsedTime = (currentTime - preTime).TotalSeconds;
            preTime = currentTime;

            translateTransform.X += mouseLocation.X * elapsedTime;
            if (translateTransform.X > 300) translateTransform.X = 300;
            if (translateTransform.X < 0) translateTransform.X = 0;
            translateTransform.Y += mouseLocation.Y * elapsedTime;
            if (translateTransform.Y > 450) translateTransform.Y = 450;
            if (translateTransform.Y < 0) translateTransform.Y = 0;

        }

        private void Canvas_PointerMoved_1(object sender, PointerRoutedEventArgs e)
        {
            mouseLocation = e.GetCurrentPoint(this.rectangle).Position;
        }
    }
}
