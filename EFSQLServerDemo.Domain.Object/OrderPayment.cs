using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class OrderPayment
    {
        public virtual int OrderPaymentId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual int PaymentModeId { get; set; }
        public virtual int CardNumber { get; set; }
        public virtual string CardName { get; set; }
        public virtual string CCV { get; set; }
        public virtual string ExpirationDate { get; set; }
        public virtual decimal PaymentAmount { get; set; }
        public virtual DateTime? ProcessingDate { get; set; }


    }
}
