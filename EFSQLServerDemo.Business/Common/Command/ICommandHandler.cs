using System.Collections.Generic;

namespace EFSQLServerDemo.Business.Common.Command
{
    public interface ICommandHandler<TCommand, TResult>
    {
        TResult Process(TCommand command);
    }

    public interface ICommandHandler<TCommand>
    {
        void Process(TCommand command);
    }
}
