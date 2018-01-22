using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Marvin_OS
{
    public class Smarthome
    {
        #region hue lights
        public int brightness = 0;
        public async void turnOff()
        {
            brightness = 0;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            String command = "{\"on\":false}";
            var content = new StringContent(command, Encoding.UTF8, "application/json");
            HttpResponseMessage returnStatement = await client.PutAsync("http://192.168.254.46/api/4trIsQUolAZDp7KIPrXw5rrApLpENH4euxAgtA4T/lights/1/state", content);
        }

        public async void turnOn()
        {
            brightness = 254;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            String command = "{\"on\":true}";
            var content = new StringContent(command, Encoding.UTF8, "application/json");
            HttpResponseMessage returnStatement = await client.PutAsync("http://192.168.254.46/api/4trIsQUolAZDp7KIPrXw5rrApLpENH4euxAgtA4T/lights/1/state", content);
        }

        public async void changeColor(string hue)
        {
            brightness = 254;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            String command = "{\"on\":true, \"sat\":254, \"bri\":254, \"hue\": " + hue + "}";
            var content = new StringContent(command, Encoding.UTF8, "application/json");
            HttpResponseMessage returnStatement = await client.PutAsync("http://192.168.254.46/api/4trIsQUolAZDp7KIPrXw5rrApLpENH4euxAgtA4T/lights/1/state", content);
        }

        public async void changeBrightness(string bri)
        {
            brightness = Convert.ToInt32(bri);
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            String command = "{\"on\":true, \"sat\":254, \"bri\": " + bri +"}";
            var content = new StringContent(command, Encoding.UTF8, "application/json");
            HttpResponseMessage returnStatement = await client.PutAsync("http://192.168.254.46/api/4trIsQUolAZDp7KIPrXw5rrApLpENH4euxAgtA4T/lights/1/state", content);
        }

        public async void changeWhite(string ct)
        {
            brightness = 254;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            String command = "{\"on\":true, \"sat\":254, \"bri\":254, \"ct\":" + ct + "}";
            var content = new StringContent(command, Encoding.UTF8, "application/json");
            HttpResponseMessage returnStatement = await client.PutAsync("http://192.168.254.46/api/4trIsQUolAZDp7KIPrXw5rrApLpENH4euxAgtA4T/lights/1/state", content);
        }
        #endregion
    }
}