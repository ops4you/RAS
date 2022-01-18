using System;
using System.CodeDom;
using System.Configuration;
using System.IO.Pipes;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication3
{
    public class Teste
    {
        public static void Main(string[] args)
        {
            Boolean pi = true;
            while (pi)
            {
                Welcome.Welcomen();
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
                            LogIn.Login();
                            break;
                        case "2":
                            SignUp.SignUpe();
                            break;
                        case "3":
                            Console.WriteLine("Até Breve");
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}