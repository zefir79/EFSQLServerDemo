using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class AccountMapping : EntityTypeConfiguration<Account>
    {
        public AccountMapping()
        {
            ToTable("Account", "ECommerce");
            Property(o => o.AccountId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(u => u.User).WithMany().HasForeignKey(u => u.UserId);
        }
    }
}
