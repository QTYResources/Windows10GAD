using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace LineChartReportDemo
{
    public class ChartStyle
    {
        private double xmin = 0;
        public double Xmin
        {
            get { return xmin; }
            set { xmin = value; }
        }

        private double xmax = 1;
        public double Xmax
        {
            get { return xmax; }
            set { xmax = value; }
        }

        private double ymin = 0;
        public double Ymin
        {
            get { return ymin; }
            set { ymin = value; }
        }

        private double ymax = 1;
        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        private Canvas chartCanvas;
        public Canvas ChartCanvas
        {
            get { return chartCanvas; }
            set { chartCanvas = value; }
        }

        public ChartStyle()
        {
        }

        public void ResizeCanvas(double width, double height)
        {
            ChartCanvas.Width = width;
            ChartCanvas.Height = height;
        }

        public Point NormalizePoint(Point pt)
        {
            if (ChartCanvas.Width.ToString() == "NaN")
                ChartCanvas.Width = 400;
            if (ChartCanvas.Height.ToString() == "NaN")
                ChartCanvas.Height = 400;
            Point result = new Point();
            result.X = (pt.X - Xmin) * ChartCanvas.Width / (Xmax - Xmin);
            result.Y = ChartCanvas.Height - (pt.Y - Ymin) * ChartCanvas.Height / (Ymax - Ymin);
            return result;
        }
    }
}
