using EFSQLServerDemo.Business.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EFSQLServerDemo.Business.ViewModel.Account;
using EFWebAppPrototype.Utilities;
//using EFSQLServerDemo.Business.ViewModel.User;

namespace EFWebAppPrototype.Controllers
{

    [Authorize]
    [RoutePrefix("api/data")]
    public class DataController : BaseApiController
    {
        private GetLatestAccountPerUserHandler _getLatestAccountPerUserHandler;
        private GetUserHandler _getUserHandler;
        private AccountListingHandler _accountListingHandler;
        private UserAccountHandler _userAccountPerUserHandler;
        private int _userId;

        public DataController(GetLatestAccountPerUserHandler getLatestAccountPerUserHandler,
                              GetUserHandler getUserHandler,
                              AccountListingHandler accountListingHandler,
                              UserAccountHandler userAccountPerUserHandler)
        {
            _getLatestAccountPerUserHandler = getLatestAccountPerUserHandler;
            _getUserHandler = getUserHandler;
            _accountListingHandler = accountListingHandler;
            _userAccountPerUserHandler = userAccountPerUserHandler;
            _userId = ConvertToInt(RequestContext.Principal.Identity.Name);
        }

        [HttpGet]
        [Route("useraccount")]
        public HttpResponseMessage GetUserAccount(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserAccountViewModel userAccountViewModel = _userAccountPerUserHandler.Get(new UserAccountQuery { UserId = _userId });
                response = request.CreateResponse<UserAccountViewModel>(HttpStatusCode.OK, userAccountViewModel);
                return response;
            });
        }

        //Param in userId 
        [HttpGet]
        [Route("latestaccount")]
        public HttpResponseMessage GetLatestAccountPerUser(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AccountViewModel accountViewModel = _getLatestAccountPerUserHandler.Get(new GetLatestAccountPerUserQuery { UserId = _userId });
                response = request.CreateResponse<AccountViewModel>(HttpStatusCode.OK, accountViewModel);

                return response;
            });
        }

        [HttpGet]
        [Route("user")]
        public HttpResponseMessage GetUser(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                UserViewModel userViewModel = _getUserHandler.Get(new GetUserQuery { UserId = _userId });
                response = request.CreateResponse<UserViewModel>(HttpStatusCode.OK, userViewModel);

                return response;
            });
        }

        [HttpGet]
        [Route("accounts")]
        public HttpResponseMessage GetAccountsPerUser(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                AccountListingViewModel accountListingViewModel = _accountListingHandler.Get(new AccountListingQuery { UserId = _userId });
                response = request.CreateResponse<AccountListingViewModel>(HttpStatusCode.OK, accountListingViewModel);

                return response;
            });

        }

        public static int ConvertToInt(string strIn)
        {
            int parsedInt = -1;
            if (!String.IsNullOrEmpty(strIn))
                int.TryParse(strIn, out parsedInt);
            return parsedInt;
        }

    }

}
