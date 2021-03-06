using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication3
{
    public class Welcome
    {
                public static void Welcomen()
        {
            bool p = true;
            while (p)
            {
                Console.WriteLine("\nRASBet ---- BEM VINDO ---- RASBet\n" +
                                  "-----------------------------------\n" +
                                  "| MENU                            |\n" +
                                  "|                                 |\n" +
                                  "|1. Log In                        |\n" +
                                  "|2. Sign Up                       |\n" +
                                  "|3. Fechar                        |\n" +
                                  "-----------------------------------");
                Console.WriteLine("Choose menu item: ");
                String escolha = Console.ReadLine();
                switch (escolha)
                {
                    case "1":
                        //Login();
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
                        break;
                    case "2":
                        //SignUp();
                        Console.WriteLine("\n RASBet ---- SIGN UP ---- RASBet");
            //Console.WriteLine("\n");

            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Username: ");
            string newusername = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Password: ");
            string newpassword = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Repeat Password: ");
            string repPassword = Console.ReadLine();
            //Console.WriteLine("\n");
            while (repPassword != newpassword)
            {
                Console.WriteLine("\n !! PASSWORDS N??O S??O IGUAIS !!\n");
                Console.WriteLine("Password: ");
                newpassword = Console.ReadLine();

                Console.WriteLine("Repeat Password: ");
                repPassword = Console.ReadLine();
            }

            Console.WriteLine("Nif: (apenas n??meros)");
            String nif = Console.ReadLine();
            //Console.WriteLine("\n");

            Console.WriteLine("Data Nascimento (dd/mm/aaaa): ");
            DateTime userDateTime;

            while (!DateTime.TryParse(Console.ReadLine(), out userDateTime) ||
                   userDateTime > DateTime.Now)
            {
                Console.WriteLine("Data errada.");
                Console.WriteLine("REPETE");
                Console.WriteLine("Data Nascimento (dd/mm/aaaa): ");
            }
            //Console.WriteLine("Nasceste no dia: " + userDateTime);
            // while (userDateTime>(DateTime.Now))
            // {
            //     Console.WriteLine("Data errada.");
            //     Console.WriteLine("REPETE");
            //     Console.WriteLine("Data Nascimento (dd/mm/aaaa): ");
            // }

            if (userDateTime.AddYears(18) > DateTime.Now)
            {
                Console.WriteLine("N??o possui idade legal para jogar");
                Environment.Exit(0);
            }

            userDateTime = userDateTime.Date;

            Console.WriteLine("Admin (s/n): ");
            int fin = 0;
            String admin = Console.ReadLine();
            if (admin is "s" or "S")
            {
                Console.WriteLine("Admin-Key: ");
                String key = Console.ReadLine();
                var i = 1;
                while (key != "RAS2122" & i < 4)
                {
                    Console.WriteLine("\n CHAVE ERRADA (tentativa " + i + "/3)");
                    Console.WriteLine("Admin-Key: ");
                    i++;
                    key = Console.ReadLine();
                }

                if (key == "RAS2122") fin = 1;

                if (key != "RAS2122")
                {
                    Console.WriteLine("Continuar SignUp sem ser admin? (s/n):");
                    string choose = Console.ReadLine();
                    if (choose is "s" or "S")
                    {
                        admin = "n";
                        fin = 0;
                    }
                    else Environment.Exit(0);
                }
            }
            else
            {
                admin = "n";
                fin = 0;
            }
            //Console.WriteLine("\n");

            var signup = new
            {
                nome = nome,
                username = newusername,
                email = email,
                password = newpassword,
                nif = nif,
                datanasc = userDateTime,
                admin = fin
            };
            string jsonData = JsonConvert.SerializeObject(signup);
            JObject jsonObject = JObject.Parse(jsonData);
            string a = PostGet.Postread("user/register", jsonObject);
            dynamic jsone = JsonConvert.DeserializeObject(a);
            //Console.WriteLine(jsonData);

            //Print the parsed Json object
            //Console.WriteLine((string)jsonObject["Nome"]);
            //Console.WriteLine((string)jsonObject["Username"]);
            //Console.WriteLine((string)jsonObject["Email"]);
            //Console.WriteLine((string)jsonObject["Password"]);
            // Console.WriteLine((string)jsonObject["Nif"]);
            // Console.WriteLine((string)jsonObject["DataNasc"]);


            if (jsone["error"] == null)
            {
                Console.WriteLine((string) jsone["sucess"]);
                Console.WriteLine("\nBem-Vindo " + nome);
                Console.WriteLine("Aposte com consci??ncia.");
                AfterLogin.Afterlogin(newusername);
            }
            else Console.WriteLine((string) jsone["error"]);
                        break;
                    case "3":
                        Console.WriteLine("At?? Breve");
                        Environment.Exit(0);
                        break;
                }
            }
        }

    }
}