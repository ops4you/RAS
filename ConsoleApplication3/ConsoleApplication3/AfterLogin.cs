using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication3
{
    public class AfterLogin
    {
                public static void Afterlogin(string username)
        {
            bool p = true;
            double saldo = 0.00;

            //get a partir do username
            PostGet.Getread("user/wallet/" + username);
            while (p)
            {
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
                var escolha1 = Console.ReadLine();
                switch (escolha1)
                {
                    case "1": //DESPORTOS
                        //todo LISTAR DESPORTOS
                        string desp = PostGet.Getread("user/sports");
                        dynamic json = JsonConvert.DeserializeObject(desp);
                        break;
                    case "2": //ABERTAS
                        //todo LISTAR APOSTAS ABERTAS
                        //foreach(aposta n)
                        //jogo nr: 
                        //equipa1 1 : (odds)
                        //equipa2 2 : (odds)
                        //empate x : (odds)
                        string bets = PostGet.Getread("bet/bets");
                        dynamic json1 = JsonConvert.DeserializeObject(bets);
                        break;
                    case "3": //HISTORICO
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
                    case "4": //DEPOSIT
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
                    case "5": //LEVANTA
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
                        string lev = PostGet.Postread("user/trade",
                            JObject.Parse(JsonConvert.SerializeObject(levanta)));
                        break;
                    case "6": //LOG OUT
                        Console.WriteLine("Esperamos vê-lo em breve!");
                        p = false;
                        break;
                    case "7": //DELETE
                        string del = PostGet.Getread("user/delete/" + username);
                        break;
                    case "8": //TRADE
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
}