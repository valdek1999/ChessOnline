using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ClientChessAPI
{
    public class ChessClient
    {
        public string host { get; set; }

        public int user { get; private set; }

        private string pattern = @"""(\w+)\"":""?([^,""]*)""?";

        public int CurrentID { get; set; } = -1;

        public InfoGame info { get; private set; }

        public ChessClient (string host, int user){
            this.user = user;
            this.host = host;
        }
        public InfoGame GetGame(bool restart){
            NameValueCollection list;
            if(!restart)
                list = JsonParser(CallServer("Chess", ""));
            else
            { 
                JsonParser(CallServer("Restart", ""));
                list = JsonParser(CallServer("Chess", ""));
            }
            CurrentID = Convert.ToInt32(list["GameID"]);
            return info = new InfoGame(list);
        }
        public void CloseGame()
        {
            JsonParser(CallServer("Restart", ""));
        }
        public InfoGame MakeMove(string move)
        {
            if(CurrentID==-1)
            {
                return new InfoGame() { GameID = -1};
            }
            NameValueCollection list;
            list = JsonParser(CallServer("Chess", CurrentID + "/" + move));

            return info = new InfoGame(list);
        }



        private string CallServer (string controller,string param = "")//web-client - возращает ответ веб-сервера по заданному контроллеру и его пути url 
        {
            WebClient req = new WebClient();
            
            //WebRequest request = WebRequest.Create(host + controller + "/" + user + "/" + param);//создает запрос на данный url 
            //WebResponse response = request.GetResponse();//получает ответ на этот интернет запрос
            using(Stream stream = req.OpenRead(host + controller + "/" + user + "/" + param))//переводит его в Stream -в поток байтов для последующей обработки
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private NameValueCollection JsonParser (string jsInput){
            NameValueCollection list = new NameValueCollection();
            foreach(Match m in Regex.Matches(jsInput, pattern))
                if (m.Groups.Count == 3)
                    list[m.Groups[1].Value] = m.Groups[2].Value;//по нужнему патерну выделяются группы у json ответа от сервера и назначаются ассоциативной коллекции - нужный элемент
            return list;
        }





    }
}
