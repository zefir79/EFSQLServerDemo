using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public OrderMapping()
        {
            ToTable("Order", "ECommerce");
            Property(o => o.OrderId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(c => c.Customer).WithMany().HasForeignKey(c => c.CustomerId);

            HasRequired(s => s.ShippingService).WithMany().HasForeignKey(s => s.ShippingServiceId);

            HasMany(o => o.OrderItems)
               .WithRequired()
               .HasForeignKey(o => o.OrderId);

            HasMany(o => o.OrderAddresses)
               .WithRequired()
               .HasForeignKey(o => o.OrderId);

            HasMany(o => o.OrderPayments)
               .WithRequired()
               .HasForeignKey(o => o.OrderId);

        }
    }
}
