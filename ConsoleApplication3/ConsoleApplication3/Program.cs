/*
using System;


namespace ConsoleApplication3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}

package menu;

import business.GestaoDeStocks;
import business.GestaoDeStocksInterface;
import business.NoPaletesException;
import business.NoRobotAvailableException;
import business.Robot;

import java.util.LinkedList;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Random;
using Scanner;

public class Menu {

    //GestaoDeStocks gds;

    public Menu()
    {
    	this.gds = new GestaoDeStocks(); 
    	//dar load da gds da base de dados
    	initMenu();
    	
    }
    
    
    public void initMenu(){
    	
        Scanner in = new Scanner(System.in);

        // print menu

//            Console.WriteLine("\n######### Sistema de GestÃ£o de Stocks ########\n" +
//                    "Pressione a tecla correspondente Ã  aÃ§Ã£o pretendida:\n" +
//                    "--------------------------------------------------------\n" +
//                    "|1. Comunicar cÃ³digo QR                               |\n" +
//                    "|2. Sistema comunica ordem de transporte               |\n" +
//                    "|3. Notificar recolha de paletes                       |\n" +
//                    "|4. Notificar entrega de paletes                       |\n" +
//                    "|5. Consultar listagem de localizaÃ§Ãµes               |\n" +
//                    "|0. Quit                                               |\n" +
//                    "--------------------------------------------------------");

        // handle user commands

        bool quit = false;

        int menuItem;

        while(!quit)
        {
        	 Console.WriteLine("\n######### Sistema de GestÃ£o de Stocks ########\n" +
                     "Pressione a tecla correspondente Ã  aÃ§Ã£o pretendida:\n" +
                     "--------------------------------------------------------\n" +
                     "|1. Comunicar cÃ³digo QR                                |\n" +
                     "|2. Sistema comunica ordem de transporte               |\n" +
                     "|3. Notificar recolha de paletes                       |\n" +
                     "|4. Notificar entrega de paletes                       |\n" +
                     "|5. Consultar listagem de localizaÃ§Ãµes                 |\n" +
                     "|0. Quit                                               |\n" +
                     "--------------------------------------------------------");

            Console.WriteLine("Choose menu item: ");

            menuItem = in.nextInt();

            switch (menuItem) {

                case 1:
                    Console.WriteLine("\n\nComunicar cÃ³digo QR\n");
                    Console.WriteLine("Digite o cÃ³digo QR da palete");
                    
                    bool q = false;
                    Scanner input = new Scanner(System.in);
                
                    int codPalete = Integer.parseInt(input.nextLine());
                    
                    Console.WriteLine("Digite qual o material");
                    String material = input.nextLine();
                    
                    do{
                        
                        //
                        this.gds.addPalete(codPalete,material);
                        Console.WriteLine("Palete adicionada com sucesso\n");
                        q = true;
                    } while (!q);


                    break;

                case 2:

                    Console.WriteLine("\n\nSistema comunica ordem de transporte\n");


                    bool p = false;
                    int localizacaoPalete;
                    int idPalete;

                    Console.WriteLine("Indique a LocalizaÃ§Ã£o da palete:");
                    Scanner ordem = new Scanner(System.in);
                    localizacaoPalete = Integer.parseInt(ordem.nextLine());

                    Console.WriteLine("Indique o identificador da palete:");
                    idPalete = Integer.parseInt(ordem.nextLine());

                    do {
                        try {
							this.gds.atribuiRobot(localizacaoPalete,idPalete);
						} catch (NoRobotAvailableException e) {
							Console.WriteLine("Não existem robots disponiveis neste momento");
						}
                        Console.WriteLine("Ordem em curso\n");
                        p = true;
                    }while(!p);

                    break;

                case 3:

                    Console.WriteLine("\n\nNotificar recolha de paletes\n");

                    bool x = false;
                    int idePalete;

                    Console.WriteLine("Indique a LocalizaÃ§Ã£o da palete:");
                    Scanner recolha = new Scanner(System.in);
                    idePalete = Integer.parseInt(recolha.nextLine());

                    do {
                    	this.gds.notificarRecolha(idePalete);
                        Console.WriteLine("Recolha efetuada\n");
                        x = true;

                    }while(!x);

                    break;

                case 4:

                    Console.WriteLine("\n\nNotificar entrega de paletes\n");

                    bool u = false;
                    int idesPalete;
                    int localizacao;

                    Console.WriteLine("Indique o Identificador da palete:");
                    int entrega = int.Parse(Console.ReadLine());
                    idesPalete = Integer.parseInt(entrega.nextLine());

                    Console.WriteLine("Indique a LocalizaÃ§Ã£o da palete:");
                    localizacao = int.Parse(Console.ReadLine());

                    Console.WriteLine("Indique o id do Robot:");
                    int idRobot = int.Parse(Console.ReadLine());
                    
                    do {
                    	this.gds.notificarEntrega(idesPalete,localizacao,idRobot);
                        Console.WriteLine("Entrega efetuada\n");
                        u = true;

                    }while(!u);

                    break;

                case 5:

                    Console.WriteLine("Consultar listagem de localizaÃ§Ãµes");
                    bool k = false;
                    
                    try {
                    	for (Entry<Integer, Integer> entry : gds.getPaletes().entrySet()) {
                    	    Console.WriteLine("A palete " + entry.getKey() + "\t esta em: " + entry.getValue());
                    	}
					} catch (NoPaletesException e) {
						Console.WriteLine("Não exsitem Palletes no sistema");
						k=true;
					}

                    break;

                case 0:

                    quit = true;

                    break;

                default:

                    Console.WriteLine("Invalid choice.");

            }

        }

        Console.WriteLine("Bye-bye!");

    }
}
*/