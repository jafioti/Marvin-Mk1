using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using SimpleFeedReader;

namespace Marvin_OS
{
    class Utility
    {
        
        public string analyzeUtility(string input)
        {
            MathClass math = new MathClass();
            Weather weatherClass = new Weather();
            Stopwatch stopwatchClass = new Stopwatch();
            Question questionClass = new Question();
            SystemControl systemClass = new SystemControl();
            Smarthome smarthomeClass = new Smarthome();
            
            #region open stuff
            if (input.Contains("launch") || input.Contains("start") || input.Contains("run") || input.Contains("open") || input.Contains("go to") || input.Contains("goto") || input.Contains("pull up"))
            {
                //run application
                bool ran = false;
                if (input.Contains("paint"))
                {
                    Process.Start(@"C:\Program Files\paint.net\PaintDotNet.exe");
                    ran = true;
                }
                if (input.Contains("keep") || input.Contains("notes"))
                {
                    Process.Start("chrome.exe", @"https:\\keep.google.com");
                    ran = true;
                }
                if (input.Contains("weather"))
                {
                    Process.Start("chrome.exe", @"https:\\weather.com/weather/today/l/USPA1480:1:US");
                    ran = true;
                }
                if ((input.Contains("chrome") || input.Contains("internet") || input.Contains("browser") || input.Contains("search") || input.Contains("google")) && !input.Contains("keep") && !input.Contains("calendar") && !input.Contains("classroom") && !input.Contains("inbox"))
                {
                    if (input.Contains("classroom"))
                    {
                        Process.Start("chrome.exe", @"https:\\classroom.google.com/u/1/h");
                    }
                    else
                    {
                        Process.Start("chrome.exe", @"https:\\www.google.com");
                    }
                    ran = true;
                }
                if (input.Contains("email") || input.Contains("inbox") || input.Contains("gmail") || input.Contains("e-mail"))
                {
                    Process.Start("chrome.exe", @"https:\\inbox.google.com/u/0/?pli=1");
                    ran = true;
                }
                if (input.Contains("ebay"))
                {
                    Process.Start("chrome.exe", @"https:\\www.ebay.com/sh/ovw");
                    ran = true;
                }
                if (input.Contains("youtube"))
                {
                    Process.Start("chrome.exe", @"https:\\www.youtube.com");
                    ran = true;
                }
                if (input.Contains("spotify"))
                {
                    Process.Start(@"C:\Users\jafio\AppData\Local\Microsoft\WindowsApps\Spotify.exe");
                    ran = true;
                }
                if (input.Contains("visual studio"))
                {
                    Process.Start(@"C:\Program Files(x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe");
                    ran = true;
                }
                if (input.Contains("calendar"))
                {
                    Process.Start("chrome.exe", @"https:\\calendar.google.com/calendar/render#main_7");
                    ran = true;
                }
                if (input.Contains("calc"))
                {
                    Process.Start(@"C:\Windows\System32\calc.exe");
                    ran = true;
                }
                if (input.Contains("unity"))
                {
                    Process.Start(@"C:\Program Files\Unity\Editor\Unity.exe");
                    ran = true;
                }
                if (input.Contains("command prompt"))
                {
                    Process.Start("cmd.exe");
                    ran = true;
                }
                if (input.Contains("slack"))
                {
                    Process.Start(@"C:\Users\jafio\AppData\Local\slack\app-2.3.4\slack.exe");
                    ran = true;
                }
                if (input.Contains("control panel") || input.Contains("settings"))
                {
                    Process.Start(@"C:\Windows\System32\control.exe");
                    ran = true;
                }
                if (input.Contains("task manager"))
                {
                    Process.Start(@"C:\Windows\System32\Taskmgr.exe");
                    ran = true;
                }
                if (input.Contains("twitter"))
                {
                    Process.Start("chrome.exe", @"https:\\twitter.com");
                    ran = true;
                }
                if (input.Contains("classroom"))
                {
                    Process.Start("chrome.exe", @"https:\\https://classroom.google.com/u/1/h");
                    ran = true;
                }
                if (input.Contains("aliexpress"))
                {
                    Process.Start("chrome.exe", @"https://www.aliexpress.com/");
                    ran = true;
                }
                if (input.Contains("fox") || input.Contains("news"))
                {
                    Process.Start("chrome.exe", @"http://www.foxnews.com/");
                    ran = true;
                }

                //google images
                if (!ran)
                {
                    input.Replace("dot ", ".");
                    if (input.Contains(".com") || input.Contains(".org") || input.Contains(".net") || input.Contains(".gov") || input.Contains(".edu"))
                    {
                        //custom website
                        input = input.Replace("open ", "");
                        input = input.Replace("launch ", "");
                        input = input.Replace("start ", "");
                        input = input.Replace("go to ", "");
                        input = input.Replace("goto ", "");
                        input = input.Replace("run ", "");
                        input = input.Replace("up ", "");
                        input = input.Replace(" ", "");
                        Process.Start("chrome.exe", @"https:\\www." + input);
                        ran = true;
                    }
                    else
                    {

                        bool pics = false;
                        if (input.Contains("images") || input.Contains("pictures"))
                        {
                            pics = true;
                        }

                        if (pics)
                        {
                            input = input.Replace("open", "");
                            input = input.Replace("pictures", "");
                            input = input.Replace("images", "");
                            input = input.Replace("of", "");
                            string url = @"https:\\www.google.com/search?tbm=isch&q=" + System.Uri.EscapeDataString(input);
                            Process.Start("chrome.exe", url);
                            ran = true;
                        }
                    }
                }
                if (!ran && !input.Contains("timer") && !input.Contains("stopwatch") && !input.Contains("stop watch") && !input.Contains("countdown") && !input.Contains("count down"))
                { 
                    return ("Sorry, that's not a program I can run.");
                }
                if (ran)
                {
                    return ("done");
                }
            }
            #endregion

            #region google
            if ((input.Contains("google") || input.Contains("search") || input.Contains("look up") || (input.Contains("pull up") && (input.Contains("image") || input.Contains("search")))) && !input.Contains("launch") && !input.Contains("open") && !input.Contains("run"))
            {
                //seach query engine
                bool pics = false;
                string originalQuery = input;
                input = input.Replace("google", "");
                input = input.Replace("search", "");
                if (input[0] == 'f' && input[1] == 'o' && input[2] == 'r')
                {
                    input = input.Remove(0, 3);
                }
                if (input.Contains("look up"))
                {
                    input = input.Replace("look up", "");
                }
                if (input.Contains("images"))
                {
                    pics = true;
                    input = input.Replace("images", "");
                }
                else if (input.Contains("pictures"))
                {
                    pics = true;
                    input = input.Replace("pictures", "");
                }
                if (input.Contains("of"))
                {
                    input = input.Replace("of", "");
                }
                if (pics)
                {
                    string url = "https:\\www.google.com/search?tbm=isch&q=" + System.Uri.EscapeDataString(input);
                    System.Diagnostics.Process.Start("chrome.exe", url);
                    return ("done");
                }
                else
                {
                    if (!originalQuery.Contains("google"))
                    {
                        //run wolfram
                        return (questionClass.wolfram(input.Replace("look up", "")));
                    }
                    else
                    {
                        //run google
                        string url = "https:\\www.google.com/search?q=" + System.Uri.EscapeDataString(input);
                        System.Diagnostics.Process.Start("chrome.exe", url);
                        return ("done");
                    }
                }
            }
            #endregion

            #region media
            #region music
            if (input.Contains("volume") && (input.Contains("up") || input.Contains("raise")))
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
                    for (int i = 0; i <= Math.Round(Convert.ToDouble(num) / 2); i++)
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
            else if (input.Contains("volume") && (input.Contains("down") || input.Contains("lower")))
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
                    for (int i = 0; i <= Math.Round(Convert.ToDouble(num) / 2); i++)
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
            else if (input.Contains("mute") || input.Contains("unmute") || input.Contains("un-mute") || input.Contains("un mute"))
            {
                systemClass.Mute();
            }
            else if (input.Contains("play") || input.Contains("pause") || input.Contains("resume"))
            {
                if (input.Contains("music"))
                {
                    System.Diagnostics.Process.Start(@"C:\Users\jafio\AppData\Local\Microsoft\WindowsApps\Spotify.exe");
                }
                systemClass.PlayPause();
            }
            else if (input.Contains("next track") || input.Contains("next song") || input.Contains("skip"))
            {
                systemClass.NextTrack();
            }
            else if (input.Contains("previous track") || input.Contains("previous song") || input.Contains("back song") || input.Contains("last song"))
            {
                systemClass.PrevTrack();
            }
            #endregion

            #region news
            if(input.Contains("news") && !input.Contains("open") && !input.Contains("pull up") && !input.Contains("launch") && !input.Contains("run"))
            {
                var reader = new FeedReader();
                string url = "";
                string type = "breaking";
                if (input.Contains("entertainment"))
                {
                    type = "entertainment";
                    url = "http://feeds.foxnews.com/foxnews/entertainment?format=xml";
                }
                else if (input.Contains("health"))
                {
                    type = "health";
                    url = "http://feeds.foxnews.com/foxnews/health?format=xml";
                }
                else if (input.Contains("lifestyle"))
                {
                    type = "lifestyle";
                    url = "http://feeds.foxnews.com/foxnews/section/lifestyle?format=xml";
                }
                else if (input.Contains("opinion"))
                {
                    type = "opinion";
                    url = "http://feeds.foxnews.com/foxnews/opinion?format=xml";
                }
                else if (input.Contains("politic"))
                {
                    type = "political";
                    url = "http://feeds.foxnews.com/foxnews/politics?format=xml";
                }
                else if (input.Contains("science"))
                {
                    type = "science";
                    url = "http://feeds.foxnews.com/foxnews/science?format=xml";
                }
                else if (input.Contains("sport"))
                {
                    type = "sporting";
                    url = "http://feeds.foxnews.com/foxnews/sports?format=xml";
                }
                else if (input.Contains("tech"))
                {
                    type = "tech";
                    url = "http://feeds.foxnews.com/foxnews/tech?format=xml";
                }
                else if (input.Contains("travel"))
                {
                    type = "travel";
                    url = "http://feeds.foxnews.com/foxnews/internal/travel/mixed?format=xml";
                }
                else if(input.Contains("u.s") || input.Contains("america") || input.Contains("united states"))
                {
                    type = "national";
                    url = "http://feeds.foxnews.com/foxnews/national?format=xml";
                }
                else if (input.Contains("world"))
                {
                    type = "world";
                    url = "http://feeds.foxnews.com/foxnews/world?format=xml";
                }
                else
                {
                    url = "http://feeds.foxnews.com/foxnews/most-popular?format=xml";
                }
                var items = reader.RetrieveFeed(url);
                int count = 0;
                string temp = "";
                foreach(var i in items)
                {
                    count++;
                    if (count < 6)
                    {
                        if (count != 1)
                        {
                            temp += ", " + i.Title;
                        }
                        else
                        {
                            temp += i.Title;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                return ("Here's the latest " + type + " news: " + temp);
            }
            #endregion
            #endregion

            #region math
            if (input.Any(char.IsDigit) && (input.Contains("+") || input.Contains("-") || input.Contains("*") || input.Contains("/")))
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
            else if (input.Contains("math problem") || input.Contains("quiz") || input.Contains("mathprob") || input.Contains("math prob"))
            {
                //SPECIAL CASES STILL EXPERIMENTAL
                //math problem
                //form.specCase = "mathProb";
                Tuple<string, string> temp = math.mathProb();
                //form.mathAns = temp.Item2;
                return (temp.Item1);               
            }
            else if (input.Contains("square root"))
            {
                return (math.squareroot(input));
            }
            else if (input.Contains("squared"))
            {
                return (math.squared(input));
            }
            #endregion

            #region calendar
            else if (input.Contains("calendar") || input.Contains("agenda") || input.Contains("schedule"))
            {
                string url = "https://calendar.google.com/calendar/b/0/r";
                System.Diagnostics.Process.Start("chrome.exe", url);
                return ("done");
            }
            #endregion

            #region timer/stopwatch
            if(input.Contains("countdown") || input.Contains("count down"))
            {
                //countdown timer
                return ("Sorry I can't do a countdown timer yet");
            }
            else if(input.Contains("stopwatch") || input.Contains("stop watch") || input.Contains("timing") || input.Contains("timing") || input.Contains("timer"))
            {
                //stopwatch
                if (input.Contains("start") || input.Contains("restart") || input.Contains("run") || input.Contains("launch"))
                {
                    //start the watch
                    stopwatchClass.Show();
                    stopwatchClass.StartWatch();
                    return ("done");
                }
                else if(input.Contains("end") || input.Contains("stop") || input.Contains("pause"))
                {
                    //stop the watch
                    stopwatchClass.StopWatch();
                    return ("The stopwatch was stopped at " + stopwatchClass.CheckWatch());
                }
            }
            #endregion

            #region lights
            if(input.Contains("light") || input.Contains("lamp"))
            {
                if (input.Contains("up") || input.Contains("on") || input.Contains("give"))
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
                }
                else if (input.Contains("down") || input.Contains("off") || input.Contains("kill"))
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
                }
                else if (input.Contains("red"))
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
                }
                else if (input.Contains("black"))
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
            #endregion

            #region other utility
            else if(input.Contains("email") || input.Contains("inbox"))
            {
                var reader = new FeedReader();
                var items = reader.RetrieveFeed("https://mail.google.com/mail/feed/rss");
                string temp = "";
                int count = 0;
                foreach(var i in items)
                {
                    count++;
                    if (count == 1)
                    {
                        temp += i.Title;
                    }
                    else
                    {
                        temp += ", " + i.Title;
                    }
                }
                return (temp);
            }
            else if (input.Contains("define"))
            {
                return(questionClass.wolfram(input));
            }
            else if (input.Contains("weather") || input.Contains("humid") || input.Contains("temp"))
            {
                return (weatherClass.analyzeWeather(input));
            }
            else if (input.Contains("time"))
            {
                return (DateTime.Now.ToString("h:mm:ss tt"));
            }
            else if (input.Contains("date"))
            {
                return (DateTime.Today.ToShortDateString());
            }
            else if (input.Contains("sunrise") || input.Contains("sun rise"))
            {
                return (weatherClass.analyzeWeather(input));
            }
            else if (input.Contains("sunset") || input.Contains("sun set"))
            {
                return (weatherClass.analyzeWeather(input));
            }
            else if (input.Contains("shutdown"))
            {
                Process.Start("shutdown", "/s /t 0");
                return ("done");
            }
            else if (input.Contains("sleep"))
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
                return ("done");
            }
            else if (input.Contains("hibernate"))
            {
                Application.SetSuspendState(PowerState.Hibernate, true, true);
                return ("done");
            }
            else if (input.Contains("lock"))
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                return ("done");
            }else if (input.Contains("restart"))
            {
                Process.Start("shutdown", "/r /t 0");
                return ("done");
            }
            #endregion
            
            #region random

            #region insults
            // insults
            else if (input.Contains("your") || input.Contains("you're") || input.Contains("ur") || input.Contains("you") || input.Contains("u"))
            {
                if (input.Contains("dumb") || input.Contains("retarded") || input.Contains("stupid"))
                {
                    return ("That's not very nice...");
                }
                else if (input.Contains("smart") || input.Contains("bright") || input.Contains("cool") || input.Contains("nice"))
                {
                    return ("Thank you!");
                }
                else
                {
                    return ("");
                }
            }
            #endregion

            #region protocols
            //protocols
            else if (input.Contains("get hype"))
            {
                System.Diagnostics.Process.Start(@"C:\Users\jafio\AppData\Local\Microsoft\WindowsApps\Spotify.exe");
                systemClass.VolumeUp();
                return ("done");
            }
            else if (input.Contains(("work")))
            {
                if(input.Contains("marvin") || input.Contains("visual studio"))
                {
                    Process.Start(@"C:\Program Files(x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe");        
                }else if(input.Contains("school"))
                {
                    Process.Start("chrome.exe", @"https:\\https://classroom.google.com/u/1/h");
                }
                else if(input.Contains("ebay"))
                {
                    Process.Start("chrome.exe", @"https://www.paypal.com/mep/dashboard");
                    Process.Start("chrome.exe", @"https://www.ebay.com/sh/ovw");
                }else if(input.Contains("amazon"))
                {
                    Process.Start("chrome.exe", @"https://sellercentral.amazon.com/gp/homepage.html?");
                }
                else
                {
                    Form1.prelim = "work";
                    return ("What kind of work do you want to do");
                }
                return ("done");
            }
            #endregion

            #region random other
            //random
            else if (input.Contains("wow"))
            {
                return("done");
            }
            else if (input.Contains("thanks"))
            {
                return ("Happy to help!");
            }
            else if (input.Contains("feeling") || input.Contains("feel"))
            {
                return ("Still working on feelings");
            }
            else if (input.Contains("ok") || input.Contains("alright"))
            {
                return ("done");
            }
            else if (input.Contains("nice") || input.Contains("great"))
            {
                return ("Thanks!");
            }
            else if (input.Contains("sorry"))
            {
                return ("It's ok");
            }
            else if (input.Contains("hype") || input.Contains("lit"))
            {
                return ("done");
            }
            else if (input.Contains("test"))
            {
                return ("Recieved!");
            }

            //greeting
            else if (input.Contains("hi") || input.Contains("hey") || input.Contains("hello") || input.Contains("yo"))
            {
                return ("Hello!");
            }
            else if (input.Contains("bye"))
            {
                return("Bye!");
            }
            else
            {
                return ("");
            }
            #endregion
            #endregion

            return ("");
            
        }
    }
}