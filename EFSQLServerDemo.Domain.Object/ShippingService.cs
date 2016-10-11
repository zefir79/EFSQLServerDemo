using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class ShippingService
    {
        public virtual int ShippingServiceId { get; set; }
        public virtual string ShippingServiceCode { get; set; }
        public virtual string ShippingServiceDescription { get; set; }
    }
}
