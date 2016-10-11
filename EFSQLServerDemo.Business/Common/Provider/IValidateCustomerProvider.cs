using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.Common.Provider
{
    public interface IValidateCustomerProvider
    {
        bool IsExistingCustomer(int customerId);
    }
}
