using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace Marvin_OS
{
    class Weather
    {
        //weather
        private const string API_KEY = "f65268dddf50ca94e95bedb00e196013";

        // Query URLs. Replace @LOC@ with the location.
        private const string CurrentUrl =
            "http://api.openweathermap.org/data/2.5/weather?" +
            "q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;
        private const string ForecastUrl =
            "http://api.openweathermap.org/data/2.5/forecast?" +
            "q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;

        public string GetWeather(string town, string type, string day)
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return ("I need an internet connection to get weather information");
            }
            day = day.Replace(" ", "");
            //order days
            string[] days = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"};
            string[] myDays = new string[4];
            int temp = Array.IndexOf(days, DateTime.Now.DayOfWeek.ToString().ToLower()) + 1;
            if(temp > 6)
            {
                temp = 0;
            }
            myDays[0] = days[temp];
            for(int i = 1; i < 4; i++)
            {
                //loop 4 times
                temp++;
                if (temp > 6)
                {
                    temp = 0;
                }
                myDays[i] = days[temp];
            }
            if (day.Contains("tom"))
            {
                day = myDays[0];
            }
            if (!myDays.Contains(day) && day != "")
            {
                return ("Sorry I only have a 5 day forcast");
            }            
            //myDays now contains the next 5 days from now

            int[] starts = { 0, 8, 17, 26, 35 };
            int[] ends = { 7, 16, 25, 34, 43 };

            string url = "";
            if (day != "")
            {
                url = ForecastUrl.Replace("@LOC@", town);
            }
            else
            {
                url = CurrentUrl.Replace("@LOC@", town);
            }
            // Create a web client
            WebClient client = new WebClient();
            // Get the response string from the URL
            string xml;
            try
            {
                xml = client.DownloadString(url);
            }
            catch
            {
                return ("I can't get weather from that city");
            }

            // Load the response into an XML document
            XmlDocument xml_document = new XmlDocument();
            xml_document.LoadXml(xml);
            XmlDocument forcast_xml_document = new XmlDocument();
            if(type == "rainNext")
            {
                xml = client.DownloadString(ForecastUrl.Replace("@LOC@", town));
                forcast_xml_document.LoadXml(xml);
            }

            // Format the XML
            StringWriter string_writer = new StringWriter();
            XmlTextWriter xml_text_writer = new XmlTextWriter(string_writer);
            xml_text_writer.Formatting = Formatting.Indented;
            xml_document.WriteTo(xml_text_writer);
            if(type == "rainNext")
            {
                forcast_xml_document.WriteTo(xml_text_writer);
            }
            // Return the result

            if (type == "temp")
            {
                if (day == "")
                {
                    XmlNode MyNode = xml_document.SelectSingleNode("current/temperature");
                    string tempResponse = "It is currently " + Math.Round(Convert.ToDouble(MyNode.Attributes["value"].Value), 1).ToString() + " degrees";
                    return (tempResponse);
                }
                else
                {
                    XmlNodeList tempNodes = xml_document.GetElementsByTagName("temperature");
                    double[] highs = new double[9];
                    double[] lows = new double[9];
                    temp = 0;
                    for (int i = starts[Array.IndexOf(myDays, day)]; i <= ends[Array.IndexOf(myDays, day)]; i++)
                    {
                        highs[temp] = Convert.ToDouble(tempNodes[i].Attributes["max"].Value);
                        lows[temp] = Convert.ToDouble(tempNodes[i].Attributes["min"].Value);
                        temp++;
                    }
                    string tempResponse = "On " + day + " there will be a high of " + Math.Round(highs.Max(), 1).ToString() + " degrees and a low of " + Math.Round(lows.Min(), 1).ToString() + " degrees";
                    return (tempResponse);
                }
            }
            else if (type == "condition")
            {
                if (day == "")
                {
                    XmlNode tempNode = xml_document.SelectSingleNode("current/temperature");
                    XmlNode condNode = xml_document.SelectSingleNode("current/weather");
                    XmlNode windNode = xml_document.SelectSingleNode("current/wind/speed");
                    return ("It is currently " + Math.Round(Convert.ToDouble(tempNode.Attributes["value"].Value), 1).ToString() + " degrees and there are " + condNode.Attributes["value"].Value + " with a " + windNode.Attributes["name"].Value);
                }
                else
                {
                    XmlNodeList tempNodes = xml_document.GetElementsByTagName("temperature");
                    double[] highs = new double[9];
                    double[] lows = new double[9];
                    temp = 0;
                    for (int i = starts[Array.IndexOf(myDays, day)]; i <= ends[Array.IndexOf(myDays, day)]; i++)
                    {
                        highs[temp] = Convert.ToDouble(tempNodes[i].Attributes["max"].Value);
                        lows[temp] = Convert.ToDouble(tempNodes[i].Attributes["min"].Value);
                        temp++;
                    }
                    tempNodes = xml_document.GetElementsByTagName("symbol");
                    XmlNode condNode = tempNodes[Array.IndexOf(myDays, day) * 5];
                    tempNodes = xml_document.GetElementsByTagName("windSpeed");
                    XmlNode windNode = tempNodes[Array.IndexOf(myDays, day) * 5];
                    string tempResponse = "On " + day + " there will be " + condNode.Attributes["name"].Value + " with a " + windNode.Attributes["name"].Value + " and with a high of " + Math.Round(highs.Max(), 1).ToString() + " degrees and a low of " + Math.Round(lows.Min(), 1).ToString() + " degrees";
                    return (tempResponse);
                }
            }
            else if (type == "humidity")
            {
                if (day == "")
                {
                    XmlNode humidNode = xml_document.SelectSingleNode("current/humidity");
                    return ("There is " + humidNode.Attributes["value"].Value + "% humidity");
                }
                else
                {
                    XmlNodeList tempNodes = xml_document.GetElementsByTagName("humidity");
                    double[] humidity = new double[9];
                    temp = 0;
                    for (int i = starts[Array.IndexOf(myDays, day)]; i <= ends[Array.IndexOf(myDays, day)]; i++)
                    {
                        humidity[temp] = Convert.ToDouble(tempNodes[i].Attributes["value"].Value);
                        temp++;
                    }
                    double avg = Math.Round(humidity.Sum() / humidity.Length, 1);
                    if (day == myDays[0])
                    {
                        return ("There will be an average of " + avg + "% humidity tomorrow");
                    }
                    else
                    {
                        return ("There will be an average of " + avg + "% humidity on " + day);
                    }
                }
            }
            else if (type == "sunrise")
            {
                if (day == "")
                {
                    XmlNode riseNode = xml_document.SelectSingleNode("current/city/sun");
                    string time = riseNode.Attributes["rise"].Value.Remove(0, 12);
                    time = time.Remove(4, 3);
                    return ("The sun rises at " + time + " AM today");
                }
                else if (day == myDays[0])
                {
                    XmlNodeList riseNodes = xml_document.GetElementsByTagName("sun");
                    XmlNode riseNode = riseNodes[Array.IndexOf(myDays, day)];
                    string time = riseNode.Attributes["rise"].Value.Remove(0, 12);
                    time = time.Remove(4, 3);
                    return ("The sun rises at " + time + " AM on " + day);
                }
                else
                {
                    return ("Sorry I can't predict sun rises beyond 1 day");
                }
            }
            else if (type == "sunset")
            {
                if (day == "")
                {
                    XmlNode riseNode = xml_document.SelectSingleNode("current/city/sun");
                    string time = riseNode.Attributes["set"].Value.Remove(0, 11);
                    if (time[0] == '0')
                    {
                        time = ("2" + time.Remove(0, 1));
                    }
                    int numTime = Convert.ToInt32(time.Remove(2, 6));
                    if (numTime > 12)
                    {
                        numTime -= 12;
                    }
                    time = time.Remove(5, 3);
                    time = numTime.ToString() + time.Remove(0, 2);
                    return ("The sun sets at " + time + " PM today");
                }
                else if (day == myDays[0])
                {
                    XmlNodeList riseNodes = xml_document.GetElementsByTagName("sun");
                    XmlNode riseNode = riseNodes[Array.IndexOf(myDays, day)];
                    string time = riseNode.Attributes["set"].Value.Remove(0, 11);
                    if (time[0] == '0')
                    {
                        time = ("2" + time.Remove(0, 1));
                    }
                    int numTime = Convert.ToInt32(time.Remove(2, 6));
                    if (numTime > 12)
                    {
                        numTime -= 12;
                    }
                    time = time.Remove(5, 3);
                    time = numTime.ToString() + time.Remove(0, 2);
                    return ("The sun will set at " + time + " PM tomorrow");
                }
                else
                {
                    return ("Sorry I can't predict sun sets beyond 1 day");
                }
            }
            else if (type == "rain")
            {
                if (day == "")
                {
                    XmlNode rainNode = xml_document.SelectSingleNode("current/precipitation");
                    if (rainNode.Attributes["mode"].Value == "no")
                    {
                        return ("No rain is expected today");
                    }
                    else
                    {
                        return (rainNode.Attributes["value"].Value + " milimeters of " + rainNode.Attributes["mode"].Value + " is expected today");
                    }
                }
                else
                {
                    XmlNodeList rainNodes = xml_document.GetElementsByTagName("precipitation");
                    string[] rain = new string[8];
                    string[] times = { "12 AM", "3 AM", "6 AM", "9 AM", "12 PM", "3 PM", "6 PM", "9 PM", "12AM" };
                    int start = 0;
                    int end = 0;
                    bool isRain = false;
                    int tempNum = 0;
                    for (int i = starts[Array.IndexOf(myDays, day)]; i < ends[Array.IndexOf(myDays, day)] - 1; i++)
                    {
                        if (rainNodes[i].Attributes["value"] != null)
                        {
                            rain[tempNum] = rainNodes[i].Attributes["value"].Value;
                            if (rain[tempNum] != "0")
                            {
                                if (start == 0)
                                {
                                    start = tempNum;
                                }
                                end = tempNum + 1;
                                isRain = true;
                            }
                        }
                        else
                        {
                            rain[tempNum] = "0";
                        }
                        tempNum++;
                    }
                    if (isRain)
                    {
                        return ("Rain is expected " + day + " from " + times[start] + " to " + times[end]);
                    }
                    else
                    {
                        return ("No rain is expected on " + day);
                    }
                }
            }
            else if (type == "rainNext")
            {
                XmlNode rainNode = xml_document.SelectSingleNode("current/precipitation");
                if (rainNode.Attributes["mode"].Value != "no")
                {
                    return (rainNode.Attributes["mode"].Value + " is expected today");
                }
                else
                {
                    //no rain today
                    XmlNodeList rainNodes = forcast_xml_document.GetElementsByTagName("precipitation");
                    string[] rain = new string[8];
                    string[] times = { "12 AM", "3 AM", "6 AM", "9 AM", "12 PM", "3 PM", "6 PM", "9 PM", "12AM" };
                    int start = 0;
                    int end = 0;
                    bool isRain = false;
                    int tempNum = 0;
                    string tempDay = "";
                    for (int dayNum = 0; dayNum <= 4; dayNum++)
                    {
                        tempDay = myDays[dayNum];
                        for (int i = starts[Array.IndexOf(myDays, tempDay)]; i < ends[Array.IndexOf(myDays, tempDay)] - 2; i++)
                        {
                            if (rainNodes[i].Attributes["value"] != null)
                            {
                                rain[tempNum] = rainNodes[i].Attributes["value"].Value;
                                if (rain[tempNum] != "0")
                                {
                                    if (start == 0)
                                    {
                                        start = tempNum;
                                    }
                                    end = tempNum + 1;
                                    isRain = true;
                                }
                            }
                            else
                            {
                                rain[tempNum] = "0";
                            }
                            tempNum++;
                        }
                        if (isRain)
                        {
                            break;
                        }
                    }
                    if (isRain)
                    {
                        return ("Rain is expected " + tempDay + " from " + times[start] + " to " + times[end]);
                    }
                    else
                    {
                        return ("No rain is expected in the next 5 days");
                    }
                }
            }
            else
            {
                return ("");
            }
        }

        public string analyzeWeather(string input)
        {
            string tempResponse = "";
            string place = "";
            string day = "";
            #region day/place
            //place
            if (input.Contains(" in ") || input.Contains(" at "))
            {
                place = input;
                place = place.Replace(" at ", "");
                place = place.Replace("whats", "");
                place = place.Replace("what's", "");
                place = place.Replace("what", "");
                place = place.Replace("is", "");
                place = place.Replace("will", "");
                place = place.Replace("the", "");
                place = place.Replace("weather", "");
                place = place.Replace("in", "");
                place = place.Replace("wear", "");
                place = place.Replace("temperature", "");
                place = place.Replace("temp", "");
                place = place.Replace(" ", "");
            }
            else
            {
                place = "Shavertown";
            }
            //days
            if (input.Contains("tomorrow"))
            {
                day = "tomorrow";
            }
            else if (input.Contains("monday"))
            {
                day = "monday";
            }
            else if (input.Contains("tuesday"))
            {
                day = "tuesday";
            }
            else if (input.Contains("wednesday"))
            {
                day = "wednesday";
            }
            else if (input.Contains("thursday"))
            {
                day = "thursday";
            }
            else if (input.Contains("friday"))
            {
                day = "friday";
            }
            else if (input.Contains("saturday"))
            {
                day = "saturday";
            }
            else if (input.Contains("sunday"))
            {
                day = "sunday";
            }

            #endregion

            if (input.Contains("weather"))
            {
                tempResponse = GetWeather(place, "condition", day);
            }
            else if (input.Contains("temp"))
            {
                tempResponse = GetWeather(place, "temp", day);
            }
            else if (input.Contains("sunny") || input.Contains("cold") || input.Contains("hot") || input.Contains("chilly") || input.Contains("cloudy") || input.Contains("warm") || input.Contains("cool") || input.Contains("rain") || input.Contains("precip") || input.Contains("wet") || input.Contains("nice out") || input.Contains("storm"))
            {
                if (input.Contains("next"))
                {
                    tempResponse = GetWeather(place, "rainNext", day);
                }
                else
                {
                    tempResponse = GetWeather(place, "rain", day);
                }
            }

            if (input.Contains("humid"))
            {
                if (tempResponse == "")
                {
                    tempResponse = GetWeather(place, "humidity", day);
                }
                else
                {
                    tempResponse = removeDoublesFromWeather(tempResponse);
                    tempResponse += " and " + GetWeather(place, "humidity", day);
                }
            }

            if (input.Contains("sunrise") || input.Contains("sun rise"))
            {
                if (tempResponse == "")
                {
                    tempResponse = GetWeather(place, "sunrise", day);
                }
                else
                {
                    tempResponse = removeDoublesFromWeather(tempResponse);
                    tempResponse += " and " + GetWeather(place, "sunrise", day);
                }
            }

            if (input.Contains("sunset") || input.Contains("sun set"))
            {
                if (tempResponse == "")
                {
                    tempResponse = GetWeather(place, "sunset", day);
                }
                else
                {
                    tempResponse = removeDoublesFromWeather(tempResponse);
                    tempResponse += " and " + GetWeather(place, "sunset", day);
                }
            }

            //return
            if (place == "Shavertown" || tempResponse == "I can't get weather from that city")
            {
                return (tempResponse);
            }
            else
            {
                return (tempResponse + " in " + place);
            }
        }

        string removeDoublesFromWeather(string temp)
        {
            temp = temp.Replace("tomorrow", "");
            temp = temp.Replace("on", "");
            temp = temp.Replace("monday", "");
            temp = temp.Replace("tuesday", "");
            temp = temp.Replace("wednesday", "");
            temp = temp.Replace("thursday", "");
            temp = temp.Replace("friday", "");
            temp = temp.Replace("saturday", "");
            temp = temp.Replace("sunday", "");
            return (temp);
        }
    }
}
