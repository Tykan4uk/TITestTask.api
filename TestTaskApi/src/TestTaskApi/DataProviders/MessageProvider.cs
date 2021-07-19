using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data;
using TestTaskApi.Data.Entities;
using TestTaskApi.DataProviders.Abstractions;

namespace TestTaskApi.DataProviders
{
    public class MessageProvider : IMessageProvider
    {
        private readonly TestTaskDbContext _testTaskDbContext;

        public MessageProvider(IDbContextFactory<TestTaskDbContext> dbContextFactory)
        {
            _testTaskDbContext = dbContextFactory.CreateDbContext();
        }

        public async Task<List<MessageEntity>> GetAsync(string accountId)
        {
            return await _testTaskDbContext.Messages.Where(w => w.AccountId == accountId).ToListAsync();
        }

        public async Task<string> AddAsync(string accountId, string message)
        {
            var account = await _testTaskDbContext.Accounts.Include(i => i.Role).FirstOrDefaultAsync(f => f.Id == accountId);
            var id = Guid.NewGuid().ToString();
            await _testTaskDbContext.Messages.AddAsync(new MessageEntity()
            {
                Account = account,
                AccountId = accountId,
                MessageId = id,
                Message = message
            });
            await _testTaskDbContext.SaveChangesAsync();

            return id;
        }

        public async Task<bool> UpdateAsync(string accountId, string messageId, string message)
        {
            var account = await _testTaskDbContext.Accounts.Include(i => i.Messages).FirstOrDefaultAsync(f => f.Id == accountId);
            var msg = account.Messages.FirstOrDefault(f => f.MessageId == messageId);
            if (msg != null)
            {
                msg.Message = message;
                await _testTaskDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(string accountId, string messageId)
        {
            var account = await _testTaskDbContext.Accounts.Include(i => i.Messages).FirstOrDefaultAsync(f => f.Id == accountId);
            var message = account.Messages.FirstOrDefault(f => f.MessageId == messageId);
            if (message != null)
            {
                _testTaskDbContext.Messages.Remove(message);
                await _testTaskDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
