using System;

namespace EFSQLServerDemo.Domain.Object
{
    public class Account
    {
        public virtual int AccountId { get; set; }
        public virtual User User { get; set; }
        public virtual int UserId { get; set; }
        public virtual int Year { get; set; }
        public virtual string FilingStatus { get; set; }
        public virtual string ReturnsStatus { get; set; }
        public virtual DateTime? ReturnsStatusDate { get; set; }
        public virtual string RefundStatus { get; set; }
        public virtual DateTime? RefundStatusDate { get; set; }
        public virtual int? TotalExceptions { get; set; }
        public virtual decimal AGI { get; set; }
        public virtual decimal? Deductions { get; set; }
        public virtual decimal? TaxesDue { get; set; }
        public virtual decimal? PaymentsMade { get; set; }
        public virtual decimal? BalanceDue { get; set; }
        public virtual decimal? RefundDue { get; set; }
    }
}
