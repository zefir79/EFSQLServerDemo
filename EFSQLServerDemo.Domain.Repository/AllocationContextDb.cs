using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;
using System.Linq;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class AllocationContextDb : DbContext, IAllocationContextDb
    {
        public AllocationContextDb(string connectionString) : base(connectionString)
        {
            //Helpful for debugging            
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //dynamically load all Mapping classes in this assembly
            //Database.SetInitializer<AllocationContextDb>(null);
            modelBuilder.HasDefaultSchema("ECommerce");
            LoadMappings(modelBuilder, Assembly.GetAssembly(typeof(AllocationContextDb)));
            base.OnModelCreating(modelBuilder);
        }

        private void LoadMappings(DbModelBuilder modelBuilder, Assembly assembly)
        {

            var typesToRegister =
                from t in assembly.GetTypes()
                where t.BaseType != null
                      && t.BaseType.IsGenericType
                      && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                select (dynamic)Activator.CreateInstance(t);

            foreach (var t in typesToRegister)
            {
                //Console.WriteLine("Adding ModelBuilder Configuration for " + t.GetType().Name);
                modelBuilder.Configurations.Add(t);
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters);
        }

        public IEnumerable<T> SqlQueryWithTimeout<T>(int commandTimeout, string sql, params object[] parameters)
        {
            ObjectContext.CommandTimeout = commandTimeout;
            return this.SqlQuery<T>(sql, parameters);
        }

        public int SqlCommand(string sql, params object[] parameters)
        {
            return SqlCommandWithTimeout(sql, null, parameters);
        }

        public int SqlCommandWithTimeout(string sql, int? commandTimeout, params object[] parameters)
        {
            ObjectContext.CommandTimeout = commandTimeout;
            return Database.ExecuteSqlCommand(sql, parameters);
        }

        public int? QueryTimeout
        {
            get { return ObjectContext.CommandTimeout; }
            set { ObjectContext.CommandTimeout = value; }
        }

        public void Reload(object entity)
        {
            Entry(entity).Reload();
        }

        //not currently used but defined in case somebody needs it
        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        //Business related contexts
        public IDbSet<Customer> Customer { get; set; }
        public IDbSet<Order> Order { get; set; }
        public IDbSet<OrderItem> OrderItem { get; set; }
        public IDbSet<OrderAddress> OrderAddress { get; set; }
        public IDbSet<OrderPayment> OrderPayment { get; set; }
        public IDbSet<AddressType> AddressType { get; set; }
        public IDbSet<PaymentMode> PaymentMode { get; set; }
        public IDbSet<ShippingService> ShippingService { get; set; }
        public IDbSet<State> State { get; set; }
        public IDbSet<User> User { get; set; }
        public IDbSet<Account> Account { get; set; }
    }
}
