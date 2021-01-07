using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace MeasureArrangeDemo
{
    public class TestUIElement : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Debug.WriteLine("进入子对象" + this.Name + "的MeasureOverride方法测量大小");
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine("进入子对象" + this.Name + "的ArrangeOverride方法进行排列");
            return finalSize;
        }
    }
}
