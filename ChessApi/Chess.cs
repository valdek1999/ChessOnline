using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    public class Chess
    {
        public string fen { get; private set; }//fen-стандартная нотация записи шахматных диаграмм ,
        Board board;//доска
        Moves moves;//можно ли сделать ход фигурой вообще
        bool check;//флажок для проверки "шах и мата" или "пата"
        List<FigureMoving> allMoves;//находит все ходы
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")//конструктор с начальной позицией по умолчанию
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
            
        }
        Chess (Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }
        public Chess Move(string move)//функция позволяющая делать ход Pe2e4(пешка переместилась с e2 на e4) Pe7e8Q(Q - это в кого она превратилась)
        {
            FigureMoving fm = new FigureMoving(move);//инициализирует ход (какая фигура)->(откуда)->(куда)->(превратилась ли во что-то)
            if (!moves.CanMove(fm))//можем ли сделать данный ход.
                return this;
                if (board.IsCheckAfterMove(fm))//и не будет ли после него возможность съесть короля
                    return this;
            Board nextBoard = board.Move(fm);//выдает новую доску с другими расположениями фигур
            Chess nextChess = new Chess(nextBoard);//иницализирует новый шахматный экзеплеяр с новым fen и board
            return nextChess;
        }
        public char GetFigureAt(int x, int y)//получить где находится фигура
        {
            Square square = new Square(x,y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.':(char)f;
        }

        public char GetFigureAt(string xy)//получить где находится фигура через строковый ввод
        {
            Square square = new Square(xy);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach(FigureOnSquare fs in board.YieldFigures())//по списку всех доступных фигур
                foreach (Square to in Square.YieldSquares())//по списку всех клеток 
                {
                    FigureMoving fm = new FigureMoving(fs, to);//создает ход фигуры
                    if (moves.CanMove(fm))//проверяет может ли идти по  этому ходу
                        if(!board.IsCheckAfterMove (fm))//проверяем после ход пропал шах или нет
                        allMoves.Add(fm);//если да,добавляет в список все возможных ходов.
                }
            
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach (FigureMoving fm in allMoves)
                list.Add(fm.ToString());
            return list;
        }

        public bool IsCheck()//помогает определить поставлен ли шах
        {
            return board.IsCheck();//метод возращающий true,если поставлен шах
        }
        public string ColorBoard()
        {
           return board.moveColor == Color.white ? "white":"black";
        }
    }
}
