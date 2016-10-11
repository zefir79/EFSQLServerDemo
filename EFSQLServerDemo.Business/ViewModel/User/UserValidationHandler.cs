using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.ViewModel.User
{
    public class UserValidationHandler : IQueryHandler<UserValidationQuery, bool>
    {
        private readonly IAllocationContextDb _db;

        public UserValidationHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public bool Get(UserValidationQuery query)
        {
          
            var isUserValid = false;

            var result = _db.User.Where(o => o.UserName == query.UserName && o.Password == query.Password);
       
            isUserValid = result.Any() ? true : false;

            return isUserValid;
        }

        public SessionUser GetUser(GetSessionUserQuery query)
        {

            SessionUser sessionUser = null;

            var user = _db.User.Where(o => o.UserName == query.UserName).FirstOrDefault();

            if (user != null)
            {
                sessionUser = new SessionUser
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName
                };

            }

            return sessionUser;
        }
    }
}
