using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientChessAPI;
using Microsoft.Win32;

namespace ConsoleChess
{
    class Program
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());
        private ChessClient client;
        public const string HOST = "http://hardfoxy.ddns.net:7777/api/";
        public int User { get; private set; }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();

        }

        void Start()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            int USER = random.Next(10000);
            client = new ChessClient(HOST, USER);
            Console.WriteLine(client.host);
           
            while(true)
            {
                string param = Console.ReadLine();
                Console.Clear();
                if(param == "r")
                {
                    Console.WriteLine(client.GetGame(true));
                    continue;
                }

                if(param == "q")
                {
                    client.CloseGame();
                    break;
                }

                if (param == "" ) Console.WriteLine(client.GetGame(false));
                else if(param.Length == 5)//проверка на дурака ,т.к ход должен содержать 5 символов
                {
                    Console.WriteLine(client.MakeMove(param));
                }
                else//проверка на дурака ,т.к ход должен содержать 5 символов
                {
                    Console.WriteLine("Неверный формат хода или "+client.MakeMove(param));
                }



            }
        }
    }
}
