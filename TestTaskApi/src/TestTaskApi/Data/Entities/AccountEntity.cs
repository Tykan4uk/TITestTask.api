namespace TestTaskApi.Data.Entities
{
    public class AccountEntity
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
