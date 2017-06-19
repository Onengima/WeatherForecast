using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherForecast
{
    class WeatherInfoNext6Days
    {
        public int code;
        public DataNext6Days data;
        public string msg;
        public RC rc;
    }

    class WeatherNow
    {
        public int code;
        public DataNow data;
        public string msg;
        public RC rc;
    }

    class Forecast
    {
        public string conditionDay;
        public int conditionIdDay;
        public int conditionIdNight;
        public string conditionNight;
        public string predictDate;
        public int tempDay;
        public int tempNight;
        public string updatetime;
        public string windDirDay;
        public string windDirNight;
        public int windLevelDay;
        public int windLevelNight;
    }

    class City
    {
        public int cityId;
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
        public int c;
        public string p;
    }

    class Condition
    {
        public string condition;
        public int humidity;
        public int icon;
        public int temp;
        public string updatetime;
        public string windDir;
        public int windLevel;
    }
}
