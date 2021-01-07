using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace StarFlowDemo
{
    public static class StarFactory
    {
        const int MIN = 0;
        const int MAX = 2;


        static readonly Random _random = new Random();


        public static Path Create()
        {
            Point a = new Point(0, 0);
            Point b = new Point(_random.NextDouble() * 70.0 + 15.0, 0);
            Point c = new Point(0, b.X);

            int levels = _random.Next(MIN, MAX);

            List<Point> points = new List<Point>();
            points.AddRange(_RecurseSide(a, b, levels));
            points.AddRange(_RecurseSide(b, c, levels));
            points.AddRange(_RecurseSide(c, a, levels));
            Path retVal = _CreatePath(points);

            _ColorFactory(retVal);


            RotateTransform rotate = new RotateTransform();
            rotate.CenterX = 0.5;
            rotate.CenterY = 0.5;
            rotate.Angle = _random.NextDouble() * 360.0;

            retVal.SetValue(Path.RenderTransformProperty, rotate);
            return retVal;
        }

        private static List<Point> _RecurseSide(Point a, Point b, int level)
        {

            if (level == 0)
            {
                return new List<Point> { a, b };
            }
            else
            {
                List<Point> newPoints = new List<Point>();

                foreach (Point point in _RefactorPoints(a, b))
                {
                    newPoints.Add(point);
                }

                List<Point> aggregatePoints = new List<Point>();

                for (int x = 0; x < newPoints.Count; x++)
                {
                    int y = x + 1 == newPoints.Count ? 0 : x + 1;
                    aggregatePoints.AddRange(_RecurseSide(newPoints[x], newPoints[y], level - 1));
                }

                return aggregatePoints;
            }
        }

        private static IEnumerable<Point> _RefactorPoints(Point a, Point b)
        {
            yield return a;

            double dX = b.X - a.X;
            double dY = b.Y - a.Y;

            yield return new Point(a.X + dX / 3.0, a.Y + dY / 3.0);

            double factor = _random.NextDouble() - 0.5;

            double vX = (a.X + b.X) / (2.0 + factor) + Math.Sqrt(3.0 + factor) * (b.Y - a.Y) / (6.0 + factor * 2.0);
            double vY = (a.Y + b.Y) / (2.0 + factor) + Math.Sqrt(3.0 + factor) * (a.X - b.X) / (6.0 + factor * 2.0);

            yield return new Point(vX, vY);

            yield return new Point(b.X - dX / 3.0, b.Y - dY / 3.0);

            yield return b;
        }

        private static void _ColorFactory(Path input)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1.0, 1.0);
            GradientStop start = new GradientStop();
            start.Color = _GetColor();
            start.Offset = 0;
            GradientStop middle = new GradientStop();
            middle.Color = _GetColor();
            middle.Offset = _random.NextDouble();
            GradientStop end = new GradientStop();
            end.Color = _GetColor();
            end.Offset = 1.0;
            brush.GradientStops.Add(start);
            brush.GradientStops.Add(middle);
            brush.GradientStops.Add(end);
            input.Fill = brush;
        }

        private static Color _GetColor()
        {
            Color color = new Color();
            color.A = (byte)(_random.Next(200) + 20);
            color.R = (byte)(_random.Next(200) + 50);
            color.G = (byte)(_random.Next(200) + 50);
            color.B = (byte)(_random.Next(200) + 50);

            return color;
        }

        private static Path _CreatePath(List<Point> points)
        {
            PathSegmentCollection segments = new PathSegmentCollection();

            bool first = true;

            foreach (Point point in points)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    segments.Add(
                        new LineSegment
                        {
                            Point = point
                        });
                }
            }

            PathGeometry pathGeometry = new PathGeometry();

            pathGeometry.Figures.Add(
                new PathFigure
                {
                    IsClosed = true,
                    StartPoint = points[0],
                    Segments = segments
                });

            return new Path { Data = pathGeometry };
        }
    }
}
