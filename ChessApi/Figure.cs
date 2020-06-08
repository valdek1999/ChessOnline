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
        whiteQuen = 'Q',//королева
        whiteRook = 'R',//ладья
        whiteBishop = 'B',//слон
        whiteKnigth = 'N',//конь
        whitePawn = 'P',//пешка

        blackKing = 'k',//король
        blackQuen = 'q',//королева
        blackRook = 'r',//ладья
        blackBishop = 'b',//слон
        blackKnigth = 'n',//конь
        blackPawn = 'p'//пешка

    }
}
