using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class AddressType
    {
        public virtual int AddressTypeId { get; set; }
        public virtual string AddressTypeCode { get; set; }
        public virtual string AddressTypeDescription { get; set; }
    }
}
