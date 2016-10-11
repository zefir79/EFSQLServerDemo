using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class PaymentModeMapping : EntityTypeConfiguration<PaymentMode>
    {
        public PaymentModeMapping()
        {
            ToTable("PaymentMode", "ECommerce");
            Property(o => o.PaymentModeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
