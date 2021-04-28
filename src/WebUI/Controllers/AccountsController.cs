using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ing.Interview.Application.Accounts.Queries.GetAccountsQuery;

namespace Ing.Interview.WebUI.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAccountsResult>> GetAccountsWithPagination([FromQuery] GetAccountsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
