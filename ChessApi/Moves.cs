using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    class Moves
    {
        FigureMoving fm;
        Board board;
        
        public Moves (Board board)
        {
            this.board = board;
        }
        public bool CanMove (FigureMoving fm)//можно ли пойти
        {
            this.fm = fm;
            return CanMoveFrom() &&//можно ли пойти с той клетки куда мы идем
                CanMoveTo() &&//можно ли пойти на ту клетку куда собрались идти
                CanFigureMove();//может ли фигура сделать этот ход

        }
        bool CanMoveFrom()
        {
            return fm.from.OnBoard() && fm.figure.GetColor() == board.moveColor;
            //клетка должна быть на доске и фигура должна быть того цвета - чей ход
        }
        

        bool CanMoveTo()
        {
            return fm.to.OnBoard() && fm.from != fm.to && board.GetFigureAt(fm.to).GetColor() != board.moveColor;
                //клетка должна быть на доске и должна идти или на клетку с фигурой противоположного цвета(чтобы не есть свои) или пустую
        }
        bool CanFigureMove()
        {
            switch (fm.figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return (fm.SignX == 0 || fm.SignY == 0) &&
                        CanStraightMove();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (fm.SignX != 0 && fm.SignY != 0) &&
                        CanStraightMove();

                case Figure.whiteKnigth://конь
                case Figure.blackKnigth:
                    return CanKnigthMove();

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();

                default:
                    return false;
            }
        }

        bool CanPawnMove()
        {
            if (fm.from.y < 1 || fm.from.y > 6)//не может быть на 1ой и 7ой горизонтали при ходе
                return false;
            int stepY = fm.figure.GetColor() == Color.white ? 1 : -1;
            return
                CanPawnGo(stepY) ||//пойти вперед на одну клетку
                CanPawnJump(stepY) ||//пойти вперед на две клетки
                CanPawnEat(stepY);//может ли съесть
            //взятие на проходе пока нет..

        }
        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.none)//нет припятствий в виде фигуры
                if (fm.DeltaX == 0)//не двигается в лево или в право
                    if (fm.DeltaY == stepY)//двигается на шаг вперед
                        return true;
            return false;
        }
        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.none)//нет припятствий в виде фигуры
                if (fm.DeltaX == 0)//не двигается в лево или в право
                    if (fm.DeltaY == 2 * stepY)
                        if (fm.from.y == 1 || fm.from.y == 6)//то что идет с 1ой или 6ой горизонтали
                            if (board.GetFigureAt(new Square(fm.from.x, fm.from.y + stepY)) == Figure.none)//то что пешка не перепрыгивает фигуру
                                return true;
            return false;
        }
        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.to) != Figure.none)
                if (fm.AbsDeltaX == 1)//кушает по диогонали
                    if (fm.DeltaY == stepY)//вперед или назад
                        return true;
            return false;
        }
        

        bool CanStraightMove()//королева
        {
            Square at = fm.from;
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);//прибавляет единичный вектор в которую направлена фигура пока не достигнет точки
                if (at == fm.to)
                    return true;
            }
            while (at.OnBoard() && board.GetFigureAt(at)==Figure.none);//если наткнется на фигуру раньше или зайдет за пределы то false 
            return false;
        }

        private bool CanKingMove()//может ли ходить король 
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1)
                return true;
            return false;
        }

        private bool CanKnigthMove()//конь
        {
            if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2) return true;
            if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1) return true;
            return false;
        }
    }
}
