using System.Web.Mvc;
using EFWebAppPrototype.Models;
using EFSQLServerDemo.Business.ViewModel.User;
using System.Web.Security;

namespace EFWebAppPrototype.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private UserValidationHandler _valUserHandler;
        private GetSessionUserHandler _getSessionUserHandler;
        public AccountController(UserValidationHandler valUserHandler, GetSessionUserHandler getSessionUserHandler)
        {
            _valUserHandler = valUserHandler;
            _getSessionUserHandler = getSessionUserHandler;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _valUserHandler.Get(new UserValidationQuery { UserName = model.Username, Password = model.Password });
            if (result)
            {
                this.ActiveUser = _getSessionUserHandler.Get(new GetSessionUserQuery { UserName = model.Username });

                FormsAuthentication.SetAuthCookie(this.ActiveUser.UserId.ToString(), false);
                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("Login", "Invalid username or password. Please try again.");
    
            return View(model);
        }


        // POST: /Account/LogOut
        [CheckUserSessionAttribute]
        [Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {

            HttpContext.Session.RemoveAll();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}
