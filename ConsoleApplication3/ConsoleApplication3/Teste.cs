using System;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication3
{
    public class Teste
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nRASBet ---- BEM VINDO ---- RASBet\n" +
                              "-----------------------------------\n" +
                              "| MENU                            |\n" +
                              "|                                 |\n" +
                              "|1. Log In                        |\n" +
                              "|2. Sign Up                       |\n" +
                              "-----------------------------------");
            Console.WriteLine("Choose menu item: ");
            int escolha = int.Parse(Console.ReadLine());
            switch (escolha)
            {
                case 1:
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

                    if (json["success"]) Console.WriteLine("Bem Vindo " + username);
                    else if (json["error"]) Console.WriteLine("Credenciais erradas");
                    while (json["error"])
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
                    }
                    break;
                case 2:
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
                        Console.WriteLine("\n !! PASSWORDS NÃO SÃO IGUAIS !!\n");
                        Console.WriteLine("Password: ");
                        newpassword = Console.ReadLine();

                        Console.WriteLine("Repeat Password: ");
                        repPassword = Console.ReadLine();
                    }

                    Console.WriteLine("Nif: ");
                    int nif = int.Parse(Console.ReadLine());
                    //Console.WriteLine("\n");

                    Console.WriteLine("Data Nascimento (dd/mm/aaaa): ");
                    DateTime userDateTime;

                    while (!DateTime.TryParse(Console.ReadLine(), out userDateTime) || userDateTime > DateTime.Now)
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
                        Console.WriteLine("Não possui idade legal para jogar");
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
                            else break;
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
                    PostGet.Postread("user/register", jsonObject);
                    Console.WriteLine(jsonData);

                    //Print the parsed Json object
                    // Console.WriteLine((string)jsonObject["Nome"]);
                    // Console.WriteLine((string)jsonObject["Username"]);
                    // Console.WriteLine((string)jsonObject["Email"]);
                    // Console.WriteLine((string)jsonObject["Password"]);
                    // Console.WriteLine((string)jsonObject["Nif"]);
                    // Console.WriteLine((string)jsonObject["DataNasc"]);

                    Console.WriteLine("\nBem-Vindo " + nome);
                    Console.WriteLine("Aposte com consciência.");
                    afterlogin(newusername);
                    break;
            }
        }
        static void afterlogin(string username)
                             {
                                 double saldo = 0.00;
                                 string nome = "Default";
                                 
                                 //get a partir do username
                                 PostGet.Getread("user/wallet/" + username);             
         
                                 Console.WriteLine("\n \n");
                                 //Menu principal after Log IN
                                 Console.WriteLine("\nRASBet ---- BEM VINDO ---- RASBet\n" +
                                                   ("- \n") +
                                                   ("Bem-Vindo " + username + "\n") +
                                                   ("Saldo: " + saldo + "€ \n") + //colocar para diferentes moedas no prt
                                                   ("- \n") +
                                                   "-----------------------------------\n" +
                                                   "| MENU                            |\n" +
                                                   "|                                 |\n" +
                                                   "|1. Desportos                     |\n" +
                                                   "|2. Apostas Abertas               |\n" +
                                                   "|3. Histórico de Apostas          |\n" +
                                                   "|4. Depositar                     |\n" +
                                                   "|5. Levantar                      |\n" +
                                                   "|6. Log Out                       |\n" +
                                                   "|7. Apagar Conta                  |\n" +
                                                   "|8. Converter moedas              |\n" +
                                                   "-----------------------------------");
                                 Console.WriteLine("Choose menu item: ");
                                 int escolha1 = int.Parse(Console.ReadLine());
                                 switch (escolha1)
                                 {
                                     case 1: //DESPORTOS
                                         //todo LISTAR DESPORTOS
                                         string desp = PostGet.Getread("user/sports");
                                         dynamic json = JsonConvert.DeserializeObject(desp);
                                         break;
                                     case 2: //ABERTAS
                                         //todo LISTAR APOSTAS ABERTAS
                                         //foreach(aposta n)
                                         //jogo nr: 
                                         //equipa1 1 : (odds)
                                         //equipa2 2 : (odds)
                                         //empate x : (odds)
                                         string bets = PostGet.Getread("bet/bets");
                                         dynamic json1 = JsonConvert.DeserializeObject(bets);
                                         break;
                                     case 3: //HISTORICO
                                         //todo LISTAR HISTÓRICO DE APOSTAS
                                         //foreach(aposta n)
                                         //jogo nr: 
                                         //equipa1 1
                                         //equipa2 2
                                         //empate x
                                         //Aposta:
                                         //Valor apostado:
                                         //Odd:
                                         //-> (ganhos ou LOST)
                                         string bethistory = PostGet.Getread("user/bethistory");
                                         dynamic json2 = JsonConvert.DeserializeObject(bethistory);
                                         break;
                                     case 4: //DEPOSIT
                                         Console.WriteLine("Selecione moeda:");
                                         Console.WriteLine("1- Euro \n" +
                                                           ("2- USD \n") +
                                                           ("3- Libras \n") +
                                                           ("4- Cardano \n"));
                                         int moedal = int.Parse(Console.ReadLine());
                                         int moeda = moedal - 1;
                                         Console.WriteLine("Quantidade a depositar:");
                                         int deposit = int.Parse(Console.ReadLine()); 
                                         var depos = new
                                         {
                                             coin = moeda,
                                             deposi = deposit
                                         };
                                         string dep = PostGet.Postread("user/trade", JObject.Parse(JsonConvert.SerializeObject(depos)));
                                         break;
                                     case 5: //LEVANTA
                                         Console.WriteLine("Selecione moeda:");
                                         Console.WriteLine("1- Euro \n" +
                                                           ("2- USD \n") +
                                                           ("3- Libras \n") +
                                                           ("4- Cardano \n"));
                                         int moedital = int.Parse(Console.ReadLine());
                                         int moedita = moedital - 1;
                                         Console.WriteLine("Quantidade a levantar:");
                                         int levantar = int.Parse(Console.ReadLine()); 
                                         
                                         var levanta = new
                                         {
                                             coin = moedita,
                                             levant = levantar
                                         };
                                         string lev = PostGet.Postread("user/trade", JObject.Parse(JsonConvert.SerializeObject(levanta)));
                                         break;
                                     case 6: //LOG OUT
                                         Console.WriteLine("Esperamos vê-lo em breve!");
                                         Environment.Exit(0);
                                         break;
                                     case 7: //DELETE
                                         string del = PostGet.Getread("user/delete/" + username);
                                         break;
                                     case 8: ///TRADE
                                         Console.WriteLine("Trocar: \n");
                                         Console.WriteLine("1- Euro \n" +
                                                           ("2- USD \n") +
                                                           ("3- Libras \n") +
                                                           ("4- Cardano \n"));
                                         int antigal = int.Parse(Console.ReadLine());
                                         int antiga = antigal - 1;
                                         Console.WriteLine("Por: \n");
                                         Console.WriteLine("1- Euro \n" +
                                                           ("2- USD \n") +
                                                           ("3- Libras \n") +
                                                           ("4- Cardano \n"));
                                         int noval = int.Parse(Console.ReadLine());
                                         int nova = noval - 1;
                                         Console.WriteLine("Quantidade: \n");
                                         int quant = int.Parse(Console.ReadLine());
                                         
                                         var troca = new
                                         {
                                             old = antiga,
                                             newe = nova,
                                             amount = quant
                                         };
                                         
                                         string tro = PostGet.Postread("user/trade", JObject.Parse(JsonConvert.SerializeObject(troca)));
                                         break;
                                 }
                             }
    }
}