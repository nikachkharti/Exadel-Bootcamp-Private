using Microsoft.Extensions.Options;
using ReportHub.Application.Contracts.Repository;
using ReportHub.Domain.Entities;
using ReportHub.Infrastructure.Helper;

namespace ReportHub.Infrastructure.Repository
{
    public class BankAccountRepository : MongoRepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IOptions<MongoDbSettings> options) : base(options, "BankAccounts")
        {
        }
    }
}
