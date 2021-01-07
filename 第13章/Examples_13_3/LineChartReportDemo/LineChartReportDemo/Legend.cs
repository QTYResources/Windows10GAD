using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace LineChartReportDemo
{
    public class Legend
    {
        private bool isLegend;
        public bool IsLegend
        {
            get { return isLegend; }
            set { isLegend = value; }
        }


        private bool isBorder;
        public bool IsBorder
        {
            get { return isBorder; }
            set { isBorder = value; }
        }

        private Canvas legendCanvas;
        public Canvas LegendCanvas
        {
            get { return legendCanvas; }
            set { legendCanvas = value; }
        }

        public Legend()
        {
            isLegend = false;
            isBorder = true;
        }

        public void AddLegend(ChartStyleGridlines cs, DataCollection dc)
        {
            TextBlock tb = new TextBlock();
            if (dc.DataList.Count < 1 || !IsLegend)
                return;
            int n = 0;

            string[] legendLabels = new string[dc.DataList.Count];
            foreach (DataSeries ds in dc.DataList)
            {
                legendLabels[n] = ds.SeriesName;
                n++;
            }

            double legendWidth = 0;
            Size size = new Size(0, 0);

            for (int i = 0; i < legendLabels.Length; i++)
            {
                tb = new TextBlock();
                tb.Text = legendLabels[i];
                tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                size = tb.DesiredSize;
                if (legendWidth < size.Width)
                    legendWidth = size.Width;
            }

            legendWidth += 80;
            legendCanvas.Width = legendWidth + 5;
            double legendHeight = 30 * dc.DataList.Count;
            double sx = 6;
            double sy = 15;
            double textHeight = size.Height;
            double lineLength = 34;

            Rectangle legendRect = new Rectangle();
            legendRect.Stroke = new SolidColorBrush(Colors.Black);
            legendRect.Width = legendWidth;
            legendRect.Height = legendHeight;

            if (IsLegend && IsBorder)
                LegendCanvas.Children.Add(legendRect);

            n = 1;

            foreach (DataSeries ds in dc.DataList)
            {
                double xSymbol = sx + lineLength / 2;
                double xText = 2 * sx + lineLength;
                double yText = n * sy + (2 * n - 1) * textHeight / 2;
                Line line = new Line();
                AddLinePattern(line, ds);
                line.X1 = sx;
                line.Y1 = yText;
                line.X2 = sx + lineLength;
                line.Y2 = yText;
                LegendCanvas.Children.Add(line);

                tb = new TextBlock();
                tb.FontSize = 15;
                tb.Text = ds.SeriesName;
                LegendCanvas.Children.Add(tb);
                Canvas.SetTop(tb, yText - 15);
                Canvas.SetLeft(tb, xText + 10);
                n++;
            }
        }

        private void AddLinePattern(Line line, DataSeries ds)
        {
            line.Stroke = ds.LineColor;
            line.StrokeThickness = ds.LineThickness;

            switch (ds.LinePattern)
            {
                case DataSeries.LinePatternEnum.Dash:
                    DoubleCollection doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(3);
                    line.StrokeDashArray = doubleCollection;
                    break;
                case DataSeries.LinePatternEnum.Dot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    line.StrokeDashArray = doubleCollection;
                    break;
                case DataSeries.LinePatternEnum.DashDot:
                    doubleCollection = new DoubleCollection();
                    doubleCollection.Add(4);
                    doubleCollection.Add(2);
                    doubleCollection.Add(1);
                    doubleCollection.Add(2);
                    line.StrokeDashArray = doubleCollection;
                    break;
                case DataSeries.LinePatternEnum.None:
                    line.Stroke = new SolidColorBrush(Colors.Transparent);
                    break;
            }
        }
    }
}
