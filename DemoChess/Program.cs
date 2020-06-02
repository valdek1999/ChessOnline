using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessApi;

namespace DemoChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            while (true)
            {
                Console.WriteLine(chess.fen);
                string move = Console.ReadLine();
                if (move == "") break;
                chess = chess.Move(move);

            }
        }
    }
}