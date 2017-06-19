using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;

namespace WeatherForecast
{
    /// <summary>
    /// WeatherShow.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherShow : Window
    {
        private String host = "http://aliv1.data.moji.com";
        private String path = "/whapi/json/aliweather/briefcondition";
        private String method = "POST";
        private const String appcode = "70bafebf87c141c782a3905ae73569e7";

        double lat = 39.91488908;
        double lon = 116.40387397;
        string token = "a231972c3e7ba6b33d8ec71fd4774f5e";
        String bodys;

        int[] imageNum ={ 0, 1, 2, 3, 4, 8, 6, 7, 7, 7, 4, 10, 10, 8, 8, 8, 8, 8, 9, 6, 9, 9, 6 };

        WeatherNow weather;


        public WeatherShow()
        {
            InitializeComponent();
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            String jsonData = GetJsonData("http://aliv1.data.moji.com/whapi/json/aliweather/briefcondition", lat, lon, token);
            weather = JsonConvert.DeserializeObject<WeatherNow>(jsonData);

            string imgPath = "";
            try
            {
                imgPath = "pack://siteoforigin:,,,/Resources/" + imageNum[weather.data.condition.icon] + ".png";
            }
            catch (Exception)
            {
                int icon = 0;
                switch (weather.data.condition.icon)
                {
                    case 30:icon = 10;break;
                    case 31:icon = 1;break;
                    case 32:icon = 9;break;
                    case 33:icon = 3;break;
                    case 34:icon = 8;break;
                    case 35:icon = 12;break;
                    case 36:icon = 12;break;
                    case 45:icon = 9;break;
                    case 46:icon = 9;break;
                    default:icon = 0;break;
                }
                imgPath = "pack://siteoforigin:,,,/Resources/" + icon + ".png";
            }

            imgMainShow.Source = new BitmapImage(new Uri(imgPath));
            lblMainShow.Content = weather.data.condition.condition;
            lblTmpShow.Content = weather.data.condition.temp+ "℃";
            lblCityShow.Content = weather.data.city.pname;
            lblHumidityShow.Content = "人体舒适度："+weather.data.condition.humidity;
            lblWindDirShow.Content = "风向：" + weather.data.condition.windDir;
            lblWindLevelShow.Content = "风速：" + weather.data.condition.windLevel;
            lblUpdateTimeShow.Content = "更新时间" + weather.data.condition.updatetime;

            jsonData = GetJsonData("http://aliv1.data.moji.com/whapi/json/aliweather/briefforecast6days", lat, lon, "0f9d7e535dfbfad15b8fd2a84fee3e36");

            WeatherInfoNext6Days weatherNext= JsonConvert.DeserializeObject<WeatherInfoNext6Days>(jsonData);
            ForecastShow(lblNext1, weatherNext, 0);
            ForecastShow(lblNext2, weatherNext, 1);
            ForecastShow(lblNext3, weatherNext, 2);
            ForecastShow(lblNext4, weatherNext, 3);
            ForecastShow(lblNext5, weatherNext, 4);
            ForecastShow(lblNext6, weatherNext, 5);

            lblName.Content = "日期：\n日间天气：\n日间温度：\n日间风向：\n日间风速：\n夜间天气：\n夜间温度：\n夜间风向：\n夜间风速：";

        }

        void ForecastShow(Label lbl,WeatherInfoNext6Days weather,int i)
        {
            lbl.Content = ""+weather.data.forecast[i].predictDate+"\n";
            lbl.Content += "" + weather.data.forecast[i].conditionDay+"\n";
            lbl.Content += "" + weather.data.forecast[i].tempDay + "\n";
            lbl.Content += "" + weather.data.forecast[i].windDirDay + "\n";
            lbl.Content += "" + weather.data.forecast[i].windLevelDay + "\n";
            lbl.Content += "" + weather.data.forecast[i].conditionNight + "\n";
            lbl.Content += "" + weather.data.forecast[i].tempDay + "\n";
            lbl.Content += "" + weather.data.forecast[i].windDirNight + "\n";
            lbl.Content += "" + weather.data.forecast[i].windLevelNight + "\n";
        }

        String GetJsonData(String url,double lat,double lon,String token)
        {
            bodys = "lat="+lat + "&" +"lon="+ lon + "&"+ "token="+ token;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }
    }
}
