using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class StateMapping : EntityTypeConfiguration<State>
    {
        public StateMapping()
        {
            ToTable("State", "ECommerce");
            Property(o => o.StateId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
