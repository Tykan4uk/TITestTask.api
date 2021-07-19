using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data;
using TestTaskApi.DataProviders.Abstractions;
using TestTaskApi.Models;
using TestTaskApi.Models.Responses;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Services
{
    public class AccountService : BaseDataService, IAccountService
    {
        private readonly IAccountProvider _accountProvider;
        private readonly IMapper _mapper;

        public AccountService(
            IDbContextFactory<TestTaskDbContext> factory,
            IAccountProvider accountProvider,
            IMapper mapper)
            : base(factory)
        {
            _accountProvider = accountProvider;
            _mapper = mapper;
        }

        public async Task AddRole(string role)
        {
            await ExecuteSafe(async () =>
            {
                await _accountProvider.AddRole(role);
            });
        }

        public async Task Registration(string email, string password, string roleName)
        {
            await ExecuteSafe(async () =>
            {
                await _accountProvider.Registration(email, password, roleName);
            });
        }

        public async Task<AuthentificationResponse> Authentification(string email, string password)
        {
            return await ExecuteSafe(async () =>
            {
                var account = await _accountProvider.Authentification(email, password);
                return new AuthentificationResponse() { Account = _mapper.Map<AccountModel>(account) };
            });
        }
    }
}
