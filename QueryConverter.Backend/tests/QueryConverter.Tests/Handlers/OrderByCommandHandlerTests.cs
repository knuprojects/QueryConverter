using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Core.Handlers;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Types.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryConverter.Core.Utils;
using QueryConverter.Tests.Templates;

namespace QueryConverter.Tests.Handlers
{
    public class OrderByCommandHandlerTests
    {
        private readonly ICommandHandler<OrderByCommand, ResultModel> _orderByCommandHandler;
        private readonly ICondition _condition;

        public OrderByCommandHandlerTests(ICondition condition)
        {
            _condition = condition;
            _orderByCommandHandler = new OrderByCommandHandler(_condition);
        }

        [Fact]
        public async Task HandleOrderByStatement_Should_Return_Valid_Query()
        {
            var command = new OrderByCommand()
            {
                SQLQuery = QueryTemplate.SelectWithOrderBy
            };

            var result = await _orderByCommandHandler.HandleAsync(command);

            result.Should().NotBeNull();
        }       
    }
}
