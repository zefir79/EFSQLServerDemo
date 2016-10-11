using System.Linq;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.Common.Provider
{
    public class ValidateCustomerProvider : IValidateCustomerProvider
    {
        private readonly IAllocationContextDb _db;
        public ValidateCustomerProvider(IAllocationContextDb db)
        {
            _db = db;
        }

        public bool IsExistingCustomer(int customerId)
        {
            var result = _db.Customer.Where(o => o.CustomerId == customerId && o.EndDate == null).Select(o => o.CustomerId);
            return (result.Any() ? true : false);
        }
    }
}
