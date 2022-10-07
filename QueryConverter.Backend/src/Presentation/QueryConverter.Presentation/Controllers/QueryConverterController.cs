using Microsoft.AspNetCore.Mvc;
using QueryConverter.Core.Processor;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryConverterController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public QueryConverterController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [HttpPost]
        public async Task<ResultModel> ConvertQuery([FromBody] ConvertModel model)
        {
            var result = await _queryProcessor.ProcessSqlQuery(model.SQLQuery);
            return result;
        }
    }
}
