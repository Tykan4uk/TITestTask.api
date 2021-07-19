using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTaskApi.Data;

namespace TestTaskApi.Services
{
    public class BaseDataService
    {
        private readonly TestTaskDbContext _testTaskDbContext;

        public BaseDataService(IDbContextFactory<TestTaskDbContext> dbContextFactory)
        {
            _testTaskDbContext = dbContextFactory.CreateDbContext();
        }

        protected async Task<T> ExecuteSafe<T>(Func<Task<T>> action)
        {
            using (var transaction = _testTaskDbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await action();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
