using System;
using System.Collections.Generic;
using EFSQLServerDemo.Domain.Object;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.ViewModel.Account
{
    public class AccountListingViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }

}
