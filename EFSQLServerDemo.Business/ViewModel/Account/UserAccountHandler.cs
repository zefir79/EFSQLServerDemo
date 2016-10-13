using System.Linq;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;
using DomainAccount = EFSQLServerDemo.Domain.Object.Account; // There is a namespace Account so use Alias to get the domain object
using System.Collections.Generic;

namespace EFSQLServerDemo.Business.ViewModel.Account
{
    public class UserAccountHandler : IQueryHandler<UserAccountQuery, UserAccountViewModel>
    {
        private readonly IAllocationContextDb _db;

        public UserAccountHandler(IAllocationContextDb db)
        {
            _db = db;
        }

        public UserAccountViewModel Get(UserAccountQuery query)
        {

            UserAccountViewModel userAccountViewModel = null;
            IEnumerable<DomainAccount> accounts = _db.Account.Where(o => o.UserId == query.UserId).ToList();
            DomainAccount previousYearAccount = null;
            var latestAccount = accounts.OrderByDescending(a => a.Year).FirstOrDefault();
            if (latestAccount.ReturnsStatus.Contains("Not Available") ||
                latestAccount.ReturnsStatus.Contains("Extension") ||
                latestAccount.ReturnsStatus.Contains("Filing Proposal"))
            {
                previousYearAccount = accounts.Where(a => a.Year == (latestAccount.Year - 1)).FirstOrDefault();
            }
            else
            {
                previousYearAccount = latestAccount;
            }

            var user = _db.User.Where(o => o.UserId == query.UserId).ToList().FirstOrDefault();

            string refundStatusImage = string.Empty;
            string refundStatusImageAlt = string.Empty;
            if (previousYearAccount.RefundStatus.Contains("Refund Approved"))
            {
                refundStatusImage = "scripts/app/assets/Refund-Apporved-01.png";
                refundStatusImageAlt = "Refund Approved";
            }
            else if (previousYearAccount.RefundStatus.Contains("Return Received"))
            {
                refundStatusImage = "scripts/app/assets/Return-Recieved-01.png";
                refundStatusImageAlt = "Return Received";
            }
            else
            {
                refundStatusImage = "scripts/app/assets/Refund-Sent-01.png";
                refundStatusImageAlt = "Return Sent";
            }

            if (user != null)
            {
                userAccountViewModel = new UserAccountViewModel
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    PrimarySSN = user.PrimarySSN,
                    SecondarySSN = user.SecondarySSN,
                    AccountId = previousYearAccount.AccountId,
                    Year = previousYearAccount.Year,
                    FilingStatus = previousYearAccount.FilingStatus,
                    ReturnsStatus = latestAccount.ReturnsStatus,
                    ReturnsStatusDate = latestAccount.ReturnsStatusDate,
                    RefundStatus = previousYearAccount.RefundStatus,
                    RefundStatusDate = previousYearAccount.RefundStatusDate,
                    TotalExceptions = previousYearAccount.TotalExceptions,
                    AGI = previousYearAccount.AGI,
                    Deductions = previousYearAccount.Deductions,
                    TaxesDue = previousYearAccount.TaxesDue,
                    PaymentsMade = previousYearAccount.PaymentsMade,
                    BalanceDue = previousYearAccount.BalanceDue,
                    RefundDue = previousYearAccount.RefundDue,
                    RefundStatusImage = refundStatusImage,
                    RefundStatusImageAlt = refundStatusImageAlt
                };

            }

            return userAccountViewModel;
        }
    }
}
