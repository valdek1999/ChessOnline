using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    class Board
    {
        public string fen { get; private set; }//fen - представление шахматных диаграмм в виде строки
        Figure[,] figures;//массив всех фигур

        public Color moveColor { get; set; }//чей ход

        public int moveNumber { get; set; }//номер хода

        public Board(string fen)//инициализация доски
        {
            this.fen = fen;
            figures = new Figure[8, 8];//доска 8на8 клеток
            Init();
        }

        private void Init()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            //0                                           1 2    3 4 5
            //0-фигуры расположение , 1-кто ходит 2-рокировка 3-проходимое поле 4- применение правила 50 ходов 5-номер хода
            string[] parts = fen.Split();
            if (parts.Length != 6) return;
            InitFigures(parts[0]);
            moveNumber = int.Parse(parts[5]);
            
            moveColor = (parts[1]=="b") ? Color.black:Color.white;
        }

        void InitFigures(string data)//инициализирует фигуры по fen
        {
            for (int j = 8; j >= 2; j--)
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
                for (int x = 0; x < 8; x++)
                    figures[x, y] = lines[7 - y][x] == '.' ? Figure.none :
                        (Figure)lines[7 - y][x];//присваивает расположение фигур из fen
        }
        void GenerateFEN()//генерация нового fen
        {
            fen = FenFigures() + " " + (moveColor == Color.white ? "w" : "b") +
                " - - 0 " + moveNumber.ToString();
        }

        public IEnumerable<FigureOnSquare> YieldFigures()//все фигуры на клетках
        {
            foreach (Square square in Square.YieldSquares())
                if (GetFigureAt(square).GetColor() == moveColor)//если полученная фигура на клетке цвета текущего хода
                    yield return new FigureOnSquare(GetFigureAt(square), square);//возращаем эту фигуру
        }

        string FenFigures()//формирует первую часть fen с фигурами
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                if (y > 0)
                    sb.Append('/');
            }
            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
                sb.Replace(eight.Substring(0, j), j.ToString());//заменяет единицы на их кол-во ,чтобы представить кол-во пустых клетов в удобной форме
            return sb.ToString();

        }

        public Figure GetFigureAt(Square square)//получить фигуру на клетке
        {
            if (square.OnBoard())
            {
                return figures[square.x, square.y];          
            }
            return Figure.none;
        }
        void SetFigureAt(Square square,Figure figure)//установаить расположение фигуры на заданной клетке
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }
        public Board Move(FigureMoving fm)//когда делает ход на досвке
        {
            Board next = new Board(fen);
            next.SetFigureAt(fm.from, Figure.none);
            next.SetFigureAt(fm.to, fm.promotion == Figure.none ? fm.figure : fm.promotion);
            if (moveColor == Color.black)
                next.moveNumber++;
            next.moveColor = moveColor.FlipColor();
            next.GenerateFEN();//после того как сделан ход запускается генерация fen
            return next;
        }

        public bool IsCheck ()//проверка на шах
        {
            Board after = new Board(fen);//создаем копию доски 
            after.moveColor = moveColor.FlipColor();//меняем ход 
            return after.CanEatKing();//и проверяем можно ли съесть нашего короля 
        }

        public bool IsCheckAfterMove(FigureMoving fm)//будет ли поставлен шах после хода
        {
            Board after = Move(fm);
            return after.CanEatKing();
        }
        
        bool CanEatKing()
        {
            Square badKing = FindBadKing();//найдем координату плохого короля
            Moves moves = new Moves(this);//все возможные ходы
            foreach (FigureOnSquare fs in YieldFigures())//по списку всех доступных фигур
            {
                FigureMoving fm = new FigureMoving(fs, badKing);//создаем ход который идет на короля
                if (moves.CanMove(fm))//проверяем можно ли этот ход сделать 
                    return true;//если да ,то поставлен шах
            }
            return false;
        }

        private Square FindBadKing()//поиск короля для которого возможен шах
        {
            Figure badKing = moveColor == Color.black ? Figure.whiteKing : Figure.blackKing;//для какого короля мы ищем шах
            foreach(Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square) == badKing)
                {
                    return square;
                }
            }
            return Square.none;
        }
    }
}
