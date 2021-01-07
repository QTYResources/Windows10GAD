using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace PieChartDemo
{
    class PiePiece : Path
    {
        #region

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("RadiusProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty PushOutProperty =
            DependencyProperty.Register("PushOutProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double PushOut
        {
            get { return (double)GetValue(PushOutProperty); }
            set { SetValue(PushOutProperty, value); }
        }

        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register("InnerRadiusProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        public static readonly DependencyProperty WedgeAngleProperty =
            DependencyProperty.Register("WedgeAngleProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double WedgeAngle
        {
            get { return (double)GetValue(WedgeAngleProperty); }
            set
            {
                SetValue(WedgeAngleProperty, value);
                this.Percentage = (value / 360.0);
            }
        }

        public static readonly DependencyProperty RotationAngleProperty =
            DependencyProperty.Register("RotationAngleProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double RotationAngle
        {
            get { return (double)GetValue(RotationAngleProperty); }
            set { SetValue(RotationAngleProperty, value); }
        }

        public static readonly DependencyProperty CentreXProperty =
            DependencyProperty.Register("CentreXProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double CentreX
        {
            get { return (double)GetValue(CentreXProperty); }
            set { SetValue(CentreXProperty, value); }
        }

        public static readonly DependencyProperty CentreYProperty =
            DependencyProperty.Register("CentreYProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double CentreY
        {
            get { return (double)GetValue(CentreYProperty); }
            set { SetValue(CentreYProperty, value); }
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("PercentageProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            private set { SetValue(PercentageProperty, value); }
        }

        public static readonly DependencyProperty PieceValueProperty =
            DependencyProperty.Register("PieceValueProperty", typeof(double), typeof(PiePiece),
            new PropertyMetadata(0.0));

        public double PieceValue
        {
            get { return (double)GetValue(PieceValueProperty); }
            set { SetValue(PieceValueProperty, value); }
        }


        #endregion

        public PiePiece()
        {
            CreatePathData(0, 0);
        }

        private double lastWidth = 0;
        private double lastHeight = 0;
        private PathFigure figure;

        private void AddPoint(double x, double y)
        {
            LineSegment segment = new LineSegment();
            segment.Point = new Point(x + 0.5 * StrokeThickness,
                y + 0.5 * StrokeThickness);
            figure.Segments.Add(segment);
        }

        private void AddLine(Point point)
        {
            LineSegment segment = new LineSegment();
            segment.Point = point;
            figure.Segments.Add(segment);
        }

        private void AddArc(Point point, Size size, bool largeArc, SweepDirection sweepDirection)
        {
            ArcSegment segment = new ArcSegment();
            segment.Point = point;
            segment.Size = size;
            segment.IsLargeArc = largeArc;
            segment.SweepDirection = sweepDirection;
            figure.Segments.Add(segment);
        }

        private void CreatePathData(double width, double height)
        {
            if (lastWidth == width && lastHeight == height) return;
            lastWidth = width;
            lastHeight = height;

            Point startPoint = new Point(CentreX, CentreY);
            Point innerArcStartPoint = ComputeCartesianCoordinate(RotationAngle, InnerRadius);
            innerArcStartPoint = Offset(innerArcStartPoint, CentreX, CentreY);
            Point innerArcEndPoint = ComputeCartesianCoordinate(RotationAngle + WedgeAngle, InnerRadius);
            innerArcEndPoint = Offset(innerArcEndPoint, CentreX, CentreY);
            Point outerArcStartPoint = ComputeCartesianCoordinate(RotationAngle, Radius);
            outerArcStartPoint = Offset(outerArcStartPoint, CentreX, CentreY);
            Point outerArcEndPoint = ComputeCartesianCoordinate(RotationAngle + WedgeAngle, Radius);
            outerArcEndPoint = Offset(outerArcEndPoint, CentreX, CentreY);
            bool largeArc = WedgeAngle > 180.0;
            if (PushOut > 0)
            {
                Point offset = ComputeCartesianCoordinate(RotationAngle + WedgeAngle / 2, PushOut);
                innerArcStartPoint = Offset(innerArcStartPoint, offset.X, offset.Y);
                innerArcEndPoint = Offset(innerArcEndPoint, offset.X, offset.Y);
                outerArcStartPoint = Offset(outerArcStartPoint, offset.X, offset.Y);
                outerArcEndPoint = Offset(outerArcEndPoint, offset.X, offset.Y);
            }
            Size outerArcSize = new Size(Radius, Radius);
            Size innerArcSize = new Size(InnerRadius, InnerRadius);

            var geometry = new PathGeometry();
            figure = new PathFigure();
            figure.StartPoint = innerArcStartPoint;
            AddLine(outerArcStartPoint);
            AddArc(outerArcEndPoint, outerArcSize, largeArc, SweepDirection.Clockwise);
            AddLine(innerArcEndPoint);
            AddArc(innerArcStartPoint, innerArcSize, largeArc, SweepDirection.Counterclockwise);
            figure.IsClosed = true;
            geometry.Figures.Add(figure);
            this.Data = geometry;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            CreatePathData(finalSize.Width, finalSize.Height);
            return finalSize;
        }

        private Point Offset(Point point, double offsetX, double offsetY)
        {
            point.X += offsetX;
            point.Y += offsetY;
            return point;
        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            double angleRad = (Math.PI / 180.0) * (angle - 90);
            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);
            return new Point(x, y);
        }
    }
}
