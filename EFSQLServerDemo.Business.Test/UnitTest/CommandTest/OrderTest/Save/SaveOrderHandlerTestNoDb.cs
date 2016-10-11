using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Domain.Object;
using EFSQLServerDemo.Business.Common.Provider;
using EFSQLServerDemo.Business.Command.Order.Save;
using EFSQLServerDemo.Business.Common.Command;
using EFSQLServerDemo.Business.Test.DataBuilder;
using EFSQLServerDemo.Business.Test.DbIntegrationTest;
using NSubstitute;


namespace EFSQLServerDemo.Business.Test.UnitTest.CommandTest.OrderTest.Save
{
    [TestClass]
    [Category("Db_Free")]
    public class SaveOrderHandlerTestNoDb 
    {
        #region "Positive tests"
        [TestClass]
        [Category("Db_Free")]
        public class ShouldSuccessfullySaveOrder : TestBase
        {
            private IAllocationContextDb _dbContext;
            private IValidateCustomerProvider _customerProvider;
            private IDbSet<Domain.Object.Order> _order;
            private IDbSet<Domain.Object.Customer> _customer;

            private ICommandHandler<Domain.Object.Order, CommandHandlerResult> _command;

            private int customerId = 999;

            [TestInitialize]
            public void Initialize()
            {
                _dbContext = Substitute.For<IAllocationContextDb>();

                _customerProvider = new ValidateCustomerProvider(_dbContext);
                _customerProvider.IsExistingCustomer(customerId).Returns(true);

                _customer = new FakeDbSet<Domain.Object.Customer>();
                _dbContext.Customer.Returns(_customer);

                _order = new FakeDbSet<Domain.Object.Order>();
                _dbContext.Order.Returns(_order);
            }

            [TestMethod]
            public void ShouldSuccessfullySaveAnOrder()
            {
                Describes("Successfully inserting an order.");
                Given_That_I_Have_An_Order_Save_Handler();
                When_I_Try_To_Insert_An_Order();
                Then_I_Should_Be_Able_To_Successfully_Insert();
            }

            private void Given_That_I_Have_An_Order_Save_Handler()
            {
                _command = new SaveOrderHandler(_dbContext, _customerProvider);
            }

            private void When_I_Try_To_Insert_An_Order()
            {
                _command.Process(Insert_Order_For_Customer1());
                _order.Add(_dbContext.Order.FirstOrDefault());
                _customer.Add(_dbContext.Customer.FirstOrDefault());
            }

            private void Then_I_Should_Be_Able_To_Successfully_Insert()
            {
                Assert.AreEqual(customerId, _order.FirstOrDefault().CustomerId, "Customer Id should match.");

            }

            private Domain.Object.Order Insert_Order_For_Customer1()
            {
                var customer = new CustomerBuilder()
                    .WithCustomerId(999)
                    .Build();

                var address1 = new OrderAddressBuilder()
                    .WithAddressTypeId(1)
                    .WithAddress1("123 XYZ Street")
                    .WithCity("Rockville")
                    .WithStateId(1)
                    .WithZipCode("20853")
                    .Build();

                var addresses = new List<OrderAddress> { address1 };

                var payment1 = new OrderPaymentBuilder()
                    .WithPaymentModeId(1)
                    .WithCardName("AMEX")
                    .WithCardNumber(1234567890)
                    .WithCCV("1234")
                    .WithExpirationDate("01/2020")
                    .WithPaymentAmount(Convert.ToDecimal("30.00"))
                    .Build();

                var payments = new List<OrderPayment> { payment1 };

                var payment2 = new OrderPaymentBuilder()
                    .WithPaymentModeId(2)
                    .WithCardName("AMAZON")
                    .WithCardNumber(9876543)
                    .WithPaymentAmount(Convert.ToDecimal("10.00"))
                    .Build();


                payments.Add(payment2);

                var payment3 = new OrderPaymentBuilder()
                    .WithPaymentModeId(3)
                    .WithCardName("VISA")
                    .WithCardNumber(77554466)
                    .WithExpirationDate("03/2018")
                    .WithPaymentAmount(Convert.ToDecimal("10.99"))
                    .Build();

                payments.Add(payment3);

                var orderItem1 = new OrderItemBuilder()
                    .WithOrderItemId(1)
                    .WithItemDescription("Ladies Shoes")
                    .WithColor("Brown")
                    .WithSize("8")
                    .WithPrice(Convert.ToDecimal("20.99"))
                    .WithQuantity("1")
                    .Build();

                var orderItems = new List<OrderItem> { orderItem1 };

                var orderItem2 = new OrderItemBuilder()
                    .WithOrderItemId(2)
                    .WithItemDescription("Boys Tank Top")
                    .WithColor("White")
                    .WithSize("S")
                    .WithPrice(Convert.ToDecimal("30.00"))
                    .WithQuantity("2")
                    .Build();

                orderItems.Add(orderItem2);

                var order = new OrderBuilder()
                    .WithOrderId(-1)
                    .WithCustomerId(customer.CustomerId)
                    .WithOrderDate(DateTime.Now)
                    .WithGiftPackaging(true)
                    .WithShippingServiceId(1)
                    .WithTotalOrderCost(Convert.ToDecimal("50.99"))
                    .WithOrderItems(orderItems)
                    .WithPayments(payments)
                    .WithOrderAddresses(addresses)
                    .Build();


                return order;
            }


        }
        #endregion

        #region "Negative tests"
        public class ShouldNotSaveOrder
        { 
        
        }
        #endregion
    }
}
