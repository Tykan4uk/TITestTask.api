using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskApi.Data.Entities;

namespace TestTaskApi.Data.EntityConfigurations
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.ToTable("Message")
                .HasKey(h => h.UserId);

            builder.Property(p => p.Message)
                .IsRequired()
                .HasColumnName("Message")
                .HasMaxLength(255);
        }
    }
}
