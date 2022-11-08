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

        /// <summary>
        /// Convert Sql Select Command to ELK
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("/select")]
        public async Task<IActionResult> ConvertSelectQuery([FromBody] SelectCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Convert Sql OrderBy Command to ELK
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("/orderBy")]
        public async Task<IActionResult> ConvertOrderByQuery([FromBody] OrderByCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Convert Sql GroupBy Command to ELK
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("/groupBy")]
        public async Task<IActionResult> ConvertGroupByQuery([FromBody] GroupByCommand command)
        {
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }
    }
}
