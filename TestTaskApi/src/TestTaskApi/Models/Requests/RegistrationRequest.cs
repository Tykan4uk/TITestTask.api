using System.ComponentModel.DataAnnotations;

namespace TestTaskApi.Models.Requests
{
    public class RegistrationRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
