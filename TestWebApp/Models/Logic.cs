using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ChessApi;

namespace TestWebApp.Models
{
    public class Logic
    {
        private ModelChessDB db;

        public Logic()
        {
            db = new ModelChessDB();
        }
        public Game GetCurrentGame(int userid)//получить текущую игру.
        {//получить текущую игру 
            Side side = db
                .Sides
                .Where(g => g.PlayerID == userid)
                .OrderBy(g => g.ID)
                .FirstOrDefault();
            if(side!=null)
            {
                return side.Game;
            }

            Game game = db
                .Games
                .Where(g => g.Status == "wait")
                .OrderBy(g => g.ID)
                .FirstOrDefault();
            if(game != null)
            {
                game = ConnectExistGame(userid,game);
                return game;
            }
            game = CreateNewGame(userid);
            return game;
        }

        private Game ConnectExistGame(int userid, Game game){//подключиться к существующей игре

            game.Status = "play";
            db.Entry(game).State = System.Data.Entity.EntityState.Modified;

            Player player = db.Players.Find(userid);
            if(player == null)
            {
                player = new Player() { ID = userid, Name = "testBlack" };
                db.Players.Add(player);
            }
            db.SaveChanges();
            Side side = new Side();
            side.Player = player;
            side.Game = game;
            side.YourColor = "black";
            side.IsYourMove = 0;
            

            
            db.Sides.Add(side);

            db.SaveChanges();

            return game;
        }

        public GameInfo InitGameInfo(Game game ,Side side)//получить GameInfo из базы данных о запрошенном игроке.
        {
            return new GameInfo(gameId: game.ID, fen: game.FEN, status: game.Status, playerId: side.PlayerID, nickname: 
                side.Player.Name,yourColor:side.YourColor,isYourMove:side.IsYourMove==1?true:false,winner:"",offerDraw:"");
        }
        private Game CreateNewGame(int userid)//создать новую игру
        {
            Game game = new Game();
            Chess chess = new Chess();
            Player player = db.Players.Find(userid);
            if(player==null)
            {
                player = new Player() { ID = userid, Name = "testWhite" };
                db.Players.Add(player);
            }
            game.FEN = chess.fen;
            game.Status = "wait";

            Side side = new Side();
            side.GamesID = game.ID;
            side.PlayerID = userid;
            side.YourColor = "white";
            side.IsYourMove = 1;//1 = true , 0 - false;

            db.Games.Add(game);
           
            db.Sides.Add(side);
            
            db.SaveChanges();

            return game;
        }

        
        public Game GetGame(int id)//получить игру по id игры
        {
            return db.Games.Find(id);
        }

        public Side GetSide(int userid)//получить сторону по id игрока
        {
            Side side = db
                .Sides
                .Where(g=>g.PlayerID == userid)
                .OrderBy(g => g.ID)
                .FirstOrDefault();
            return side;
        }

        internal Game MakeMove(int id,int userid,string move){//сделать ход 
            Side side= GetSide(userid);
            Side temp;
            Game game = GetGame(id);
            if(game == null || side.IsYourMove == 0 || game.Status =="done" || game.Status == "wait")
                return game;
            Chess chess = new Chess(game.FEN);
            Chess chessNext = chess.Move(move);
            if(chessNext.fen == game.FEN)
            { 
                return game;
            }
            side.IsYourMove = 0;
            if(side.YourColor == "white")
            {
                temp = game.Sides.Where(g => g.YourColor == "black").FirstOrDefault();
                temp.IsYourMove = 1;
                db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
            }
            else 
            { 
                temp = game.Sides.Where(g => g.YourColor == "white").FirstOrDefault();
                temp.IsYourMove = 1;
                db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
              
            }
            List<string> list = chessNext.GetAllMoves();
            if(list.Count == 0)
            {
                game.Status = "done";
            }
            game.FEN = chessNext.fen;
            db.Entry(game).State = System.Data.Entity.EntityState.Modified;
            db.Entry(side).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return game;

        }

        public bool RestartOrNewGame(int userid){//начать новую игру
            Player player = db.Players.Find(userid);
            if(player != null)
            {
                IQueryable<Side> sides = db.Sides.Where(g => g.PlayerID == userid);
                if(sides!=null)
                    foreach (Side side in sides)
                    {
                        side.Game.Status = "done";
                        db.Sides.Remove(side);
                    }
                db.SaveChanges();
            }
            
            return true;
        }
    }
}