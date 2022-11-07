using Microsoft.AspNetCore.Mvc;
using QueryConverter.Core.Handlers;
using QueryConverter.Shared.Cqrs.Dispatchers;

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

        [HttpPut("/select")]
        public async Task<IActionResult> ConvertSelectQuery([FromBody] SelectCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPut("/orderBy")]
        public async Task<IActionResult> ConvertOrderByQuery([FromBody] OrderByCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPut("/groupBy")]
        public async Task<IActionResult> ConvertGroupByQuery([FromBody] GroupByCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }
    }
}
