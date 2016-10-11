using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFSQLServerDemo.Business.ViewModel.Customer;
using EFSQLServerDemo.Domain.Object;
using EFSQLServerDemo.Domain.Repository;
using NSubstitute;

namespace EFSQLServerDemo.Business.Test.UnitTest.ViewModelTest.CustomerTest
{
    [TestClass]
    [Category("Db_Free")]
    public class CustomerListingHandlerTestNoDb
    {
        private IAllocationContextDb _dbContext;

        private CustomerListingHandler _handler;
        CustomerListingViewModel vm = new CustomerListingViewModel();

        Customer customer1 = null; // Active customer in zip 20850.
        Customer customer2 = null; // Inactive customer in zip 20855.
        Customer customer3 = null; // Active customer in zip 20850.
        Customer customer4 = null; // Inactive customer in zip 20850.
        Customer customer5 = null; // Active customer in zip 20855.

        private const string LoginId1 = "a.bc@abc.com";
        private const string LoginId2 = "x.yz@xyz.com";
        private const string LoginId3 = "m.mo@mno.com";
        private const string LoginId4 = "de.f@def.com";
        private const string LoginId5 = "pq.r@pqr.com";
        private const string ZipCode1 = "20850";
        private const string ZipCode2 = "20855";
        private const string AccountId1 = "Acct1";
        private const string AccountId2 = "Acct2";
        private const string AccountId3 = "Acct3";
        private const string AccountId4 = "Acct4";
        private const string AccountId5 = "Acct5";

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = Substitute.For<IAllocationContextDb>();
            //Insert a couple of customers
            Insert_Customers();
        }

        [TestMethod]
        public void ShouldFetchCustomersForASpecificZipCode()
        {
            Given_That_I_Have_A_Customer_Listing_Handler();
            When_I_Try_To_Fetch_Customers_For_A_Specific_ZipCode();
            Then_I_Should_Fetch_The_Customers();
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        private void Given_That_I_Have_A_Customer_Listing_Handler()
        {
            _handler = new CustomerListingHandler(_dbContext);
        }

        private void When_I_Try_To_Fetch_Customers_For_A_Specific_ZipCode()
        {
            var query = new CustomerListingQuery { ZipCode = "20850" };
            vm = _handler.Get(query);
        }

        private void Then_I_Should_Fetch_The_Customers()
        {
            Assert.AreEqual(ZipCode1, vm.ZipCode, "The query should return valid zip code.");
            Assert.AreEqual(2, vm.Customers.Count(), "The query should return one valid record.");

            //Assert.AreEqual("John", vm.Customers.First().FirstName, "The query should return valid First Name.");
            //Assert.AreEqual("Doe", vm.Customers.First().LastName, "The query should return valid Last Name.");
            //Assert.AreEqual(LoginId1, vm.Customers.First().LoginId, "The query should return valid Login Id.");

            Assert.AreEqual("John", vm.Customers.First(o => o.FirstName.Contains("John")).FirstName, "The query should return valid First Name.");
            Assert.AreEqual("Doe", vm.Customers.First(o => o.LastName.Contains("Doe")).LastName, "The query should return valid Last Name.");
            Assert.AreEqual(LoginId1, vm.Customers.First(o => o.LoginId.Contains(LoginId1)).LoginId, "The query should return valid Login Id.");

            Assert.AreEqual("Banta", vm.Customers.First(o => o.FirstName.Contains("Banta")).FirstName, "The query should return valid First Name.");
            Assert.AreEqual("Singh", vm.Customers.First(o => o.LastName.Contains("Singh")).LastName, "The query should return valid Last Name.");
            Assert.AreEqual(LoginId3, vm.Customers.First(o => o.LoginId.Contains(LoginId3)).LoginId, "The query should return valid Login Id.");
        }


        private void Insert_Customers()
        {
            IDbSet<Customer> customers = new FakeDbSet<Customer>();

            // Active customer in zip 20850.
            customer1 = new DataBuilder.CustomerBuilder()
                .WithLoginId(LoginId1)
                .WithFirstName("John")
                .WithLastName("Doe")
                .WithAccountId(AccountId1)
                .WithAddress1("100 Main Street")
                .WithStateId(1)
                .WithZipCode(ZipCode1)
                .WithStartDate(DateTime.Now)
                .Build();
            customers.Add(customer1);

            // Inactive customer in zip 20855.
            customer2 = new DataBuilder.CustomerBuilder()
                .WithLoginId(LoginId2)
                .WithFirstName("Jane")
                .WithLastName("Doe")
                .WithAccountId(AccountId2)
                .WithAddress1("200 Main Street")
                .WithStateId(2)
                .WithZipCode(ZipCode2)
                .WithStartDate(DateTime.Now)
                .WithEndDate(DateTime.Now)
                .Build();
            customers.Add(customer2);

            // Active customer in zip 20850.
            customer3 = new DataBuilder.CustomerBuilder()
                .WithLoginId(LoginId3)
                .WithFirstName("Banta")
                .WithLastName("Singh")
                .WithAccountId(AccountId3)
                .WithAddress1("600 Main Street")
                .WithStateId(1)
                .WithZipCode(ZipCode1)
                .WithStartDate(DateTime.Now)
                .Build();
            customers.Add(customer3);

            // Inactive customer in zip 20850.
            customer4 = new DataBuilder.CustomerBuilder()
                .WithLoginId(LoginId4)
                .WithFirstName("Gary")
                .WithLastName("Sobers")
                .WithAccountId(AccountId4)
                .WithAddress1("650 Main Street")
                .WithStateId(1)
                .WithZipCode(ZipCode1)
                .WithStartDate(DateTime.Now)
                .WithEndDate(DateTime.Now)
                .Build();
            customers.Add(customer4);

            // Active customer in zip 20855.
            customer5 = new DataBuilder.CustomerBuilder()
                .WithLoginId(LoginId5)
                .WithFirstName("Ding")
                .WithLastName("Dong")
                .WithAccountId(AccountId4)
                .WithAddress1("899 Main Street")
                .WithStateId(2)
                .WithZipCode(ZipCode2)
                .WithStartDate(DateTime.Now)
                .Build();
            customers.Add(customer5);

            _dbContext.Customer.ReturnsForAnyArgs(customers);
        }
    }
}
