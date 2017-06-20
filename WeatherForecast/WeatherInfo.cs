using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast
{
    class WeatherInfoNext6Days
    {
        public string code;
        public DataNext6Days data;
        public string msg;
        public RC rc;
    }

    class WeatherNow
    {
        public string code;
        public DataNow data;
        public string msg;
        public RC rc;
    }

    class Forecast
    {
        public string conditionDay;
        public string conditionIdDay;
        public string conditionIdNight;
        public string conditionNight;
        public string predictDate;
        public string tempDay;
        public string tempNight;
        public string updatetime;
        public string windDirDay;
        public string windDirNight;
        public string windLevelDay;
        public string windLevelNight;
    }

    class City
    {
        public string cityId;
        public string counname;
        public string name;   //区名
        public string pname;   //城市名
    }

    class DataNext6Days
    {
        public City city;
        public List<Forecast> forecast;
    }

    class DataNow
    {
        public City city;
        public Condition condition;
    }

    class RC
    {
        public string c;
        public string p;
    }

    class Condition
    {
        public string condition;
        public string humidity;
        public int icon;
        public string temp;
        public string updatetime;
        public string windDir;
        public string windLevel;
    }

    class CityItem
    {
        public String Cityname { get; set; }
        public double lat;
        public double lon;
        public CityItem() { }
        public CityItem(double la,double lo)
        {
            lat = la;
            lon = lo;
        }
    }
}
