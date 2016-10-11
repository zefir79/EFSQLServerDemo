using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.ViewModel.Account
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public int Year { get; set; }
        public string FilingStatus { get; set; }
        public string ReturnsStatus { get; set; }
        public DateTime? ReturnsStatusDate { get; set; }
        public string RefundStatus { get; set; }
        public DateTime? RefundStatusDate { get; set; }
        public int? TotalExceptions { get; set; }
        public decimal AGI { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? TaxesDue { get; set; }
        public decimal? PaymentsMade { get; set; }
        public decimal? BalanceDue { get; set; }
        public decimal? RefundDue { get; set; }
    }
}
