using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    public class Chess
    {
        public string fen { get; private set; }
        Board board;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")//конструктор с начальной позицией по умолчанию
        {
            this.fen = fen;
            board = new Board(fen);
        }
        Chess (Board board)
        {
            this.board = board;
        }
        public Chess Move(string move)//функция позволяющая делать ход Pe2e4(пешка переместилась с e2 на e4) Pe7e8Q(Q - это в кого она превратилась)
        {
            FigureMoving fm = new FigureMoving(move);
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }
        public char GetFigureAt(int x, int y)//получить где находится фигура
        {
            Square square = new Square(x,y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.':(char)f;
        }
    }
}
