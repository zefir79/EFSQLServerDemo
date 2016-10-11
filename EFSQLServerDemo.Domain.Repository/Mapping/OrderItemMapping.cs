using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class OrderItemMapping : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMapping()
        {
            ToTable("OrderItem", "ECommerce");
            Property(o => o.OrderItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(o => o.Order).WithMany().HasForeignKey(o => o.OrderId);
        }
    }
}
