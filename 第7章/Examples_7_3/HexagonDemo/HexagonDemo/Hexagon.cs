using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace HexagonDemo
{
    public class Hexagon : Path
    {
        private double lastWidth = 0;
        private double lastHeight = 0;
        private PathFigure figure;

        public Hexagon()
        {
            CreateDataPath(0, 0);
        }

        private void CreateDataPath(double width, double height)
        {
            height -= this.StrokeThickness;
            width -= this.StrokeThickness;

            if (lastWidth == width && lastHeight == height)
                return;

            lastWidth = width;
            lastHeight = height;

            PathGeometry geometry = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = new Point(0.25 * width, 0);
            AddPoint(0.75 * width, 0);
            AddPoint(width, 0.5 * height);
            AddPoint(0.75 * width, height);
            AddPoint(0.25 * width, height);
            AddPoint(0, 0.5 * height);
            figure.IsClosed = true;
            geometry.Figures.Add(figure);
            this.Data = geometry;
        }

        private void AddPoint(double x, double y)
        {
            LineSegment segment = new LineSegment();
            segment.Point = new Point(x + 0.5 * StrokeThickness,
                y + 0.5 * StrokeThickness);
            figure.Segments.Add(segment);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            CreateDataPath(finalSize.Width, finalSize.Height);
            return finalSize;
        }
    }
}
