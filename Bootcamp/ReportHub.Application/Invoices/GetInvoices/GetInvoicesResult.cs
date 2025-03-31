using ReportHub.Domain.Entities;

namespace ReportHub.Application.Invoices.GetInvoices
{
    public record GetInvoicesResult(IEnumerable<Invoice> Invoices);
}
