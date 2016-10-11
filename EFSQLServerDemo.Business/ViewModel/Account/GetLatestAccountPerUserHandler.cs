using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.ViewModel.Account
{
    public class GetLatestAccountPerUserHandler : IQueryHandler<GetLatestAccountPerUserQuery, AccountViewModel>
    {
        private readonly IAllocationContextDb _db;

        public GetLatestAccountPerUserHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public AccountViewModel Get(GetLatestAccountPerUserQuery query)
        {
            AccountViewModel accountViewModel = null;
            var accountInternal = _db.Account.Where(o => o.UserId == query.UserId 
                                               && !o.ReturnsStatus.Contains("Not Available")
                                               && !o.ReturnsStatus.Contains("Extension") 
                                               && !o.ReturnsStatus.Contains("Filing Proposal")).OrderByDescending(a => a.Year).ToList().FirstOrDefault();
            if (accountInternal != null)

            {
                accountViewModel = new AccountViewModel
                {
                    AccountId = accountInternal.AccountId,
                    UserId = accountInternal.UserId,
                    Year = accountInternal.Year,
                    FilingStatus = accountInternal.FilingStatus,
                    ReturnsStatus = accountInternal.RefundStatus,
                    ReturnsStatusDate = accountInternal.ReturnsStatusDate,
                    RefundStatus = accountInternal.ReturnsStatus,
                    RefundStatusDate = accountInternal.ReturnsStatusDate,
                    TotalExceptions = accountInternal.TotalExceptions,
                    AGI = accountInternal.AGI,
                    Deductions = accountInternal.Deductions,
                    TaxesDue = accountInternal.TaxesDue,
                    PaymentsMade = accountInternal.PaymentsMade,
                    BalanceDue = accountInternal.BalanceDue,
                    RefundDue = accountInternal.RefundDue
                };

            }

                    
            return accountViewModel;
        }
    }
}
