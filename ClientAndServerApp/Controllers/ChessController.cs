using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class ChessController : ApiController
    {
        private ModelChessDB db = new ModelChessDB();

        private GameInfo info = new GameInfo();
        public JsonResult<GameInfo> GetChess(int userid)
        {
            Logic logic = new Logic();
            Game game = logic.GetCurrentGame(userid);
            Side side = logic.GetSide(userid);
            
            GameInfo gameInfo = logic.InitGameInfo(game,side);
                

            JsonResult<GameInfo> jsonResult = Json(gameInfo);
            return jsonResult;
        }


        public JsonResult<GameInfo> GetMove(int userid,int id,string move)
        {
            Logic logic = new Logic();
            Game game = logic.MakeMove(id,userid,move);
            Side side = logic.GetSide(userid);
            GameInfo gameInfo = logic.InitGameInfo(game, side);


            JsonResult<GameInfo> jsonResult = Json(gameInfo);
            return jsonResult;
        }


        //public JsonResult<Game> GetChess(int id,int userid)
        //{
        //    Logic logic = new Logic();
        //    Game game = logic.GetGame(id);
        //    JsonResult<Game> jsonResult = Json(game);
        //    return jsonResult;
        //}


    }
}
