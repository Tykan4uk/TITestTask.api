using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data;
using TestTaskApi.DataProviders.Abstractions;
using TestTaskApi.Models;
using TestTaskApi.Models.Responses;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Services
{
    public class MessageService : BaseDataService, IMessageService
    {
        private readonly IMessageProvider _messageProvider;
        private readonly IMapper _mapper;

        public MessageService(
            IDbContextFactory<TestTaskDbContext> factory,
            IMessageProvider messageProvider,
            IMapper mapper)
            : base(factory)
        {
            _messageProvider = messageProvider;
            _mapper = mapper;
        }

        public async Task<GetResponse> GetAsync(string accountId)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _messageProvider.GetAsync(accountId);
                return new GetResponse() { Messages = _mapper.Map<List<MessageModel>>(result) };
            });
        }

        public async Task<AddResponse> AddAsync(string accountId, string message)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _messageProvider.AddAsync(accountId, message);
                return new AddResponse() { MessageId = result };
            });
        }

        public async Task<UpdateResponse> UpdateAsync(string accountId, string messageId, string message)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _messageProvider.UpdateAsync(accountId, messageId, message);
                return new UpdateResponse() { IsUpdated = result };
            });
        }

        public async Task<RemoveResponse> RemoveAsync(string accountId, string messageId)
        {
            return await ExecuteSafe(async () =>
            {
                var result = await _messageProvider.RemoveAsync(accountId, messageId);
                return new RemoveResponse() { IsDeleted = result };
            });
        }
    }
}
