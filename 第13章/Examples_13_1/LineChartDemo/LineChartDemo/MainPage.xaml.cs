using System;
using System.Collections.Generic;
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

namespace LineChartDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private PointCollection GetLineChartPointCollection(List<double> datas, double topHeight, double perWidth, double topValue)
        {
            PointCollection pointCollection = new PointCollection();
            double x = 0;
            foreach (double data in datas)
            {
                double y;
                if (data > topValue) y = 0;
                else y = (topHeight - (data * topHeight) / topValue);
                Point point = new Point(x, y);
                pointCollection.Add(point);
                x += perWidth;
            }
            return pointCollection;
        }

        private PathGeometry GetLineChartPathGeometry(List<double> datas, double topHeight, double perWidth, double topValue)
        {
            PathGeometry pathGeometry = new PathGeometry();
            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            PathFigure pathFigure = new PathFigure { StartPoint = new Point(0, topHeight) };
            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
            double x = 0;
            foreach (double data in datas)
            {
                double y;
                if (data > topValue) y = 0;
                else y = (topHeight - (data * topHeight) / topValue);
                Point point = new Point(x, y);
                LineSegment lineSegment = new LineSegment { Point = point };
                pathSegmentCollection.Add(lineSegment);
                x += perWidth;
            }
            x -= perWidth;
            LineSegment lineSegmentEnd = new LineSegment { Point = new Point(x, topHeight) };
            pathSegmentCollection.Add(lineSegmentEnd);
            pathFigure.Segments = pathSegmentCollection;
            pathFigureCollection.Add(pathFigure);
            pathGeometry.Figures = pathFigureCollection;
            return pathGeometry;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            chartCanvas.Children.Clear();
            List<double> datas = new List<double> { 23, 23, 45, 26, 45, 36, 29, 30, 27, 38, 36, 52, 27, 35 };
            PointCollection pointCollection = GetLineChartPointCollection(datas, 400, 30, 100);
            Polyline polyline = new Polyline { Points = pointCollection, Stroke = new SolidColorBrush(Colors.Red) };
            chartCanvas.Children.Add(polyline);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            chartCanvas.Children.Clear();
            List<double> datas = new List<double> { 23, 23, 45, 26, 45, 36, 29, 30, 27, 38, 36, 52, 27, 35 };
            PathGeometry pathGeometry = GetLineChartPathGeometry(datas, 400, 30, 100);
            Path path = new Path { Data = pathGeometry, Fill = new SolidColorBrush(Colors.Red) };
            chartCanvas.Children.Add(path);
        }
    }
}
