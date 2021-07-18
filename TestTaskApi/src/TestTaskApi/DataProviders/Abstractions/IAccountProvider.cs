using System.Threading.Tasks;
using TestTaskApi.Data.Entities;

namespace TestTaskApi.DataProviders.Abstractions
{
    public interface IAccountProvider
    {
        Task AddRole(string role);
        Task Registration(string email, string password, string roleName);
        Task<AccountEntity> Authentification(string email, string password);
    }
}
