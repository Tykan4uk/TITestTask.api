using System.Threading.Tasks;
using TestTaskApi.Models.Responses;

namespace TestTaskApi.Services.Abstractions
{
    public interface IAccountService
    {
        Task AddRole(string role);
        Task Registration(string email, string password, string roleName);
        Task<AuthentificationResponse> Authentification(string email, string password);
    }
}
