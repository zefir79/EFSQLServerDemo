using System;
using System.Collections.ObjectModel;
using EFSQLServerDemo.Business.Common.Command;
using EFSQLServerDemo.Business.Common.Provider;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Command.Order.Save
{
    public class SaveOrderHandler : ICommandHandler<Domain.Object.Order, CommandHandlerResult>
    {
        private readonly IAllocationContextDb _db;
        private readonly IValidateCustomerProvider _customerProvider;

        public SaveOrderHandler(IAllocationContextDb db, IValidateCustomerProvider customerProvider)
        {
            _db = db;
            _customerProvider = customerProvider;
        }

        public CommandHandlerResult Process(Domain.Object.Order command)
        {
            var result = new CommandHandlerResult { IsValid = true };

            if (command.OrderId > 0)
            {
                //Edit
            }
            else
            {
                //Add new
                result = ValidateForSave(command);
                if (result.IsValid)
                {
                    command = SaveForCreate(command);
                }
            }
            _db.SaveChanges();
            result.Id = command.OrderId;
            return result;

        }

        private Domain.Object.Order SaveForCreate(Domain.Object.Order command)
        {
            _db.Order.Add(command);
            return command;
        }

        #region "Validation"
        private CommandHandlerResult ValidateForSave(Domain.Object.Order command)
        {
            //Validate email
            return ValidateOrderForSave(command);
        }

        private CommandHandlerResult ValidateOrderForSave(Domain.Object.Order command)
        {
            var commandHandlerResult = new CommandHandlerResult { IsValid = true };

            if (_customerProvider.IsExistingCustomer(command.CustomerId)) return commandHandlerResult;
            var valMsg = new CommandHandlerValidation { ValidationMessage = "There is no active customer with this Id." };
            commandHandlerResult.IsValid = false;
            commandHandlerResult.CommandHandlerValidations = new Collection<CommandHandlerValidation> { valMsg };

            return commandHandlerResult;
        }
        #endregion

    }
}
