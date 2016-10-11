using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Domain.Repository.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("User", "ECommerce");
            Property(o => o.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           
            HasMany(o => o.Accounts)
              .WithRequired()
              .HasForeignKey(o => o.UserId);
            //HasRequired(a => a.Account).WithMany().HasForeignKey(a => a.AccountId);
        }
    }
}
