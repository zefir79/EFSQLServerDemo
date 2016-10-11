using System;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class UserBuilder
    {
        private int _userId = -1;
        private string _firstName = null;
        private string _lastName = null;
        private string _userName = null;
        private string _password = null;
        private int _primarySSN = -1;
        private int? _secondarySSN = null;

        public User Build()
        {
            return new User
            {
                UserId = _userId,
                FirstName = _firstName,
                LastName = _lastName,
                UserName = _userName,
                Password = _password,
                PrimarySSN = _primarySSN,
                SecondarySSN = _secondarySSN
            };
        }

        public UserBuilder WithUserId(int newUserId)
        {
            _userId = newUserId;
            return this;
        }

        public UserBuilder WithFirstName(string newFirstName)
        {
            _firstName = newFirstName;
            return this;
        }

        public UserBuilder WithLastName(string newLastName)
        {
            _lastName = newLastName;
            return this;
        }

        public UserBuilder WithUserName(string newUserName)
        {
            _userName = newUserName;
            return this;
        }

        public UserBuilder WithPassword(string newPassword)
        {
            _password = newPassword;
            return this;
        }

        public UserBuilder WithPrimarySSN(int newPrimarySSN)
        {
            _userId = newPrimarySSN;
            return this;
        }

        public UserBuilder WithSecondarySSN(int newSecondarySSN)
        {
            _secondarySSN = newSecondarySSN;
            return this;
        }
    }
}
