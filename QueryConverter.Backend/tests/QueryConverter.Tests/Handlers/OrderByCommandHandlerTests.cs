using QueryConverter.Core.Utils;
using QueryConverter.Presentation.Handlers;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Tests.Templates;
using QueryConverter.Types.Shared.Dto;

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
