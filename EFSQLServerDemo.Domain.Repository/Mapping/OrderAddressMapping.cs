using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class OrderAddressMapping : EntityTypeConfiguration<OrderAddress>
    {
        public OrderAddressMapping()
        {
            ToTable("OrderAddress", "ECommerce");
            Property(o => o.OrderAddressId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(o => o.Order).WithMany().HasForeignKey(o => o.OrderId);

            HasRequired(m => m.AddressType).WithMany().HasForeignKey(m => m.AddressTypeId);

            HasRequired(s => s.State).WithMany().HasForeignKey(s => s.StateId);
        }
    }
}
