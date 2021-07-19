using System.ComponentModel.DataAnnotations;

namespace TestTaskApi.Models.Requests
{
    public class AddRequest
    {
        public string AccountId { get; set; }
        [StringLength(255)]
        public string Message { get; set; }
    }
}
