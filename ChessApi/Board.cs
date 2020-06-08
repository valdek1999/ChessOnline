using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    class Board
    {
        public string fen { get; private set; }
        Figure[,] figures;//массив всех фигур
        public Color moveColor { get; set; }//чей ход

        public int moveNumber { get; set; }//номер хода

        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        private void Init()
        {
            SetFigureAt(new Square("a1"), Figure.whiteKing);
            SetFigureAt(new Square("h8"), Figure.blackKing);
            moveColor = Color.white;
        }

        public Figure GetFigureAt(Square square)//получить фигуру на клетке
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];          
            }
            return Figure.none;
        }
        void SetFigureAt(Square square,Figure figure)
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }
        public Board Move(FigureMoving fm)
        {
            Board next = new Board(fen);
            next.SetFigureAt(fm.from, Figure.none);
            next.SetFigureAt(fm.to, fm.promotion == Figure.none ? fm.figure : fm.promotion);
            if (moveColor == Color.black)
                next.moveNumber++;
            next.moveColor = moveColor.FlipColor();
            return next;
        }


    }
}
