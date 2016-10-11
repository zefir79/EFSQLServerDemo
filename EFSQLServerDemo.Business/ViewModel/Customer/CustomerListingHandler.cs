using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.ViewModel.Customer
{
    public class CustomerListingHandler : IQueryHandler<CustomerListingQuery, CustomerListingViewModel>
    {
        private readonly IAllocationContextDb _db;

        public CustomerListingHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public CustomerListingViewModel Get(CustomerListingQuery query)
        {
            var vm = CreateCustomerListingViewModel(query);
            LoadCustomersForASpecificZipCode(vm, query);
            return vm;
        }

        private static CustomerListingViewModel CreateCustomerListingViewModel(CustomerListingQuery query)
        {
            return new CustomerListingViewModel() { ZipCode =  query.ZipCode};
        }

        private void LoadCustomersForASpecificZipCode(CustomerListingViewModel vm, CustomerListingQuery query)
        {
            var customers = _db.Customer.Where(o => o.ZipCode == query.ZipCode && o.EndDate == null).Select(o => new CustomerList
            {
                CustomerId = o.CustomerId,
                LoginId = o.LoginId,
                FirstName = o.FirstName,
                LastName = o.LastName
            }).ToList();
            vm.Customers = customers;
        }
    }
}
