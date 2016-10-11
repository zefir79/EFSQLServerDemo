using System;
using System.Collections.Generic;
using EFSQLServerDemo.Domain.Object;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.ViewModel.Customer
{
    public class CustomerListingViewModel
    {
        public string ZipCode { get; set; }
        public ICollection<CustomerList> Customers { get; set; }
    }

    public class CustomerList
    {
        public int CustomerId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
