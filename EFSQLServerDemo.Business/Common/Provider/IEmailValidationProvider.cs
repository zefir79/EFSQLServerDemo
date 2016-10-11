using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.Common.Provider
{
    public interface IEmailValidationProvider
    {
        bool IsValidEmail(string emailAddress);
    }
}
