using System;
using System.Collections.ObjectModel;
using EFSQLServerDemo.Business.Common.Command;
using EFSQLServerDemo.Business.Common.Provider;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Command.Customer.Save
{
    public class SaveCustomerHandler : ICommandHandler<SaveCustomer, CommandHandlerResult>
    {
        private readonly IAllocationContextDb _db;
        private readonly IEmailValidationProvider _emailValidationProvider;
        private readonly IAccountIdProvider _accountIdProvider;

        public SaveCustomerHandler(IAllocationContextDb db, IEmailValidationProvider emailValidationProvider, IAccountIdProvider accountIdProvider)
        {
            _db = db;
            _emailValidationProvider = emailValidationProvider;
            _accountIdProvider = accountIdProvider;
        }

        public CommandHandlerResult Process(SaveCustomer command) 
        {
            var customer = new Domain.Object.Customer();
            var result = new CommandHandlerResult { IsValid = true };

            if (command.CustomerId > 0)
            {
                //Edit
            }
            else
            {
                //Add new
                result = ValidateForSave(command);
                if (result.IsValid)
                {
                    customer = SaveForCreate(command);
                }
            }
            _db.SaveChanges();
            result.Id = customer.CustomerId;
            return result;
        }

        private Domain.Object.Customer SaveForCreate(SaveCustomer command)
        {
            var customer = new Domain.Object.Customer
            {
                LoginId = command.LoginId,
                AccountId = _accountIdProvider.GetNextAccountId(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                FriendlyName = command.FriendlyName,
                Address1 = command.Address1,
                Address2 = command.Address2,
                City = command.City,
                StateId = command.StateId,
                ZipCode = command.ZipCode,
                StartDate = DateTime.Now,
                Phone = command.Phone
            };
            _db.Customer.Add(customer);
            return customer;
        }

        private Domain.Object.Customer SaveForEdit(SaveCustomer command)
        {
            var customer = new Domain.Object.Customer();
            customer = _db.Customer.Find(command.CustomerId);
            customer.FirstName = command.FirstName;
            customer.LastName = command.LastName;
            customer.LoginId = command.LoginId;
            customer.FriendlyName = command.FriendlyName;
            customer.Address1 = command.Address1;
            customer.Address2 = command.Address2;
            customer.City = command.City;
            customer.StateId = command.StateId;
            customer.ZipCode = command.ZipCode;
            customer.Phone = command.Phone;
            return customer;
        }

        #region "Validation"
        private CommandHandlerResult ValidateForSave(SaveCustomer command)
        {
            //Validate email
            return ValidateCustomer(command);
        }

        private CommandHandlerResult ValidateCustomer(SaveCustomer command)
        {
            var commandHandlerResult = new CommandHandlerResult { IsValid = true };

            if (_emailValidationProvider.IsValidEmail(command.LoginId)) return commandHandlerResult;
            var valMsg = new CommandHandlerValidation { ValidationMessage = "Invalid loginId." };
            commandHandlerResult.IsValid = false;
            commandHandlerResult.CommandHandlerValidations = new Collection<CommandHandlerValidation> { valMsg };

            return commandHandlerResult;
        }

        //private CommandHandlerResult ValidateForEdit(SaveCustomer command)
        //{

        //}
        #endregion
    }
}
