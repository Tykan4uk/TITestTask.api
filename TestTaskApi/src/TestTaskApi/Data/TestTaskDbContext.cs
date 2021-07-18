using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data.Entities;
using TestTaskApi.Data.EntityConfigurations;

namespace TestTaskApi.Data
{
    public class TestTaskDbContext : DbContext
    {
        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
        }
    }
}
