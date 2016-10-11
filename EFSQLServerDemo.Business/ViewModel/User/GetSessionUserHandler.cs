using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.ViewModel.User
{
    public class GetSessionUserHandler : IQueryHandler<GetSessionUserQuery, SessionUser>
    {
        private readonly IAllocationContextDb _db;

        public GetSessionUserHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public SessionUser Get(GetSessionUserQuery query)
        {

            SessionUser sessionUser = null;

            var result = _db.User.Where(o => o.UserName == query.UserName).ToList();
            var user = result.FirstOrDefault();

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
