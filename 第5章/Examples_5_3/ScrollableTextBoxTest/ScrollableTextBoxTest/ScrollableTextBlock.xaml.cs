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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ScrollableTextBoxTest
{
    public sealed partial class ScrollableTextBlock : UserControl
    {
        private string text = "";
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                ParseText(text);
            }
        }

        public ScrollableTextBlock()
        {
            this.InitializeComponent();
        }

        private void ParseText(string value)
        {
            string[] textBlockTexts = value.Split(' ');

            this.stackPanel.Children.Clear();

            for (int i = 0; i < textBlockTexts.Length; i++)
            {
                TextBlock textBlock = this.GetTextBlock();
                textBlock.Text = textBlockTexts[i].ToString();
                this.stackPanel.Children.Add(textBlock);
            }
        }


        private TextBlock GetTextBlock()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.FontSize = this.FontSize;
            textBlock.FontFamily = this.FontFamily;
            textBlock.FontWeight = this.FontWeight;
            textBlock.Foreground = this.Foreground;
            textBlock.Margin = new Thickness(0, 10, 0, 0);
            return textBlock;
        }
    }
}
