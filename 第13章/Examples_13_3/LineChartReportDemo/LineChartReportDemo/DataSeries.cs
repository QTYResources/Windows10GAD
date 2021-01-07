using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace LineChartReportDemo
{
    public class DataSeries
    {

        private Polyline lineSeries = new Polyline();
        public Polyline LineSeries
        {
            get { return lineSeries; }
            set { lineSeries = value; }
        }

        private Brush lineColor;
        public Brush LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        private double lineThickness = 1;
        public double LineThickness
        {
            get { return lineThickness; }
            set { lineThickness = value; }
        }

        private LinePatternEnum linePattern;
        public LinePatternEnum LinePattern
        {
            get { return linePattern; }
            set { linePattern = value; }
        }

        private string seriesName = "Default Name";
        public string SeriesName
        {
            get { return seriesName; }
            set { seriesName = value; }
        }

        public DataSeries()
        {
            LineColor = new SolidColorBrush(Colors.Black);
        }

        public void AddLinePattern()
        {
            LineSeries.Stroke = LineColor;
            LineSeries.StrokeThickness = LineThickness;

            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    DoubleCollection doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(3);
                    LineSeries.StrokeDashArray = doubleCollection;
                    break;
                case LinePatternEnum.Dot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    LineSeries.StrokeDashArray = doubleCollection;
                    break;
                case LinePatternEnum.DashDot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(2);
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    LineSeries.StrokeDashArray = doubleCollection;
                    break;
                case LinePatternEnum.None:
                    LineSeries.Stroke = new SolidColorBrush(Colors.Transparent);
                    break;
            }
        }

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }
}
