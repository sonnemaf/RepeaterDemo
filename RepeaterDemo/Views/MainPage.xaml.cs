using RepeaterDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RepeaterDemo {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {


        private Random random = new Random();
        private int MaxLength = 400;
        private bool isHorizontal = false;

        public ObservableCollection<SampleData> SampleDataItems { get; } = new ObservableCollection<SampleData>();

        public MainPage() {
            this.InitializeComponent();
            InitializeData();
            //repeater2.ItemsSource = Enumerable.Range(0, 500);
        }

        private void InitializeData() {
            for (int i = 0; i < MaxLength; i++) {
                SampleDataItems.Add(new SampleData(random.Next(this.MaxLength), this.MaxLength));
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e) {
            SampleDataItems.Insert(0, new SampleData(random.Next(this.MaxLength), this.MaxLength));
            DeleteBtn.IsEnabled = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e) {
            if (SampleDataItems.Count > 0) {
                SampleDataItems.RemoveAt(0);
                if (SampleDataItems.Count == 0) {
                    DeleteBtn.IsEnabled = false;
                }
            }
        }

        //private void OrientationBtn_Click(object sender, RoutedEventArgs e) {
        //    string layoutKey = String.Empty, elementGeneratorKey = String.Empty, itemTemplateKey = String.Empty;

        //    if (isHorizontal) {
        //        layoutKey = "VerticalStackLayout";
        //        elementGeneratorKey = "HorizontalElementGenerator";
        //        itemTemplateKey = "HorizontalBarTemplate";
        //    } else {
        //        layoutKey = "HorizontalStackLayout";
        //        elementGeneratorKey = "VerticalElementGenerator";
        //        itemTemplateKey = "VerticalBarTemplate";
        //    }

        //    repeater.Layout = Resources[layoutKey] as Microsoft.UI.Xaml.Controls.VirtualizingLayoutBase;
        //    repeater.ViewGenerator = Resources[elementGeneratorKey] as Microsoft.UI.Xaml.Controls.ViewGenerator;
        //    repeater.ItemsSource = BarItems;

        //    layout.Text = layoutKey;
        //    elementGenerator.Text = itemTemplateKey;

        //    isHorizontal = !isHorizontal;
        //}

        //private void LayoutBtn_Click(object sender, RoutedEventArgs e) {
        //    string layoutKey = ((FrameworkElement)sender).Tag as string;

        //    repeater2.Layout = Resources[layoutKey] as Microsoft.UI.Xaml.Controls.VirtualizingLayoutBase;

        //    layout2.Text = layoutKey;
        //}

        private void RadioBtn_Click(object sender, RoutedEventArgs e) {
            string elementGeneratorKey = String.Empty, itemTemplateKey = String.Empty;
            var layoutKey = ((FrameworkElement)sender).Tag as string;

            if (layoutKey.Equals(nameof(this.VerticalStackLayout))) // we used x:Name in the resources which both acts as the x:Key value and creates a member field by the same name
            {
                elementGeneratorKey = "HorizontalElementGenerator";
            } else if (layoutKey.Equals(nameof(this.FlexLayout))) {
                elementGeneratorKey = "FlexElementGenerator";
            } else if (layoutKey.Equals(nameof(this.UniformGridLayout))) {
                elementGeneratorKey = "GridViewGenerator";
            }

            repeater.Layout = Resources[layoutKey] as Microsoft.UI.Xaml.Controls.VirtualizingLayoutBase;
            repeater.ViewGenerator = Resources[elementGeneratorKey] as Microsoft.UI.Xaml.Controls.ViewGenerator;
        }

        private void GridViewGenerator_SelectTemplateKey(Microsoft.UI.Xaml.Controls.RecyclingViewGenerator sender, Microsoft.UI.Xaml.Controls.SelectTemplateEventArgs args) {
            args.TemplateKey = ((SampleData)args.DataContext).Value > 200 ? nameof(this.EllipseItem) : nameof(this.RectangleItem);
        }
    }
}
