using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Business.Common.Provider;
using EFSQLServerDemo.Business.Command.Customer.Save;
using EFSQLServerDemo.Business.Common.Command;
using EFSQLServerDemo.Business.Test.DbIntegrationTest;

using NSubstitute;

namespace EFSQLServerDemo.Business.Test.UnitTest.CommandTest.CustomerTest.Save
{
    [TestClass]
    [Category("Db_Free")]
    public class SaveCustomerHandlerTestNoDb : TestBase
    {
        private IAllocationContextDb _dbContext;
        private IAccountIdProvider _accountIdProvider;
        private IEmailValidationProvider _emailValidationProvider;
        private IDbSet<Domain.Object.Customer> _customer;

        private ICommandHandler<SaveCustomer, CommandHandlerResult> _command;

        private const string LoginId = "a.bc@abc.com";

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = Substitute.For<IAllocationContextDb>();

            _accountIdProvider = new AccountIdProvider(_dbContext);
            var accountId = _accountIdProvider.GetNextAccountId();

            _emailValidationProvider = Substitute.For<IEmailValidationProvider>();
            _emailValidationProvider.IsValidEmail(LoginId).Returns(true);

            _customer = new FakeDbSet<Domain.Object.Customer>();

            _dbContext.Customer.Returns(_customer);
        }

        [TestMethod]
        public void ShouldSuccessfullySaveACustomer()
        {
            Describes("Successfully inserting a customer.");
            Given_That_I_Have_A_Customer_Save_Handler();
            When_I_Try_To_Insert_A_Customer();
            Then_I_Should_Be_Able_To_Successfully_Insert();

        }

        private void Given_That_I_Have_A_Customer_Save_Handler()
        {
            _command = new SaveCustomerHandler(_dbContext, _emailValidationProvider, _accountIdProvider);
        }

        private void When_I_Try_To_Insert_A_Customer()
        {
            // Active customer in zip 20850.
            _command.Process(new SaveCustomer
            {
                CustomerId = -1,
                LoginId = LoginId,
                FirstName = "Say",
                LastName = "Save",
                FriendlyName = "SSS",
                Address1 = "123 Main Street",
                City = "Rockville",
                StateId = 1,
                ZipCode = "20850",
                Phone = 1234567890
            });
            _customer.Add(_dbContext.Customer.FirstOrDefault());
        }

        private void Then_I_Should_Be_Able_To_Successfully_Insert()
        {
            Assert.AreEqual(LoginId, _customer.FirstOrDefault().LoginId, "Login Id should match.");
        }

    }
}
