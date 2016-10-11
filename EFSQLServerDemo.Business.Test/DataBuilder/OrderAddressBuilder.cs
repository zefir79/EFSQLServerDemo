using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class OrderAddressBuilder
    {
        private int _orderAddressId = -1;
        private int _orderId = -1;
        private int _addressTypeId = -1;
        private string _address1 = null;
        private string _address2 = null;
        private string _city = null;
        private int _stateId = -1;
        private string _zipCode = null;

        public OrderAddress Build()
        {
            return new OrderAddress
            {
                OrderAddressId = _orderAddressId,
                OrderId = _orderId,
                AddressTypeId = _addressTypeId,
                Address1 = _address1,
                Address2 = _address2,
                City = _city,
                StateId = _stateId,
                ZipCode = _zipCode
            };
        }

        public OrderAddressBuilder WithOrderAddressId(int newOrderAddressId)
        {
            _orderAddressId = newOrderAddressId;
            return this;
        }

        public OrderAddressBuilder WithOrderId(int newOrderId)
        {
            _orderId = newOrderId;
            return this;
        }

        public OrderAddressBuilder WithAddressTypeId(int newAddressTypeId)
        {
            _addressTypeId = newAddressTypeId;
            return this;
        }

        public OrderAddressBuilder WithAddress1(string newAddress1)
        {
            _address1 = newAddress1;
            return this;
        }

        public OrderAddressBuilder WithAddress2(string newAddress2)
        {
            _address2 = newAddress2;
            return this;
        }

        public OrderAddressBuilder WithCity(string newCity)
        {
            _city = newCity;
            return this;
        }

        public OrderAddressBuilder WithStateId(int newStateId)
        {
            _stateId = newStateId;
            return this;
        }

        public OrderAddressBuilder WithZipCode(string newZipCode)
        {
            _zipCode = newZipCode;
            return this;
        }

    }
}
