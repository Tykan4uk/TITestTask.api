using System.Threading.Tasks;
using AutoMapper;
using TestTaskApi.DataProviders.Abstractions;
using TestTaskApi.Models;
using TestTaskApi.Models.Responses;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountProvider _accountProvider;
        private readonly IMapper _mapper;

        public AccountService(
            IAccountProvider accountProvider,
            IMapper mapper)
        {
            _accountProvider = accountProvider;
            _mapper = mapper;
        }

        public async Task AddRole(string role)
        {
            await _accountProvider.AddRole(role);
        }

        public async Task Registration(string email, string password, string roleName)
        {
            await _accountProvider.Registration(email, password, roleName);
        }

        public async Task<AuthentificationResponse> Authentification(string email, string password)
        {
            var account = await _accountProvider.Authentification(email, password);
            return new AuthentificationResponse() { Account = _mapper.Map<AccountModel>(account) };
        }
    }
}
