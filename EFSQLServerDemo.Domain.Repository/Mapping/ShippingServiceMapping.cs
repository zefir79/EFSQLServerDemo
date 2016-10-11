using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class ShippingServiceMapping : EntityTypeConfiguration<ShippingService>
    {
        public ShippingServiceMapping()
        {
            ToTable("ShippingService", "ECommerce");
            Property(o => o.ShippingServiceId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
