using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApi
{
    enum Figure//перечисление фигур
    {
        none,

        whiteKing = 'K',//король
        whiteQueen = 'Q',//королева
        whiteRook = 'R',//ладья
        whiteBishop = 'B',//слон
        whiteKnigth = 'N',//конь
        whitePawn = 'P',//пешка

        blackKing = 'k',//король
        blackQueen = 'q',//королева
        blackRook = 'r',//ладья
        blackBishop = 'b',//слон
        blackKnigth = 'n',//конь
        blackPawn = 'p'//пешка

    }
    static class FigureMethods
    {
        public static Color GetColor (this Figure figure)
        {
            if (figure == Figure.none)
                return Color.none;
            return (figure == Figure.whiteKing ||
                figure == Figure.whiteQueen ||
                figure == Figure.whiteRook ||
                figure == Figure.whiteBishop ||
                figure == Figure.whiteKnigth ||
                figure == Figure.whitePawn) ? Color.white : Color.black;
        }
    }
}
