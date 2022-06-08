using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json.Linq;

namespace MathService1
{
    /// <summary>
    /// Сводное описание для WebMathService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]

    public class WeatherForecast
    {
        public string name { get; set; }
        public string country { get; set; }
        public string localtime { get; set; }
        public string temp_c { get; set; }

    }

    public static class SearchCity
    {
        public static string GettingListOfCitiesOnRequest(string formalCityName)
        {
            string apiKey = "34a781eb8bac4e5b9c944329223103";

            string jsonOnWeb = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={formalCityName}&aqi=no";


            WebClient webClient = new WebClient();
            string prepareString = webClient.DownloadString(jsonOnWeb);

            //ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);

            WeatherForecast weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(prepareString);



            JObject jObject = JObject.Parse(prepareString);
            JToken jLocaton = jObject["location"];
            weatherForecast.name = (string)jLocaton["name"];
            weatherForecast.country = (string)jLocaton["country"];
            weatherForecast.localtime = (string)jLocaton["localtime"];
            JToken jCurrent = jObject["current"];
            weatherForecast.temp_c = (string)jCurrent["temp_c"];


            
            return $"Страна: {weatherForecast.country}, Город: {weatherForecast.name}, Местное время: {weatherForecast.localtime}, Температура: {weatherForecast.temp_c}c";
        }
    }

    public class WebMathService1 : WebService
    {

        [WebMethod]
        public string GetWeather(string formalCityName)
        {
            return SearchCity.GettingListOfCitiesOnRequest(formalCityName);
        }

    }
}
