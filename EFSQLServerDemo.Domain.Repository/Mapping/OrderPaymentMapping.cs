using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class OrderPaymentMapping : EntityTypeConfiguration<OrderPayment>
    {
        public OrderPaymentMapping()
        {
            ToTable("OrderPayment", "ECommerce");
            Property(o => o.OrderPaymentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(o => o.Order).WithMany().HasForeignKey(o => o.OrderId);

            HasRequired(m => m.PaymentMode).WithMany().HasForeignKey(m => m.PaymentModeId);

        }
    }
}
