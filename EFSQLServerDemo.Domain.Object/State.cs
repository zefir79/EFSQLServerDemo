using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class State
    {
        public virtual int StateId { get; set; }
        public virtual string StateCode { get; set; }
        public virtual string StateDescription { get; set; }
    }
}
