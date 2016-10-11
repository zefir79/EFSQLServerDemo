using System;
using System.Collections.Generic;

namespace EFSQLServerDemo.Domain.Object
{
    public class User
    {
        public virtual int UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int PrimarySSN { get; set; }
        public virtual int? SecondarySSN { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
