using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.ViewModel.Account
{
    public class AccountListingHandler : IQueryHandler<AccountListingQuery, AccountListingViewModel>
    {
        private readonly IAllocationContextDb _db;

        public AccountListingHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public AccountListingViewModel Get(AccountListingQuery query)
        {
            
            var accounts = _db.Account.Where(o => o.UserId == query.UserId).OrderByDescending(o => o.Year).Select(o => new AccountViewModel
            {
                AccountId = o.AccountId,
                UserId = o.UserId,
                Year = o.Year,
                FilingStatus = o.FilingStatus,
                ReturnsStatus = o.RefundStatus,
                ReturnsStatusDate = o.ReturnsStatusDate,
                RefundStatus = o.ReturnsStatus,
                RefundStatusDate = o.ReturnsStatusDate,
                TotalExceptions = o.TotalExceptions,
                AGI = o.AGI,
                Deductions = o.Deductions,
                TaxesDue = o.TaxesDue,
                PaymentsMade = o.PaymentsMade,
                BalanceDue = o.BalanceDue,
                RefundDue = o.RefundDue
            }).ToList();

            AccountListingViewModel model = new AccountListingViewModel() { Accounts = accounts };
            return model;
        }
    }
}
