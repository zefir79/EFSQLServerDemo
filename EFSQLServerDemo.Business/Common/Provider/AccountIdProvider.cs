using System.Linq;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.Common.Provider
{
    public class AccountIdProvider : IAccountIdProvider
    {
        private readonly IAllocationContextDb _db;

        public AccountIdProvider(IAllocationContextDb db)
        {
            _db = db;
        }
        public string GetNextAccountId()
        {
            //var result = _db.Customer.OrderByDescending(o => o.CustomerId).Select(o => o.AccountId);
            var result = _db.Customer.ToList();

            if (!result.Any()) return "A001";
            var accountId = result.First() + "1";
            return accountId; 
        }
    }
}
