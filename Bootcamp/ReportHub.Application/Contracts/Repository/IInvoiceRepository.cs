using ReportHub.Domain.Entities;

namespace ReportHub.Application.Contracts.Repository
{
    public interface IInvoiceRepository : IMongoRepositoryBase<Invoice>
    {
    }
}
