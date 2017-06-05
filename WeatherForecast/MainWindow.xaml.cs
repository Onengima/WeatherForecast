using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WeatherForecast
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string values="";
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mapweb_Loaded(object sender, RoutedEventArgs e)
        {
            //String sURL = AppDomain.CurrentDomain.BaseDirectory+"baiduMap.html";
            String sURL = "http://139.199.155.77:8080/WeatherForecast/baiduMap.html";
            Uri uri = new Uri(sURL);
            mapweb.Navigate(uri);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Refresh);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            values = this.mapweb.InvokeScript("getLocation", null).ToString();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            values = tbSearch.Text;
            this.mapweb.InvokeScript("SearchPlace", tbSearch.Text);
        }

        private void Refresh(object sender, EventArgs e)
        {
            try
            {
                values = this.mapweb.InvokeScript("getLocation", null).ToString();
            }
            catch (Exception) { }
            string[] valueList = values.Split(',');
            lblLocation.Content = valueList[0];
        }
    }
}
