using System;
using Newtonsoft.Json;

namespace ConsoleApplication3
{
    public class Readjson
    {
        public void readjason(string json)
        {
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                Console.WriteLine("{0} {1}", item.temp, item.vcc);
            }
        }
    }
}