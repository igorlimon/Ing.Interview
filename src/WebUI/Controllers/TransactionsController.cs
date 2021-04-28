using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ing.Interview.Application.Transactions.Queries.GetTransactionsQuery;

namespace Ing.Interview.WebUI.Controllers
{
    public class TransactionsController : ApiControllerBase
    {
        [HttpGet]
        [Route("report")]
        public async Task<ActionResult<GetTransactionsResult>> GetTransactionsReportWithPagination([FromQuery] GetTransactionsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
