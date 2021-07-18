using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskApi.Data.Entities;

namespace TestTaskApi.Data.EntityConfigurations
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.ToTable("Account")
                .HasKey(h => h.Id);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(50);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(50);

            builder.HasIndex(h => h.Email)
                .IsUnique();

            builder.HasOne(h => h.Role)
                .WithMany(w => w.AccountEntities)
                .HasForeignKey(h => h.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
