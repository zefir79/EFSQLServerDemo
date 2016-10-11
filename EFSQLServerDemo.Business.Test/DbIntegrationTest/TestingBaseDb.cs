using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.Test.DbIntegrationTest
{
    [Category("Hits_the_Db")]
    public class TestingBaseDb
    {
        static TestingBaseDb()
        {
//#if DEBUG
//            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
//#endif
            TestDbManager.Reset("PostgresContext");
        }

        protected AllocationContextDb Context;
        private TransactionScope _transaction;

        protected AllocationContextDb GetNewContext()
        {
            var context = new AllocationContextDb("PostgresContext");
            return context;
        }

        protected void ResetContext()
        {
            var context = GetNewContext();
            Context = context;
        }

        [TestInitialize]
        public virtual void BaseSetup()
        {
            Console.WriteLine("BaseSetup");
            _transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });

            ResetContext();
        }

        [TestCleanup]
        public virtual void BaseTeardown()
        {
            _transaction.Dispose();
        }

        protected DateTime Infinity
        {
            get { return new DateTime(2100, 1, 1); }
        }
    }
}
