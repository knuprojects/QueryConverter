using Microsoft.AspNetCore.Mvc;
using QueryConverter.Core.Handlers;
using QueryConverter.Shared.Cqrs.Dispatchers;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryConverterController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public QueryConverterController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<ActionResult<ResultModel>> ConvertSelectQuery([FromBody] SelectCommand command)
        {
            await _dispatcher.SendAsync(command);

            return Ok();
        }
    }
}
