using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace LineChartReportDemo
{
    public class DataCollection
    {

        private List<DataSeries> dataList;
        public List<DataSeries> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        public DataCollection()
        {
            dataList = new List<DataSeries>();
        }

        public void AddLines(Canvas canvas, ChartStyle cs)
        {
            int j = 0;
            foreach (DataSeries ds in DataList)
            {
                if (ds.SeriesName == "Default Name")
                {
                    ds.SeriesName = "DataSeries" + j.ToString();
                }
                ds.AddLinePattern();
                for (int i = 0; i < ds.LineSeries.Points.Count; i++)
                {
                    ds.LineSeries.Points[i] = cs.NormalizePoint(ds.LineSeries.Points[i]);
                }
                canvas.Children.Add(ds.LineSeries);
                j++;
            }
        }
    }
}
