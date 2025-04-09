using Microsoft.Extensions.Options;
using ReportHub.Application.Contracts.Repository;
using ReportHub.Domain.Entities;
using ReportHub.Infrastructure.Helper;

namespace ReportHub.Infrastructure.Repository
{
    public class ItemRepository : MongoRepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IOptions<MongoDbSettings> options) : base(options, "Items")
        {
        }
    }
}
