using System.ComponentModel.DataAnnotations;

namespace TestTaskApi.Models.Requests
{
    public class UpdateRequest
    {
        public string AccountId { get; set; }
        public string MessageId { get; set; }
        [StringLength(255)]
        public string Message { get; set; }
    }
}
