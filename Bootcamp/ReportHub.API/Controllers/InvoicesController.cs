using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportHub.Application.Invoices.GetInvoices;

namespace ReportHub.API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ISender _sender;
        public InvoicesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices([FromQuery] GetInvoicesQuery request)
        {
            var x = await _sender.Send(request);
            return Ok(x);
        }
    }
}
