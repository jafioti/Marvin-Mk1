using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;

namespace Marvin_OS
{ 
    class Question
    {
        Weather weatherClass = new Weather();
        MathClass math = new MathClass();
        Utility utilityClass = new Utility();
        SystemControl systemClass = new SystemControl();
        Smarthome smarthomeClass = new Smarthome();

        public string analyzeQuestion(string input)
        {
            #region questions
            //questions
            #region what
            //what
            if (input.Contains("what") || input.Contains("whats") || input.Contains("what's"))
            {
                if (input.Contains("name"))
                {
                    return("My name is Marvin!");
                }
                else if (input.Contains("sunrise") || input.Contains("sun rise") || input.Contains("sunset") || input.Contains("sun set"))
                {
                    return (weatherClass.analyzeWeather(input));
                }
                else if (input.Contains("can"))
                {
                    if (input.Contains("you"))
                    {
                        return ("I can have basic conversations, get you information, and keep you company!");
                    }
                }
                else if (input.Contains("will"))
                {
                    if (input.Contains("weather") || input.Contains("temp") || input.Contains("rain") || input.Contains("precip") || input.Contains("sun") || input.Contains("humidity") || input.Contains("cloud"))
                    {
                        return(weatherClass.analyzeWeather(input));
                    }
                }
                else if (input.Contains("are ") && !input.Contains("square"))
                {
                    if (input.Contains("you"))
                    {
                        if (input.Contains("doing"))
                        {
                            return ("I don't usually do too much...");
                        }
                        else
                        {
                            return ("My name is Marvin, I'm a program!");
                        }
                    }
                    else
                    {
                        input = input.Remove(0, 9);
                        return(wolfram(input));
                    }
                }
                else if(input.Contains(" do "))
                {
                    if (input.Contains("calendar"))
                    {
                        string url = "https://calendar.google.com/calendar/b/0/r";
                        System.Diagnostics.Process.Start("chrome.exe", url);
                        return ("Opening calendar");
                    }
                    if(input.Contains(" i "))
                    {
                        if (input.Contains("on") || input.Contains("have"))
                        {
                            string url = "https://calendar.google.com/calendar/b/0/r";
                            System.Diagnostics.Process.Start("chrome.exe", url);
                            return ("Opening calendar");
                        }
                    }
                }
                if (input.Contains("is") || input.Contains("whats") || input.Contains("what's"))
                {
                    if (input.Contains("sunrise") || input.Contains("sun rise") || input.Contains("sun set") || input.Contains("sunset"))
                    {
                        return (weatherClass.analyzeWeather(input));
                    }
                    else if (input.Contains("time"))
                    {
                        if (input.Contains(" in "))
                        {
                            return (wolfram(input));
                        }
                        else
                        {
                            return ("It is " + DateTime.Now.ToString("h:mm:ss tt"));
                        }
                    }
                    else if (input.Contains("date"))
                    {
                        return (DateTime.Today.ToShortDateString());
                    }
                    else if (input.Contains("weather") || input.Contains("temp") || input.Contains("rain") || input.Contains("sunny") || input.Contains("humid"))
                    {
                        return (weatherClass.analyzeWeather(input));
                    }
                    else if (input.Contains("up") || input.Contains("poppin") || input.Contains("popping") || input.Contains("cracking") || input.Contains("crackin"))
                    {
                        Form1.prelim = "whatsup";
                        return ("Not much, how about you?");
                    }
					else if (input.Contains("schedule") || input.Contains("calendar") || input.Contains("agenda"))
					{
						string url = "https://calendar.google.com/calendar/b/0/r";
                        System.Diagnostics.Process.Start("chrome.exe", url);
                        return("Opening calendar");
					}
                    else if (input.Contains("square root"))
                    {
                        return (math.squareroot(input));
                    }
                    else if (input.Contains("squared"))
                    {
                        return (math.squared(input));
                    }
                    else if (input.Any(char.IsDigit) && (input.Contains("+") || input.Contains("-") || input.Contains("*") || input.Contains("/")))
                    {
                        //math problem
                        string problem = "";
                        int start = 0;
                        for (int i = 0; i <= input.Length; i++)
                        {
                            if (char.IsDigit(input[i]))
                            {
                                //is number
                                start = i;
                                break;
                            }
                        }
                        for (int i = start; i < input.Length; i++)
                        {
                            problem += input[i];
                        }
                        MathParser parser = new MathParser();
                        return (parser.Parse(problem, false).ToString());
                    }
                    else if (input.Contains("your"))
                    {
                        if (input.Contains("favorite"))
                        {
                            if (input.Contains("color"))
                            {
                                return ("Probably blue");
                            }
                            else if (input.Contains("song"))
                            {
                                return ("Definetly classic rock.");
                            }
                            else if (input.Contains("food"))
                            {
                                return ("Programs don't usually eat.");
                            }
                            else
                            {
                                return ("I don't have too many favorites...");
                            }
                        }
                    }
                    else
                    {
                        //if all else fails
                        return(wolfram(input));
                    }
                }
                else
                {
                    //if all else fails
                    return (wolfram(input));
                }
            }
            #endregion

            #region when
            if (input.Contains("when"))
            {
                if (input.Contains("weather") || input.Contains("rain") || input.Contains("sun") || input.Contains("temp"))
                {
                    return(weatherClass.analyzeWeather(input));
                }else if (input.Contains("you"))
                {
                    if (input.Contains("birth")){
                        return ("I'm not sure, let's say January 1st");
                    }
                }
                return(wolfram(input));
            }
            #endregion

            #region why
            if (input.Contains("why"))
            {
                if (input.Contains("you"))
                {
                    return ("I'm really not sure, sir");
                }
                return (wolfram(input));
            }
            #endregion

            #region where
            if (input.Contains("where"))
            {
                if (input.Contains("you"))
                {
                    return ("In your computer");
                }
                return (wolfram(input));
            }
            #endregion

            #region who
            //who
            if (input.Contains("who"))
            {
                if (input.Contains("creator"))
                {
                    return("I am an AI programmed by Joe Fioti");
                }
                else if (input.Contains("you"))
                {
                    if (input.Contains("made"))
                    {
                        return("I am an assistant programmed by Joe Fioti");
                    }
                    else if (input.Contains("are"))
                    {
                        return("My name is Marvin!");
                    }
                }
                else if (input.Contains("your"))
                {
                    if (input.Contains("maker"))
                    {
                        return("I am an AI programmed by Joe Fioti");
                    }
                }
                else if (input.Contains("is"))
                {
                    input = input.Remove(0, 7);
                    return (wolfram(input));
                }
                return (wolfram(input));
            }
            #endregion

            #region how
            //how
            if (input.Contains("how"))
            {
                if (input.Contains("are") || input.Contains("how're"))
                {
                    if (input.Contains("old"))
                    {
                        return("Programs don't age!");
                    }
                    else if (input.Contains("you"))
                    {
                        Form1.prelim = "whatsup";
                        return ("Great thanks, how about you?");
                    }
                    else if (input.Contains("is") || input.Contains("are"))
                    {
                        if (input.Contains("is"))
                        {
                            input = input.Remove(0, 7);
                        }
                        else
                        {
                            input = input.Remove(0, 8);
                        }
                    }
                    else if (input.Contains("feel"))
                    {
                        return("Not bad, how about you?");
                    }
                    else if (input.Contains("do"))
                    {
                        if (input.Contains("i"))
                        {
                            if (input.Contains("look"))
                            {
                                return("I'm not really a mirror, but you probably look pretty nice");
                            }
                            else if (input.Contains("feel"))
                            {
                                return("You tell me!");
                            }
                        }
                    }
                }
                return (wolfram(input));
            }
            #endregion

            #region can
            //can
            if (input.Contains("can") || input.Contains("could"))
            {
                if (input.Contains("you") || input.Contains("we"))
                {
                    if (input.Contains("talk"))
                    {
                        return("I am right now!");
                    }
                    else if (input.Contains("homework"))
                    {
                        return("I don't think your teacher would be too happy!");
                    }
                    else if (input.Contains("go"))
                    {
                        return("I can't go anywhere, I'm kind of stuck in a computer...");
                    }
                    else if (input.Contains("my"))
                    {
                        if (input.Contains("mail"))
                        {
                            Process.Start("chrome.exe", @"https:\\inbox.google.com/u/0/?pli=1");
                            return ("done");
                        }
                        else
                        {
                            return ("I'm not your butler!");
                        }
                    }
                    else if (input.Contains("time"))
                    {
                        return(DateTime.Now.ToString("h:mm:ss tt"));
                    }
                    else if(input.Contains("light") || input.Contains("lamp"))
                    {
                        if(input.Contains("up") || input.Contains("on") || input.Contains("give"))
                        {
                            if (input.Contains("little"))
                            {
                                smarthomeClass.changeBrightness((smarthomeClass.brightness + 80).ToString());
                            }
                            else
                            {
                                smarthomeClass.turnOn();
                            }
                            return ("done");
                        }else if(input.Contains("down") || input.Contains("off") || input.Contains("kill"))
                        {
                            if (input.Contains("little"))
                            {
                                smarthomeClass.changeBrightness((smarthomeClass.brightness - 80).ToString());
                            }
                            else
                            {
                                smarthomeClass.turnOff();
                            }
                            return ("done");
                        }else if (input.Contains("blue"))
                        {
                            smarthomeClass.changeColor("46920");
                            return ("done");
                        } else if (input.Contains("red"))
                        {
                            smarthomeClass.changeColor("65280");
                            return ("done");
                        }
                        else if (input.Contains("pink"))
                        {
                            smarthomeClass.changeColor("56100");
                            return ("done");
                        }
                        else if (input.Contains("green"))
                        {
                            smarthomeClass.changeColor("27000");
                            return ("done");
                        }
                        else if (input.Contains("yellow"))
                        {
                            smarthomeClass.changeColor("12750");
                            return ("done");
                        }
                        else if (input.Contains("purple"))
                        {
                            smarthomeClass.changeColor("50000");
                            return ("done");
                        }
                        else if (input.Contains("white"))
                        {
                            if (input.Contains("warm"))
                            {
                                smarthomeClass.changeWhite("450");
                            }
                            else
                            {
                                smarthomeClass.changeWhite("160");
                            }
                            return ("done");
                        } else if (input.Contains("black"))
                        {
                            smarthomeClass.turnOff();
                            return ("done");
                        }
                        else if (input.Contains("orange"))
                        {
                            smarthomeClass.changeColor("5000");
                            return ("done");
                        }
                        else
                        {
                            smarthomeClass.turnOn();
                            return ("done");
                        }
                    }
                    else if (input.Contains("volume"))
                    {
                        if (input.Contains("up") || input.Contains("raise"))
                        {
                            if (input.Any(char.IsDigit))
                            {
                                string num = "";
                                //Get all numbers
                                for (int i = 0; i < input.Length; i++)
                                {
                                    if (char.IsDigit(input[i]))
                                    {
                                        num += input[i];
                                    }
                                }
                                //Raise the volume
                                for (int i = 0; i <= Convert.ToInt32(num); i++)
                                {
                                    systemClass.VolumeUp();
                                }
                            }
                            else
                            {
                                systemClass.VolumeUp();
                            }
                            return ("done");
                        }
                        else if (input.Contains("down") || input.Contains("lower"))
                        {
                            if (input.Any(char.IsDigit))
                            {
                                string num = "";
                                //Get all numbers
                                for (int i = 0; i < input.Length; i++)
                                {
                                    if (char.IsDigit(input[i]))
                                    {
                                        num += input[i];
                                    }
                                }
                                //Raise the volume
                                for (int i = 0; i <= Convert.ToInt32(num); i++)
                                {
                                    systemClass.VolumeDown();
                                }
                            }
                            else
                            {
                                systemClass.VolumeDown();
                            }
                            return ("done");
                        }
                        else if (input.Contains("mute") || input.Contains("unmute"))
                        {
                            systemClass.Mute();
                            return ("done");
                        }
                    }
                    else if (input.Contains("computer"))
                    {
                        if (input.Contains("sleep"))
                        {
                            systemClass.Sleep();
                            return ("done");
                        }
                        else if (input.Contains("hybernate"))
                        {
                            systemClass.Hybernate();
                            return ("done");
                        }
                        else if (input.Contains("lock"))
                        {
                            systemClass.Lock();
                            return ("done");
                        }
                        else if (input.Contains("restart"))
                        {
                            systemClass.Restart();
                            return ("done");
                        }
                        else if (input.Contains("shutdown") || input.Contains("shut down"))
                        {
                            Form1.prelim = "shutdown";
                            return ("Would you like to shutdown or hybernate?");
                        }
                    }
                }
                return(wolfram(input));
            }
            #endregion

            #region are
            //are
            if (input.Contains("are") && !input.Contains("square"))
            {
                if (input.Contains("you"))
                {
                    if (input.Contains("boy") || input.Contains("girl"))
                    {
                        return("I'm a program, I don't have gender, although I use a male voice");
                    }
                    else
                    {
                        return("I don't really know.");
                    }
                }
                return(wolfram(input));
            }
            #endregion

            #region is
            //is
            if (input.Contains("is"))
            {
                if (input.Contains("cold") || input.Contains("sunny") || input.Contains("cloudy") || input.Contains("warm") || input.Contains("hot") || input.Contains("chill") || input.Contains("umbrella")
                    || input.Contains("rain") || input.Contains("precip") || input.Contains("nice out") || input.Contains("weather") || input.Contains("storm"))
                {
                    return(weatherClass.analyzeWeather(input));
                }
                return(wolfram(input));
            }
            #endregion

            #region will
            if (input.Contains("will"))
            {
                if (input.Contains("cold") || input.Contains("sunny") || input.Contains("cloudy") || input.Contains("warm") || input.Contains("hot") || input.Contains("chill") || input.Contains("umbrella")
                    || input.Contains("rain") || input.Contains("precip") || input.Contains("nice out") || input.Contains("weather") || input.Contains("storm"))
                {
                    return(weatherClass.analyzeWeather(input));
                }
                return(wolfram(input));
            }
            #endregion
            #endregion
            return ("");
        }

        void google(string query)
        {
            string url = "https:\\www.google.com/search?q=" + System.Uri.EscapeDataString(query);
            System.Diagnostics.Process.Start("chrome.exe", url);
        }
        
        public string wolfram(string query)
        {
            string newquery = query.Replace(" ", "+");
            try
            {
                string url = "http://api.wolframalpha.com/v1/result?appid=Y7WEJ3-LHG35WY9R2&i=" + newquery;
                WebClient client = new WebClient();
                return (client.DownloadString(url));
            }

            catch
            {
                google(query);
                return ("done");
            }
        }
    }
}
