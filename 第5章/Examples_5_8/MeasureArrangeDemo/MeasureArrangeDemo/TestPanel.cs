using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MeasureArrangeDemo
{
    public class TestPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Debug.WriteLine("进入父对象" + this.Name + "的MeasureOverride方法测量大小");
            foreach (UIElement item in this.Children)
            {
                item.Measure(new Size(120, 120));//这里是入口
                Debug.WriteLine("子对象的DesiredSize值  Width:" + item.DesiredSize.Width + " Height:" + item.DesiredSize.Height);
                Debug.WriteLine("子对象的RenderSize值  Width:" + item.RenderSize.Width + " Height:" + item.RenderSize.Height);
            }
            Debug.WriteLine("父对象的DesiredSize值  Width:" + this.DesiredSize.Width + " Height:" + this.DesiredSize.Height);
            Debug.WriteLine("父对象的RenderSize值  Width:" + this.RenderSize.Width + " Height:" + this.RenderSize.Height);
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine("进入父对象" + this.Name + "的ArrangeOverride方法进行排列");
            double x = 0;
            foreach (UIElement item in this.Children)
            {
                //排列子对象
                item.Arrange(new Rect(x, 0, item.DesiredSize.Width, item.DesiredSize.Height));
                x += item.DesiredSize.Width;
                Debug.WriteLine("子对象的DesiredSize值  Width:" + item.DesiredSize.Width + " Height:" + item.DesiredSize.Height);
                Debug.WriteLine("子对象的RenderSize值  Width:" + item.RenderSize.Width + " Height:" + item.RenderSize.Height);
            }
            Debug.WriteLine("父对象的DesiredSize值  Width:" + this.DesiredSize.Width + " Height:" + this.DesiredSize.Height);
            Debug.WriteLine("父对象的RenderSize值  Width:" + this.RenderSize.Width + " Height:" + this.RenderSize.Height);
            return finalSize;
        }
    }
}
