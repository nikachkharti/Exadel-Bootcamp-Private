using ReportHub.Application.Contracts.CQRS;
using ReportHub.Application.Contracts.Repository;

namespace ReportHub.Application.Invoices.GetInvoices
{
    public record GetInvoicesQueryHandler
        (IInvoiceRepository invoiceRepository)
        : IQueryHandler<GetInvoicesQuery, GetInvoicesResult>
    {
        public async Task<GetInvoicesResult> Handle(GetInvoicesQuery request, CancellationToken cancellationToken = default)
        {
            var raw = await invoiceRepository
                .GetAll(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetInvoicesResult(raw);
        }
    }
}
