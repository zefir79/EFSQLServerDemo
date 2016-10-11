using EFSQLServerDemo.Business.ViewModel.User;
using EFWebAppPrototype.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFWebAppPrototype.Controllers
{
    public class BaseController : Controller
    {

        protected BaseController()
        {
            this.ActiveUser = this.ActiveUser;
        }

        public virtual SessionUser ActiveUser
        {
            get
            {
                SessionUser activeUser = (SessionUser)StateProvider.GetObject(StateStrings.ActiveUserObject, false);

                return activeUser;
            }
            set
            {
                StateProvider.SetObject(StateStrings.ActiveUserObject, value, false);
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
        public class CheckUserSessionAttribute : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                SessionUser user = (SessionUser)StateProvider.GetObject(StateStrings.ActiveUserObject, false);
                //Check the Active user is set and the request is authenticated (if the user is on controller other that login)
                if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession)
                     || (filterContext.Controller != null &&
                     filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Login" &&
                     user != null && !filterContext.HttpContext.User.Identity.IsAuthenticated))
                {
                    //send them off to the login page
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Content("~/Account/Login");
                    session.RemoveAll();
                    session.Clear();
                    session.Abandon();
                    FormsAuthentication.SignOut();
                    filterContext.HttpContext.Response.Redirect(loginUrl, true);
                }
            }
        }


    }
}