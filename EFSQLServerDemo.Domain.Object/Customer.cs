using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Domain.Object
{
    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual string LoginId { get; set; }
        public virtual string AccountId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FriendlyName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual State State { get; set; } 
        public virtual int StateId { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual int Phone { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
