using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChessAPI
{
    public struct InfoGame
    { 
        public int GameID;//id игры 
        public string FEN;// fen - шахматной доски
        public string Status;//статус игры wait/play/done
        public int PlayerID;//пользователь
        public string NickName;//ник-нейм
        public string YourColor;//какой цвет
        public bool IsYourMove;//чей ход
        public string Winner;//победитель
        public string OfferDraw; //кто предложил ничью
        public InfoGame(NameValueCollection list)
        {
            GameID = int.Parse(list["GameID"]);
            FEN = list["FEN"];
            Status=list["Status"];
            PlayerID=int.Parse(list["PlayerID"]);
            NickName = list["NickName"];
            YourColor=list["YourColor"];
            IsYourMove= bool.Parse(list["IsYourMove"]);
            Winner= list["Winner"];
            OfferDraw=list["OfferDraw"];

        }
        public override string ToString(){
            if (GameID == -1)
                return "Игра еще не создана";
            return $"{nameof(GameID)}: {GameID}\n" +
                   $"{nameof(FEN)}: {FEN}\n" +
                   $"{nameof(Status)}: {Status}\n" +
                   $"{nameof(PlayerID)}: {PlayerID}\n" +
                   $"{nameof(NickName)}: {NickName}\n" +
                   $"{nameof(YourColor)}: {YourColor}\n" +
                   $"{nameof(IsYourMove)}: {IsYourMove}\n" +
                   $"{nameof(Winner)}: {Winner}\n" +
                   $"{nameof(OfferDraw)}: {OfferDraw}";
        }
    }

    
    

}
