using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Marvin_OS
{
    class Prelim
    {
        String[] feelBetter = { "It will get better", "Cheer up sir", "Well I hope you feel better", "Antidepressants?"};
        String[] feelGood = { "Great to hear", "Nice", "Solid", "Glad to hear", "Good to hear" };
        Random rnd = new Random();
        SystemControl systemClass = new SystemControl();
        public string analyzePrelim(string prelim, string input)
        {
            Form1.prelim = "";
            if(prelim == "work")
            {
                if (input.Contains("marvin") || input.Contains("visual studio"))
                {
                    Process.Start(@"C:\Program Files(x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe");
                    return ("done");
                }
                else if (input.Contains("school"))
                {
                    Process.Start("chrome.exe", @"https:\\https://classroom.google.com/u/1/h");
                    return ("done");
                }
                else if (input.Contains("ebay"))
                {
                    Process.Start("chrome.exe", @"https://www.paypal.com/mep/dashboard");
                    Process.Start("chrome.exe", @"https://www.ebay.com/sh/ovw");
                    return ("done");
                }
                else if (input.Contains("amazon"))
                {
                    Process.Start("chrome.exe", @"https://sellercentral.amazon.com/gp/homepage.html?");
                    return ("done");
                }
            }else if(prelim == "whatsup")
            {
                if(input.Contains("bad") || input.Contains("shit") || input.Contains("horrible") || input.Contains("terrible") || input.Contains("worse"))
                {
                    if (input.Contains("not"))
                    {
                        return (feelGood[rnd.Next(0, feelBetter.Length)]);
                    }
                    else
                    {
                        return (feelBetter[rnd.Next(0, feelBetter.Length)]);
                    }
                }else if(input.Contains("good") || input.Contains("great") || input.Contains("groovy") || input.Contains("fine") || input.Contains("alright"))
                {
                    if (input.Contains("not"))
                    {
                        return (feelBetter[rnd.Next(0, feelBetter.Length)]);
                    }
                    else
                    {
                        return (feelGood[rnd.Next(0, feelBetter.Length)]);
                    }
                }
            }else if(prelim == "shutdown")
            {
                if(input.Contains("shutdown") || input.Contains("shutdown"))
                {
                    systemClass.Shutdown();
                }
                else if (input.Contains("hybernate"))
                {
                    systemClass.Hybernate();
                }
            }
            return ("");
        }
    }
}
