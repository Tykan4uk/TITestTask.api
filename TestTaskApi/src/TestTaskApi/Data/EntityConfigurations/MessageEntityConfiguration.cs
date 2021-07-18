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
                .HasKey(h => h.MessageId);

            builder.Property(p => p.Message)
                .IsRequired()
                .HasColumnName("Message")
                .HasMaxLength(255);

            builder.HasOne(h => h.Account)
                .WithMany(w => w.Messages)
                .HasForeignKey(h => h.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
