using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data;
using TestTaskApi.Data.Entities;
using TestTaskApi.DataProviders.Abstractions;

namespace TestTaskApi.DataProviders
{
    public class AccountProvider : IAccountProvider
    {
        private readonly TestTaskDbContext _testTaskDbContext;

        public AccountProvider(TestTaskDbContext testTaskDbContext)
        {
            _testTaskDbContext = testTaskDbContext;
        }

        public async Task AddRole(string role)
        {
            var checkRole = await _testTaskDbContext.Roles.FirstOrDefaultAsync(f => f.RoleName == role);

            if (checkRole == null)
            {
                var lastRole = _testTaskDbContext.Roles.OrderBy(o => o.Id).LastOrDefault() ?? new RoleEntity() { Id = 0 };

                await _testTaskDbContext.Roles.AddAsync(new RoleEntity()
                {
                    Id = lastRole.Id + 1,
                    RoleName = role
                });
                await _testTaskDbContext.SaveChangesAsync();
            }
        }

        public async Task Registration(string email, string password, string roleName)
        {
            var checkAccount = await _testTaskDbContext.Accounts.FirstOrDefaultAsync(f => f.Email == email);

            if (checkAccount == null)
            {
                var hashPassword = GenerateHashString(password);
                await _testTaskDbContext.Accounts.AddAsync(new AccountEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    Password = hashPassword,
                    RoleId = _testTaskDbContext.Roles.FirstOrDefault(f => f.RoleName == roleName).Id,
                    Role = await _testTaskDbContext.Roles.FirstOrDefaultAsync(f => f.RoleName == roleName)
                });
            }

            await _testTaskDbContext.SaveChangesAsync();
        }

        public async Task<AccountEntity> Authentification(string email, string password)
        {
            var hashPassword = GenerateHashString(password);
            return await _testTaskDbContext.Accounts.Include(i => i.Role).FirstOrDefaultAsync(f => f.Email == email && f.Password == hashPassword);
        }

        private string GenerateHashString(string text)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                var result = sha1.Hash;
                return string.Join(string.Empty, result.Select(x => x.ToString("x2")));
            }
        }
    }
}
