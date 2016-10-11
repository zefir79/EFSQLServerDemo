using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class PaymentMode
    {
        public virtual int PaymentModeId { get; set; }
        public virtual string PaymentModeCode { get; set; }
        public virtual string PaymentModeDescription { get; set; }
    }
}
