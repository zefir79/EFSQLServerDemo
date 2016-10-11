using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFSQLServerDemo.Business.ViewModel.User;
using EFSQLServerDemo.Domain.Object;
using EFSQLServerDemo.Domain.Repository;
using NSubstitute;

namespace EFSQLServerDemo.Business.Test.UnitTest.ViewModelTest.UserTest
{
    [TestClass]
    [Category("Db_Free")]
    public class UserValidationHandlerTestNoDb
    {
        private IAllocationContextDb _dbContext;

        private UserValidationHandler _handler;

        User user = null;

        private const string UserName = "a.bc@abc.com";
        private const string Password = "testAcc@1";

        bool isUserValid = false;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = Substitute.For<IAllocationContextDb>();
            //Insert an user
            Insert_User();
        }

        [TestMethod]
        public void ShouldValidateUser()
        {
            Given_That_I_Have_A_User_Validation_Handler();
            When_I_Try_To_Fetch_A_Specific_User();
            Then_The_User_Should_Be_Valid();
        }

        [TestMethod]
        public void ShouldFailValidationOfAInvalidUser()
        {
            Given_That_I_Have_A_User_Validation_Handler();
            When_I_Try_To_Fetch_An_Invalid_User();
            Then_The_User_Should_Be_InValid();
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        private void Given_That_I_Have_A_User_Validation_Handler()
        {
            _handler = new UserValidationHandler(_dbContext);
        }

        private void When_I_Try_To_Fetch_A_Specific_User()
        {
            var query = new UserValidationQuery { UserName = UserName, Password =  Password};
            isUserValid = _handler.Get(query);
        }

        private void Then_The_User_Should_Be_Valid()
        {
            Assert.AreEqual(true, isUserValid, "The user validation passed.");
        }

        private void When_I_Try_To_Fetch_An_Invalid_User()
        {
            var query = new UserValidationQuery { UserName = "abc", Password = Password };
            isUserValid = _handler.Get(query);
        }

        private void Then_The_User_Should_Be_InValid()
        {
            Assert.AreEqual(false, isUserValid, "The user validatiopn failed.");
        }

        private void Insert_User()
        {
            IDbSet<User> users = new FakeDbSet<User>();

            // Insert an user.
            user = new DataBuilder.UserBuilder()
                .WithUserName(UserName)
                .WithPassword(Password)
                .Build();
            users.Add(user);

            _dbContext.User.ReturnsForAnyArgs(users);
        }
    }
}
