using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BindingDataDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Food> AllFood { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            AllFood = new List<Food>();
            Food item0 = new Food() { Name = "西红柿", IconUri = "Images/Tomato.png", Type = "Healthy", Description = "西红柿的味道不错。" };
            Food item1 = new Food() { Name = "茄子", IconUri = "Images/Beer.png", Type = "NotDetermined", Description = "不知道这个是否好吃。" };
            Food item2 = new Food() { Name = "火腿", IconUri = "Images/fries.png", Type = "Unhealthy", Description = "这是不健康的食品。" };
            Food item3 = new Food() { Name = "三明治", IconUri = "Images/Hamburger.png", Type = "Unhealthy", Description = "肯德基的好吃？" };
            Food item4 = new Food() { Name = "冰激凌", IconUri = "Images/icecream.png", Type = "Healthy", Description = "给小朋友吃的。" };
            Food item5 = new Food() { Name = "Pizza", IconUri = "Images/Pizza.png", Type = "Unhealthy", Description = "这个非常不错。" };
            Food item6 = new Food() { Name = "辣椒", IconUri = "Images/Pepper.png", Type = "Healthy", Description = "我不喜欢吃这东西。" };
            AllFood.Add(item0);
            AllFood.Add(item1);
            AllFood.Add(item2);
            AllFood.Add(item3);
            AllFood.Add(item4);
            AllFood.Add(item5);
            AllFood.Add(item6);
            listBox.ItemsSource = AllFood;
        }

    }
}
