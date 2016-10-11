using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EFSQLServerDemo.Domain.Repository;
using EFSQLServerDemo.Business.Common.Query;

namespace EFSQLServerDemo.Data
{
   
        public class DataModule : Module
       {
           private string connStr;
           public DataModule(string connString)
           {
              this.connStr = connString;
           }
        protected override void Load(ContainerBuilder builder)
        {

            //the main DB access
            builder.Register(
                container =>
                {
                    var dbContext = new AllocationContextDb(this.connStr); // "PostgresContext"
                    //dbContext.UserContext = container.Resolve<Lazy<IUserContext>>();
                    //var audit = container.Resolve<AuditInformationHandler>();
                    //audit.SubscribeForChanges(dbContext);
                    return dbContext;
                }
            ).As<IAllocationContextDb>().InstancePerRequest();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();


            //builder.Register(c => new EFContext(this.connStr)).As<IDbContext>().InstancePerRequest();
            //  builder.RegisterType<SqlRepository>().As<IRepository>().InstancePerRequest();
            //  builder.RegisterType<TeamRepository>().As<ITeamRepository>().InstancePerRequest();
            //  builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();         

            base.Load(builder);
        }
       
    }
}
