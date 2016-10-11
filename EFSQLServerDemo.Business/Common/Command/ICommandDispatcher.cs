using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLServerDemo.Business.Common.Command
{
    public interface ICommandDispatcher
    {
        void Execute<TCommand, TResult>(TCommand command, out TResult result) where TCommand : class;
        TResult Execute<TCommand, TResult>(TCommand command) where TCommand : class;
        void Execute<TCommand>(TCommand command) where TCommand : class;

        /// <summary>
        /// query handler with no arguments, resolved by its return type
        /// </summary>
        TResult Get<TResult>();

        /// <summary>
        /// both types must be specified explicitly during invocation
        /// </summary>
        TResult Get<TQuery, TResult>(TQuery command);

        /// <summary>
        /// both types can be inferred from parameters, but have to use the "out"
        /// </summary>
        void Get<TQuery, TResult>(TQuery command, out TResult result);

        /// <summary>
        /// the Result type must be specified, and the Query type will be resolved at run time
        /// </summary>
        TResult Get<TResult>(object query);

        dynamic Handle<THandler>(dynamic query);
    }
}
