using System;
using System.Collections.Generic;
using System.Data.Entity; //from EF
using System.Data.Entity.Core.Objects; //from EF
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository
{
    public interface IAllocationContextDb
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        IEnumerable<T> SqlQueryWithTimeout<T>(int commandTimeout, string sql, params object[] parameters);
        int SqlCommand(string sql, params object[] parameters);
        int SqlCommandWithTimeout(string sql, int? commandTimeout, params object[] parameters);
        int? QueryTimeout { get; set; }

        ObjectContext ObjectContext { get; }
        void Reload(object entity);

        //Business related contexts
        IDbSet<Customer> Customer { get; }
        IDbSet<Order> Order { get; }
        IDbSet<OrderItem> OrderItem { get; }
        IDbSet<OrderAddress> OrderAddress { get; }
        IDbSet<OrderPayment> OrderPayment { get; }
        IDbSet<AddressType> AddressType { get; }
        IDbSet<PaymentMode> PaymentMode { get; }
        IDbSet<ShippingService> ShippingService { get; }
        IDbSet<State> State { get; }
        IDbSet<User> User { get; }
        IDbSet<Account> Account { get; }

    }
}
