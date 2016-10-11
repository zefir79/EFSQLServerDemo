using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class OrderAddress
    {
        public virtual int OrderAddressId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
        public virtual AddressType AddressType { get; set; }
        public virtual int AddressTypeId { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual State State { get; set; }
        public virtual int StateId { get; set; }
        public virtual string ZipCode { get; set; }

    }
}
