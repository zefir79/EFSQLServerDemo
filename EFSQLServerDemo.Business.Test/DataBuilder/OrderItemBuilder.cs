using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class OrderItemBuilder
    {
        private int _orderItemId = -1;
        private int _orderId = -1;
        private string _itemDescription = null;
        private string _color = null;
        private string _size = null;
        private decimal _price = Convert.ToDecimal("0.00");
        private string _quantity = null;

        public OrderItem Build()
        {
            return new OrderItem
            {
                OrderItemId = _orderItemId,
                OrderId = _orderId,
                ItemDescription = _itemDescription,
                Color = _color,
                Size = _size,
                Price = _price,
                Quantity = _quantity
            };
        }

        public OrderItemBuilder WithOrderItemId(int newOrderItemId)
        {
            _orderItemId = newOrderItemId;
            return this;
        }

        public OrderItemBuilder WithOrderId(int newOrderId)
        {
            _orderId = newOrderId;
            return this;
        }

        public OrderItemBuilder WithItemDescription(string newItemDescription)
        {
            _itemDescription = newItemDescription;
            return this;
        }

        public OrderItemBuilder WithColor(string newColor)
        {
            _color = newColor;
            return this;
        }

        public OrderItemBuilder WithSize(string newSize)
        {
            _size = newSize;
            return this;
        }

        public OrderItemBuilder WithPrice(decimal newPrice)
        {
            _price = newPrice;
            return this;
        }

        public OrderItemBuilder WithQuantity(string newQuantity)
        {
            _quantity = newQuantity;
            return this;
        }

    }
}
