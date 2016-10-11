using System;
using Autofac;
using EFSQLServerDemo.Business.Common.Query;
using EFSQLServerDemo.Domain.Repository;

namespace EFSQLServerDemo.Business.Test.DbIntegrationTest
{
    public class TestingBaseLive
    {
        public TestingBaseLive()
        {
            var builder = new ContainerBuilder();
            Register.RegisterTypes(builder);
            builder.Build();
            GetNewContext();
        }

        protected AllocationContextDb Context;

        protected void GetNewContext()
        {
            var context = new AllocationContextDb("PostgresContext");
            Context = context;
        }

        public static class Register
        {
            public static void RegisterTypes(ContainerBuilder builder)
            {
                //the main DB access
                builder.Register(
                    container =>
                    {
                        var dbContext = new AllocationContextDb("PostgresContext");
                        //dbContext.UserContext = container.Resolve<Lazy<IUserContext>>();
                        //var audit = container.Resolve<AuditInformationHandler>();
                        //audit.SubscribeForChanges(dbContext);
                        return dbContext;
                    }
                ).As<IAllocationContextDb>().InstancePerRequest();

                builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                       .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();
            }
        }
    }
}
