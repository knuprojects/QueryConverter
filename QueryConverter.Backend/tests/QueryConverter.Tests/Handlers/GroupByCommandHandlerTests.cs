using QueryConverter.Core.Utils;
using QueryConverter.Presentation.Handlers;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Tests.Templates;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Tests.Handlers
{
    public class GroupByCommandHandlerTests
    {
        private readonly ICommandHandler<GroupByCommand, ResultModel> _groupByCommandHandler;
        private readonly ICondition _condition;

        public GroupByCommandHandlerTests(ICondition condition)
        {
            _condition = condition;
            _groupByCommandHandler = new GroupByCommandHandler(_condition);
        }

        [Fact]
        public async Task HandleGroupByStatementWithFilter_Should_Return_Valid_Query()
        {
            var command = new GroupByCommand()
            {
                SQLQuery = QueryTemplate.SelectWithFilterAndGroupBy
            };

            var result = await _groupByCommandHandler.HandleAsync(command);

            result.Should().NotBeNull();
        }
    }
}
