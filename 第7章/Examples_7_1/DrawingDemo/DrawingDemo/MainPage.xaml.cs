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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrawingDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Point currentPoint;
        private Point oldPoint;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.ContentPanelCanvas.PointerMoved += ContentPanelCanvas_PointerMoved;
            this.ContentPanelCanvas.PointerPressed += ContentPanelCanvas_PointerPressed;
        }

        void ContentPanelCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            currentPoint = e.GetCurrentPoint(ContentPanelCanvas).Position;
            oldPoint = currentPoint;
        }

        void ContentPanelCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            currentPoint = e.GetCurrentPoint(ContentPanelCanvas).Position;

            Line line = new Line() { X1 = currentPoint.X, Y1 = currentPoint.Y, X2 = oldPoint.X, Y2 = oldPoint.Y };

            line.Stroke = new SolidColorBrush(Colors.Red);
            line.StrokeThickness = 5;
            line.StrokeLineJoin = PenLineJoin.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;
            this.ContentPanelCanvas.Children.Add(line);
            oldPoint = currentPoint;
        }
    }
}
