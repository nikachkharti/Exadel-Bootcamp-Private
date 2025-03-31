using ReportHub.Application.Contracts.CQRS;

namespace ReportHub.Application.Invoices.GetInvoices
{
    public record GetInvoicesQuery(int? PageNumber = 1, int? PageSize = 10, CancellationToken CancellationToken = default) : IQuery<GetInvoicesResult>;
}
