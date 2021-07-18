using System.Collections.Generic;

namespace TestTaskApi.Models.Responses
{
    public class GetResponse
    {
        public List<MessageModel> Messages { get; set; }
    }
}