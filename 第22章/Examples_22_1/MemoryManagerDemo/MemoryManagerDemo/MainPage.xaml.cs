using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
// 在C#的项目里面引入C++项目的命名空间
using MemoryManagerRT;

namespace MemoryManagerDemo
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // 通过Windows运行时封装的C++类在C#中使用就和C#的类的使用完全一样
            tbMemoryUsed.Text = CMemoryManagerRT.GetProcessCommittedBytes().ToString() + "字节";
            tbMaxMemoryUsed.Text = CMemoryManagerRT.GetProcessCommittedLimit().ToString() + "字节";
        }
    }
}
