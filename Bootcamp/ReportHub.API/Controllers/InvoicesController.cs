using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportHub.Application.Invoices.GetInvoices;

namespace ReportHub.API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices([FromQuery] GetInvoicesQuery request)
        {
            var x = await sender.Send(request);
            return Ok(x);
        }
    }
}
