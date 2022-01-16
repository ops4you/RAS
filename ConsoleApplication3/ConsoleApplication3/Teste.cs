using System;

namespace ConsoleApplication3
{
    public class Teste
    {
        static void Main(string[] args)
        {
            double saldo = 0.00;
            string nome = "Default";
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

                    break;
                case 2:
                    Console.WriteLine("\n RASBet ---- SIGN UP ---- RASBet");
                    //Console.WriteLine("\n");

                    Console.WriteLine("Nome: ");
                    nome = Console.ReadLine();
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
                    while (repPassword!=newpassword)
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
                    string nasc = Console.ReadLine();
                    Console.WriteLine("... \n");
                    Console.WriteLine("Bem-Vindo "+nome);
                    Console.WriteLine("Aposte com consciência.");
                    
                    Console.WriteLine("\n \n");
                    //Menu principal after Log IN
                    Console.WriteLine("\nRASBet ---- BEM VINDO ---- RASBet\n" +
                                      ("- \n") +
                                      ("Bem-Vindo" + nome + "\n") +
                                      ("Saldo: " + saldo + "€ \n") +
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
                                      "-----------------------------------");
                    Console.WriteLine("Choose menu item: ");
                    int escolha1 = int.Parse(Console.ReadLine());
                    switch (escolha1)
                    {
                        case 1:
                                //todo LISTAR DESPORTOS
                            break; 
                        case 2:
                            //todo LISTAR APOSTAS ABERTAS
                            break; 
                        case 3:
                            //todo LISTAR HISTÓRICO DE APOSTAS
                            break; 
                        case 4: //DEPOSIT
                            Console.WriteLine("Quantidade a depositar:");
                            int deposit=Console.Read();
                            break; 
                        case 5:
                            Console.WriteLine("Quantidade a levantar:");
                            int levantar=Console.Read();
                            break; 
                        case 6:
                            //todo Log out
                            break; 
                        case 7:
                            //todo apagar conta
                            break; 
                            
                    }
                    break;
             }
        }
    }
}