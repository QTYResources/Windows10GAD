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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ReadDataTemplateDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Person> Persons = new List<Person>();
        // 构造函数
        public MainPage()
        {
            this.InitializeComponent();
            Persons.Add(new Person { FirstName = "lee2", LastName = "Terry2" });
            Persons.Add(new Person { FirstName = "lee3", LastName = "Terry3" });
            Persons.Add(new Person { FirstName = "lee4", LastName = "Terry4" });
            Persons.Add(new Person { FirstName = "lee5", LastName = "Terry5" });
            listbox.ItemsSource = Persons;
        }

        private void StackPanel_Tap_1(object sender, TappedRoutedEventArgs e)
        {

            ContentPresenter myContentPresenter = (ContentPresenter)(listbox.ContainerFromItem((sender as Panel).DataContext));

            if (myContentPresenter.ContentTemplate.Equals(dataTemplateSelectName))
            {
                myContentPresenter.ContentTemplate = dataTemplateNoSelectName;
            }
            else
            {
                myContentPresenter.ContentTemplate = dataTemplateSelectName;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }

    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
