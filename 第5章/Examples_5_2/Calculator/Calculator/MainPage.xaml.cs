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

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string Operation = "";
        private int num1 = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void DigitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Operation == "=")
            {
                OperationResult.Text = "";
                InputInformation.Text = "";
                Operation = "";
                num1 = 0;
            }
            string s = ((Button)sender).Content.ToString();
            OperationResult.Text = OperationResult.Text + s;
            InputInformation.Text = InputInformation.Text + s;
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Operation == "=")
            {
                InputInformation.Text = OperationResult.Text;
                Operation = "";

            }
            string s = ((Button)sender).Content.ToString();
            InputInformation.Text = InputInformation.Text + s;
            OperationNum(s);
            OperationResult.Text = "";
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            OperationNum("=");
            OperationResult.Text = num1.ToString();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            OperationResult.Text = "";
            InputInformation.Text = "";
            Operation = "";
            num1 = 0;
        }

        private void OperationNum(string s)
        {
            if (OperationResult.Text != "")
            {
                switch (Operation)
                {
                    case "":
                        num1 = Int32.Parse(OperationResult.Text);
                        Operation = s;
                        break;
                    case "+":
                        num1 = num1 + Int32.Parse(OperationResult.Text);
                        Operation = s;
                        break;
                    case "-":
                        num1 = num1 - Int32.Parse(OperationResult.Text);
                        Operation = s;
                        break;
                    case "*":
                        num1 = num1 * Int32.Parse(OperationResult.Text);
                        Operation = s;
                        break;
                    case "/":
                        if (Int32.Parse(OperationResult.Text) != 0)
                            num1 = num1 / Int32.Parse(OperationResult.Text);
                        else num1 = 0;
                        Operation = s;
                        break;
                    default: break;
                }
            }
            else
            {
                Operation = s;
            }
        }
    }
}
