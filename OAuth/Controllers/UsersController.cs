using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using OAuth.Models;

namespace OAuth.Controllers
{
    public class UsersController : Controller
    {
        public JsonResult GetUserInformation()
        {
            var headerValue = Convert.ToString(Request.Headers["Authorization"]);
            var tokenValue = headerValue.Substring(6);
            var token = HttpContext.Application.Get(tokenValue) as AccessToken;

            if (token.Token == null || token.ExpirationDate <= DateTime.Now)
                 throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var userInformation = Users.GetUserInformation(token.User);

            if (userInformation == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return Json(userInformation, JsonRequestBehavior.AllowGet);
        }
    }
}
