using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.Common.Query
{
    public interface IQueryHandler<TResult>
    {
        TResult Get();
    }

    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Get(TQuery query);
    }
}
