using System;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class CustomerBuilder
    {
        private int _customerId = -1;
        private string _loginId = null;
        private string _accountId = null;
        private string _firstName = null;
        private string _lastName = null;
        private string _friendlyName = null;
        private string _address1 = null;
        private string _address2 = null;
        private string _city = null;
        private int _stateId = -1;
        private string _zipCode = null;
        private DateTime _startDate = DateTime.MinValue;
        private DateTime? _endDate = null;
        private int _phone = -1;

        public Customer Build()
        {
            return new Customer()
            {
                CustomerId = _customerId,
                LoginId = _loginId,
                AccountId = _accountId,
                FirstName = _firstName,
                LastName = _lastName,
                FriendlyName = _friendlyName,
                Address1 = _address1,
                Address2 = _address2,
                City = _city,
                StateId = _stateId,
                ZipCode = _zipCode,
                StartDate = _startDate,
                EndDate = _endDate,
                Phone = _phone
            };
        }

        public CustomerBuilder WithCustomerId(int newCustomerId)
        {
            _customerId = newCustomerId;
            return this;
        }

        public CustomerBuilder WithLoginId(string newLoginId)
        {
            _loginId = newLoginId;
            return this;
        }

        public CustomerBuilder WithAccountId(string newAccountId)
        {
            _accountId = newAccountId;
            return this;
        }

        public CustomerBuilder WithFirstName(string newFirstName)
        {
            _firstName = newFirstName;
            return this;
        }

        public CustomerBuilder WithLastName(string newLastName)
        {
            _lastName = newLastName;
            return this;
        }

        public CustomerBuilder WithFriendlyName(string newFriendlyName)
        {
            _friendlyName = newFriendlyName;
            return this;
        }

        public CustomerBuilder WithAddress1(string newAddress1)
        {
            _address1 = newAddress1;
            return this;
        }

        public CustomerBuilder WithAddress2(string newAddress2)
        {
            _address2 = newAddress2;
            return this;
        }

        public CustomerBuilder WithCity(string newCity)
        {
            _city = newCity;
            return this;
        }

        public CustomerBuilder WithStateId(int newStateId)
        {
            _stateId = newStateId;
            return this;
        }

        public CustomerBuilder WithZipCode(string newZipCode)
        {
            _zipCode = newZipCode;
            return this;
        }

        public CustomerBuilder WithStartDate(DateTime newStartDate)
        {
            _startDate = newStartDate;
            return this;
        }

        public CustomerBuilder WithEndDate(DateTime newEndDate)
        {
            _endDate = newEndDate;
            return this;
        }

        public CustomerBuilder WithPhone(int newPhone)
        {
            _phone = newPhone;
            return this;
        }
    }
}
