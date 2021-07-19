using System.Threading.Tasks;
using TestTaskApi.Models.Responses;

namespace TestTaskApi.Services.Abstractions
{
    public interface IMessageService
    {
        Task<GetResponse> GetAsync(string accountId);
        Task<AddResponse> AddAsync(string accountId, string message);
        Task<UpdateResponse> UpdateAsync(string accountId, string messageId, string message);
        Task<RemoveResponse> RemoveAsync(string accountId, string messageId);
    }
}
