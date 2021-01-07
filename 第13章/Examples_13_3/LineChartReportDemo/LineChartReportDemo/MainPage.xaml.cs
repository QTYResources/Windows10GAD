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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LineChartReportDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ChartStyleGridlines cs;

        private Legend lg = new Legend();

        private DataCollection dc = new DataCollection();

        private DataSeries ds = new DataSeries();

        public MainPage()
        {
            InitializeComponent();
            AddChart();
        }

        private void AddChart()
        {
            cs = new ChartStyleGridlines();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.Title = "Sine and Cosine Chart";
            cs.Xmin = 0;
            cs.Xmax = 7;
            cs.Ymin = -1.5;
            cs.Ymax = 1.5;
            cs.YTick = 0.5;
            cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
            cs.GridlineColor = new SolidColorBrush(Colors.Black);
            cs.AddChartStyle(tbTitle);

            ds.LineColor = new SolidColorBrush(Colors.Blue);
            ds.LineThickness = 1;
            ds.SeriesName = "Sine";
            for (int i = 0; i < 36; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            ds = new DataSeries();
            ds.LineColor = new SolidColorBrush(Colors.Red);
            ds.SeriesName = "Cosine";
            ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
            ds.LineThickness = 2;
            for (int i = 0; i < 36; i++)
            {
                double x = i / 5.0;
                double y = Math.Cos(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            ds = new DataSeries();
            ds.LineColor = new SolidColorBrush(Colors.Green);
            ds.SeriesName = "Sine^2";
            ds.LinePattern = DataSeries.LinePatternEnum.Dot;
            ds.LineThickness = 2;
            for (int i = 0; i < 36; i++)
            {
                double x = i / 5.0;
                double y = Math.Sin(x) * Math.Sin(x);
                ds.LineSeries.Points.Add(new Point(x, y));
            }
            dc.DataList.Add(ds);

            dc.AddLines(chartCanvas, cs);
            lg.LegendCanvas = legendCanvas;
            lg.IsLegend = true;
            lg.IsBorder = true;
            lg.AddLegend(cs, dc);
        }
    }
}
