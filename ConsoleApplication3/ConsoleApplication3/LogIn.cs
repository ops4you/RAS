using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication3
{
    public class LogIn
    {
        public static void Login()
        {
            Console.WriteLine("\n RASBet ---- LOG IN ---- RASBet");

            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            //Console.WriteLine("\n");

            var logi = new
            {
                username = username,
                password = password
            };
            string logg = PostGet.Postread("user/login", JObject.Parse(JsonConvert.SerializeObject(logi)));
            dynamic json = JsonConvert.DeserializeObject(logg);

            if (json["error"] == null)
            {
                Console.WriteLine((string) json["sucess"]);
                Console.WriteLine("Bem Vindo " + username);
                AfterLogin.Afterlogin(username);
            }
            else Console.WriteLine((string) json["error"]);

            /*while (json["error"])
            {
                Console.WriteLine("Username: ");
                username = Console.ReadLine();
                //Console.WriteLine("\n");

                Console.WriteLine("Password: ");
                password = Console.ReadLine();
                //Console.WriteLine("\n");

                logi = new
                {
                    username = username,
                    password = password
                };
                logg = PostGet.Postread("user/login", JObject.Parse(JsonConvert.SerializeObject(logi)));
                json = JsonConvert.DeserializeObject(logg);
            }*/
        }

    }
}