using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace LineChartReportDemo
{
    public class ChartStyleGridlines : ChartStyle
    {

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private Canvas textCanvas;
        public Canvas TextCanvas
        {
            get { return textCanvas; }
            set { textCanvas = value; }
        }

        private bool isXGrid = true;
        public bool IsXGrid
        {
            get { return isXGrid; }
            set { isXGrid = value; }
        }

        private bool isYGrid = true;
        public bool IsYGrid
        {
            get { return isYGrid; }
            set { isYGrid = value; }
        }

        private Brush gridlineColor = new SolidColorBrush(Colors.LightGray);
        public Brush GridlineColor
        {
            get { return gridlineColor; }
            set { gridlineColor = value; }
        }

        private double xTick = 1;
        public double XTick
        {
            get { return xTick; }
            set { xTick = value; }
        }

        private double yTick = 0.5;
        public double YTick
        {
            get { return yTick; }
            set { yTick = value; }
        }

        private GridlinePatternEnum gridlinePattern;
        public GridlinePatternEnum GridlinePattern
        {
            get { return gridlinePattern; }
            set { gridlinePattern = value; }
        }
        private double leftOffset = 20;
        private double bottomOffset = 15;
        private double rightOffset = 10;
        private Line gridline = new Line();

        public ChartStyleGridlines()
        {
            title = "Title";
        }

        public void AddChartStyle(TextBlock tbTitle)
        {
            Point pt = new Point();
            Line tick = new Line();
            double offset = 0;
            double dx, dy;
            TextBlock tb = new TextBlock();

            tb.Text = Xmax.ToString();
            tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Size size = tb.DesiredSize;
            rightOffset = size.Width / 2 + 2;

            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.TextAlignment = TextAlignment.Right;
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width)
                    offset = size.Width;
            }
            leftOffset = offset + 5;

            ChartCanvas.Width = TextCanvas.Width - leftOffset - rightOffset;
            ChartCanvas.Height = TextCanvas.Height - bottomOffset - size.Height / 2;

            Canvas.SetLeft(ChartCanvas, leftOffset);
            Canvas.SetTop(ChartCanvas, bottomOffset);

            Rectangle chartRect = new Rectangle();
            chartRect.Stroke = new SolidColorBrush(Colors.Black);
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);

            if (IsYGrid == true)
            {
                for (dx = Xmin + XTick; dx < Xmax; dx += XTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(dx, Ymin)).X;
                    gridline.Y1 = NormalizePoint(new Point(dx, Ymin)).Y;
                    gridline.X2 = NormalizePoint(new Point(dx, Ymax)).X;
                    gridline.Y2 = NormalizePoint(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }

            if (IsXGrid == true)
            {
                for (dy = Ymin + YTick; dy < Ymax; dy += YTick)
                {
                    gridline = new Line();
                    AddLinePattern();
                    gridline.X1 = NormalizePoint(new Point(Xmin, dy)).X;
                    gridline.Y1 = NormalizePoint(new Point(Xmin, dy)).Y;
                    gridline.X2 = NormalizePoint(new Point(Xmax, dy)).X;
                    gridline.Y2 = NormalizePoint(new Point(Xmax, dy)).Y;
                    ChartCanvas.Children.Add(gridline);
                }
            }
            // 创建X轴
            for (dx = Xmin; dx <= Xmax; dx += xTick)
            {
                pt = NormalizePoint(new Point(dx, Ymin));
                tick = new Line();
                tick.Stroke = new SolidColorBrush(Colors.Black);
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X;
                tick.Y2 = pt.Y - 5;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dx.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, leftOffset + pt.X - size.Width / 2);
                Canvas.SetTop(tb, pt.Y + 10 + size.Height / 2);
            }

            for (dy = Ymin; dy <= Ymax; dy += YTick)
            {
                pt = NormalizePoint(new Point(Xmin, dy));
                tick = new Line();
                tick.Stroke = new SolidColorBrush(Colors.Black);
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X + 5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetLeft(tb, -30);
                Canvas.SetTop(tb, pt.Y);
            }

            tbTitle.Text = Title;
        }

        public void AddLinePattern()
        {
            gridline.Stroke = GridlineColor;
            gridline.StrokeThickness = 1;

            switch (GridlinePattern)
            {
                case GridlinePatternEnum.Dash:
                    DoubleCollection doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(3);
                    gridline.StrokeDashArray = doubleCollection;
                    break;
                case GridlinePatternEnum.Dot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    gridline.StrokeDashArray = doubleCollection;
                    break;
                case GridlinePatternEnum.DashDot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(2);
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    gridline.StrokeDashArray = doubleCollection;
                    break;
            }
        }

        public enum GridlinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4
        }
    }
}
