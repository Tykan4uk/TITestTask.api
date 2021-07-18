using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskApi.Data.Entities;

namespace TestTaskApi.Data.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role")
                .HasKey(h => h.Id);

            builder.Property(p => p.RoleName)
                .IsRequired()
                .HasColumnName("RoleName")
                .HasMaxLength(50);

            builder.HasIndex(h => h.RoleName)
                .IsUnique();
        }
    }
}
