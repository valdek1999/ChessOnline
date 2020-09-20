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
    public class RestartController : ApiController
    {
        public JsonResult<bool> GetRestart(int userid)
        {
            Logic logic = new Logic();
            JsonResult<bool> jsonResult = Json(logic.RestartOrNewGame(userid));
            return jsonResult;
        }
    }
}
