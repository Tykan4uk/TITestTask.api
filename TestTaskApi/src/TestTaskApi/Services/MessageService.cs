using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TestTaskApi.DataProviders.Abstractions;
using TestTaskApi.Models;
using TestTaskApi.Models.Responses;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageProvider _messageProvider;
        private readonly IMapper _mapper;

        public MessageService(
            IMessageProvider messageProvider,
            IMapper mapper)
        {
            _messageProvider = messageProvider;
            _mapper = mapper;
        }

        public async Task<GetResponse> GetAsync(string accountId)
        {
            var result = await _messageProvider.GetAsync(accountId);
            return new GetResponse() { Messages = _mapper.Map<List<MessageModel>>(result) };
        }

        public async Task<AddResponse> AddAsync(string accountId, string message)
        {
            var result = await _messageProvider.AddAsync(accountId, message);
            return new AddResponse() { MessageId = result };
        }

        public async Task<UpdateResponse> UpdateAsync(string accountId, string messageId, string message)
        {
            var result = await _messageProvider.UpdateAsync(accountId, messageId, message);
            return new UpdateResponse() { IsUpdated = result };
        }

        public async Task<RemoveResponse> RemoveAsync(string accountId, string messageId)
        {
            var result = await _messageProvider.RemoveAsync(accountId, messageId);
            return new RemoveResponse() { IsDeleted = result };
        }
    }
}
