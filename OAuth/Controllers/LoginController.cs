using System;
using System.Net;
using System.Web.Mvc;
using OAuth.Models;

namespace OAuth.Controllers
{
    public class LoginController : Controller
    {
        private ClientData client_data;

        public LoginController()
        {
            client_data = ClientData.ReadFromConfig();
        }

        [HttpGet]
        public ActionResult Authorize(String client_id, String redirect_uri, String state, String error)
        {
            if (error == "InvalidLoginAttempt")
                ViewBag.ErrorMessage = "Invalid Login Attempt";

            if (!client_data.ValidateId(client_id, redirect_uri))
                return new HttpNotFoundResult();

            HttpContext.Session.Add("client_id", client_id);
            HttpContext.Session.Add("redirect_uri", redirect_uri);
            HttpContext.Session.Add("state", state);

            return View();
        }
        
        [HttpPost]
        public ActionResult Authenticate(String loginName, String password)
        {
            if (HttpContext.Session["client_id"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if (!Users.Authenticate(loginName, password))
                return Redirect(String.Format("/Login/OAuth/Authorize{0}&Error=InvalidLoginAttempt", Request.UrlReferrer.Query));

            var urlRedirect = HttpContext.Session["redirect_uri"] as String;

            if (String.IsNullOrEmpty(urlRedirect))
                urlRedirect = client_data.RedirectUri;

            var code = Convert.ToString(Guid.NewGuid());
            HttpContext.Application.Add(code, loginName);
            urlRedirect = String.Format("{0}?code={1}", urlRedirect, code);

            var state = HttpContext.Session["state"] as String;

            if (!String.IsNullOrEmpty(state))
                urlRedirect += "&state=" + state;

            return Redirect(urlRedirect);
        }

        [HttpPost]
        public ActionResult AccessToken(String client_id, String client_secret, String code)
        {
            if (!client_data.ValidateSecret(client_secret, client_id))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if (code == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var loginName = HttpContext.Application.Get(code) as String;

            if (String.IsNullOrEmpty(loginName))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var token = new AccessToken(Convert.ToString(Guid.NewGuid()), loginName, DateTime.Now.AddMinutes(15));

            HttpContext.Application.Add(token.Token, token);

            return Json(new { access_token = token }, JsonRequestBehavior.AllowGet);
        }
    }
}
