using System.Collections.Generic;

namespace TestTaskApi.Data.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}
