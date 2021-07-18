using System.ComponentModel.DataAnnotations;

namespace TestTaskApi.Models.Requests
{
    public class AddRoleRequest
    {
        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }
}
