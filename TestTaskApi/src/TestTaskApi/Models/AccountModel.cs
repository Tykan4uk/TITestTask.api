namespace TestTaskApi.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleModel Role { get; set; }
    }
}
