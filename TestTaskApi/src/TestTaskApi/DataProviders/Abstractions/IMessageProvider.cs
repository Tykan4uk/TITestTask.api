using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskApi.Data.Entities;

namespace TestTaskApi.DataProviders.Abstractions
{
    public interface IMessageProvider
    {
        Task<List<MessageEntity>> GetAsync(string accountId);
        Task<string> AddAsync(string accountId, string message);
        Task<bool> UpdateAsync(string accountId, string messageId, string message);
        Task<bool> RemoveAsync(string accountId, string messageId);
    }
}
