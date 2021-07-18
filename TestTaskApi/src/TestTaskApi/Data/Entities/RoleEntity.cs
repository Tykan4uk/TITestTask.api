using System.Collections.Generic;

namespace TestTaskApi.Data.Entities
{
    public class RoleEntity
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

        public List<AccountEntity> AccountEntities { get; set; } = new List<AccountEntity>();
    }
}
