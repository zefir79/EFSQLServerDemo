using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class AddressTypeMapping : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeMapping()
        {
            ToTable("AddressType", "ECommerce");
            Property(o => o.AddressTypeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
