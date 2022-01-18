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
            //get a partir do username


            while (p)
            {
                string ar = PostGet.Getread("user/wallet/" + username);
                dynamic json11 = JsonConvert.DeserializeObject(ar);
                double saldoE = Double.Parse((string) json11["euro"]);
                double saldoU = Double.Parse((string) json11["usd"]);
                double saldoL = Double.Parse((string) json11["gbp"]);
                double saldoA = Double.Parse((string) json11["ada"]);
                Console.WriteLine("\n \n");
                //Menu principal after Log IN
                Console.WriteLine("\nRASBet ---- BEM VINDO ---- RASBet\n" +
                                  ("- \n") +
                                  ("Bem-Vindo " + username + "\n") +
                                  ("Eur: " + saldoE + "€ \n") +
                                  ("USD: " + saldoU + "$ \n") +
                                  ("GBP: " + saldoL + "£ \n") +
                                  ("ADA: " + saldoA + "ADA \n") +
                                  ("- \n") +
                                  "-----------------------------------\n" +
                                  "| MENU                            |\n" +
                                  "|                                 |\n" +
                                  "|Bet. APOSTAR!!                   |\n" +
                                  "|1. Desportos                     |\n" +
                                  "|2. Apostas Abertas               |\n" +
                                  "|3. Histórico de Apostas          |\n" +
                                  "|4. Depositar                     |\n" +
                                  "|5. Levantar                      |\n" +
                                  "|6. Log Out                       |\n" +
                                  "|7. Apagar Conta                  |\n" +
                                  "|8. Converter moedas              |\n" +
                                  "|9. Criar Aposta                  |\n" +
                                  "|10. Fechar Aposta                |\n" +
                                  "-----------------------------------");
                Console.WriteLine("Choose menu item: ");
                var escolha1 = Console.ReadLine();
                switch (escolha1)
                {
                    case "BET" or "Bet" or "bet" :
                        Console.WriteLine("Vamos Apostar!");
                        Console.WriteLine("Indique o id do jogo:");
                        int idds = int.Parse(Console.ReadLine());
                        Console.WriteLine("Indique o resultado em que aposta:");
                        Console.WriteLine("1- Visitada \n" + 
                                          "2- Empate \n" +
                                          "3- Visitante \n");
                        int resul = int.Parse(Console.ReadLine());
                        Console.WriteLine("Indique a moeda para pagamento");
                        Console.WriteLine("1- Euro \n" +
                                          ("2- USD \n") +
                                          ("3- Libras \n") +
                                          ("4- Cardano \n"));
                        int coine = int.Parse(Console.ReadLine()) + 5;
                        Console.WriteLine("Indique o valor que deseja apostar:");
                        int valour = int.Parse(Console.ReadLine());
                        var beta = new
                        {
                            username = username,
                            aposta_id = idds,
                            resultado = resul,
                            moeda_id = coine,
                            valor = valour
                        };
                        PostGet.Postread("bet/make",
                            JObject.Parse(JsonConvert.SerializeObject(beta)));
                        break;
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
                        Console.WriteLine(bets);
                        dynamic betts = JsonConvert.DeserializeObject(bets);
                        try
                        {
                            foreach (var item in betts)
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}\n", item.id, item.clube1, item.clube2,
                                    item.odsw, item.odsd, item.odsl, item.desporto);
                            }
                        } 
                        catch (Exception e) { Console.WriteLine(e);}

                        Console.WriteLine("Voltar ao menu (type smth)");
                        Console.ReadLine();
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
                        string bethistory = PostGet.Getread("user/bets/"+username);
                        dynamic json2 = JsonConvert.DeserializeObject(bethistory);
                        break;
                    case "4": //DEPOSIT
                        Console.WriteLine("Selecione moeda:");
                        Console.WriteLine("1- Euro \n" +
                                          ("2- USD \n") +
                                          ("3- Libras \n") +
                                          ("4- Cardano \n"));
                        int moedal = int.Parse(Console.ReadLine());
                        int moeda = moedal + 5;
                        Console.WriteLine("Quantidade a depositar:");
                        int deposit = int.Parse(Console.ReadLine());
                        var depos = new
                        {
                            moeda = moeda,
                            valor = deposit,
                            nome = username
                        };
                        string dep = PostGet.Postread("user/deposit",
                            JObject.Parse(JsonConvert.SerializeObject(depos)));
                        break;
                    case "5": //LEVANTA
                        Console.WriteLine("Selecione moeda:");
                        Console.WriteLine("1- Euro \n" +
                                          ("2- USD \n") +
                                          ("3- Libras \n") +
                                          ("4- Cardano \n"));
                        int moedital = int.Parse(Console.ReadLine());
                        int moedita = moedital + 5;
                        Console.WriteLine("Quantidade a levantar:");
                        int levantar = int.Parse(Console.ReadLine());

                        var levanta = new
                        {
                            moeda = moedita,
                            valor = levantar,
                            nome = username
                        };
                        string lev = PostGet.Postread("user/withdrawl",
                            JObject.Parse(JsonConvert.SerializeObject(levanta)));
                        break;
                    case "6": //LOG OUT
                        Console.WriteLine("Esperamos vê-lo em breve!");
                        p = false;
                        break;
                    case "7": //DELETE
                        var del = new
                        {
                            nome = username
                        };
                        string dele = PostGet.Getread("user/delete/" + username);
                        Environment.Exit(0);
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
                    case "9": //ADMIN criar bet
                        //if (//is admin -> show else zona restrita)

                        Console.WriteLine("Criar Aposta");
                        Console.WriteLine("Insira o nome equipa da equipa visitada:");
                        String team1 = Console.ReadLine();
                        Console.WriteLine("Insira o nome equipa da equipa visitante:");
                        String team2 = Console.ReadLine();
                        Console.WriteLine("Insira a odd da vitória da equipa da equipa visitada:");
                        int oddsW = int.Parse(Console.ReadLine());
                        Console.WriteLine("Insira a odd da vitória da equipa da equipa visitante:");
                        int oddsL = int.Parse(Console.ReadLine());
                        Console.WriteLine("Insira a odd do resultado ser empate:");
                        int oddsD = int.Parse(Console.ReadLine());
                        Console.WriteLine("Insira o desporto em que se realiza o evento:");
                        Console.WriteLine("1- Futebol \n" + 
                                          "2- Futsal \n" +
                                          "3- Basketball \n" +
                                          "4- Andebol \n");
                        int desP = int.Parse(Console.ReadLine());
                        string desPorto = " ";
                        switch (desP)
                        {
                            case 1:
                                desPorto = "Futebol";
                                break;
                            case 2:
                                desPorto = "Futsal";
                                break;
                            case 3:
                                desPorto = "Basketball";
                                break;
                            case 4:
                                desPorto = "Andebol";
                                break;
                        }

                        var apo = new
                        {
                            clube1 = team1,
                            clube2 = team2,
                            oddsw = oddsW,
                            oddsd = oddsD,
                            oddsl = oddsL,
                            desporto = desPorto
                        };
                        string bete = PostGet.Postread("bet/create", JObject.Parse(JsonConvert.SerializeObject(apo)));
                        break;
                    
                    case "10":
                        //if (//is admin -> show else zona restrita)
                        
                        Console.WriteLine("Fechar aposta.");
                        Console.WriteLine("Id aposta a fechar:");
                        int idd = int.Parse(Console.ReadLine());
                        Console.WriteLine("Resultado:");
                        Console.WriteLine("1- Visitada \n" + 
                                          "2- Empate \n" +
                                          "3- Visitante \n");
                        int res = int.Parse(Console.ReadLine())-1;
                        var clo = new
                        {
                            aposta_id = idd,
                            result = res
                        };
                        break;
                }
            }
        }

    }
}