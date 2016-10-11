using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.ViewModel.User
{
    public class GetUserHandler : IQueryHandler<GetUserQuery, UserViewModel>
    {
        private readonly IAllocationContextDb _db;

        public GetUserHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public UserViewModel Get(GetUserQuery query)
        {

            UserViewModel userViewModel = null;

            var result = _db.User.Where(o => o.UserId == query.UserId).ToList();
            var user = result.FirstOrDefault();

            if (user != null)
            {
                userViewModel = new UserViewModel
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    PrimarySSN = user.PrimarySSN,
                    SecondarySSN = user.SecondarySSN
                };

            }

            return userViewModel;
        }
    }
}
