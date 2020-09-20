using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp.Models
{
    public class GameInfo
    {
        public int GameID;
        public string FEN;
        public string Status;
        public int PlayerID;
        public string NickName;
        public string YourColor;
        public bool IsYourMove;
        public string Winner = "";
        public string OfferDraw = "";
        public GameInfo()
        {

        }
        public GameInfo(int gameId, string fen, string status,int playerId ,string nickname, string yourColor, bool isYourMove, string winner, string offerDraw)
        {
            GameID = gameId;
            FEN = fen;
            Status = status;
            PlayerID = playerId;
            NickName = nickname;
            YourColor = yourColor;
            IsYourMove = isYourMove;
            Winner = winner;
            OfferDraw = offerDraw;
        }
    }
}