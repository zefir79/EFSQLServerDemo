using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class OrderBuilder
    {
        private List<OrderItem> _orderItems = new List<OrderItem> { new OrderItemBuilder().Build() };
        private List<OrderPayment> _orderPayments = new List<OrderPayment> { new OrderPaymentBuilder().Build() };
        private List<OrderAddress> _orderAddresses = new List<OrderAddress> { new OrderAddressBuilder().Build() };

        public OrderBuilder WithOrderItems(List<OrderItem> orderItems)
        {
            _orderItems = orderItems;
            return this;
        }

        public OrderBuilder WithPayments(List<OrderPayment> orderPayments)
        {
            _orderPayments = orderPayments;
            return this;
        }

        public OrderBuilder WithOrderAddresses(List<OrderAddress> orderAddresses)
        {
            _orderAddresses = orderAddresses;
            return this;
        }

        private int _orderId = -1;
        private int _customerId = -1;
        private DateTime _orderDate = DateTime.Now;
        private DateTime? _processDate = null;
        private bool _giftPackaging = false;
        private int _shippingServiceId = -1;
        private string _fulfilledBy = null;
        private decimal _totalOrderCost = Convert.ToDecimal("0.00");

        public Order Build()
        {
            return new Order
            {
                OrderId = _orderId,
                CustomerId = _customerId,
                OrderDate = _orderDate,
                ProcessDate = _processDate,
                GiftPackaging = _giftPackaging,
                ShippingServiceId = _shippingServiceId,
                FulfilledBy = _fulfilledBy,
                TotalOrderCost = _totalOrderCost,
                OrderItems = _orderItems,
                OrderPayments = _orderPayments,
                OrderAddresses = _orderAddresses
            };
        }

        public OrderBuilder WithOrderId(int newOrderId)
        {
            _orderId = newOrderId;
            return this;
        }

        public OrderBuilder WithCustomerId(int newCustomerId)
        {
            _customerId = newCustomerId;
            return this;
        }

        public OrderBuilder WithOrderDate(DateTime newOrderDate)
        {
            _orderDate = newOrderDate;
            return this;
        }

        public OrderBuilder WithProcessDate(DateTime? newProcessDate)
        {
            _processDate = newProcessDate;
            return this;
        }

        public OrderBuilder WithGiftPackaging(bool newGiftPackaging)
        {
            _giftPackaging = newGiftPackaging;
            return this;
        }

        public OrderBuilder WithShippingServiceId(int newShippingServiceId)
        {
            _shippingServiceId = newShippingServiceId;
            return this;
        }

        public OrderBuilder WithFulfilledBy(string newFulfilledBy)
        {
            _fulfilledBy = newFulfilledBy;
            return this;
        }

        public OrderBuilder WithTotalOrderCost(decimal newTotalOrderCost)
        {
            _totalOrderCost = newTotalOrderCost;
            return this;
        }
    }
}
