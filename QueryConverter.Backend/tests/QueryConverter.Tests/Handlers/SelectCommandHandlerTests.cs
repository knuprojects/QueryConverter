using QueryConverter.Core.Handlers;
using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Core.Utils;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Tests.Templates;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Tests.Handlers
{
    public class SelectCommandHandlerTests
    {
        private readonly ICommandHandler<SelectCommand, ResultModel> _selectCommandHandler;
        private readonly ICondition _condition;

        public SelectCommandHandlerTests(ICondition condition)
        {
            _condition = condition;
            _selectCommandHandler = new SelectCommandHandler(_condition);
        }

        [Fact]
        public async Task HandleSelectStatementWithFilter_Should_Return_Valid_Query()
        {
            var command = new SelectCommand()
            {
                SQLQuery = QueryTemplate.SelectWithFilter
            };

            var result = await _selectCommandHandler.HandleAsync(command);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task HandleSelectStatementWithFilters_Should_Return_Valid_Query()
        {
            var command = new SelectCommand()
            {
                SQLQuery = QueryTemplate.SelectWithFilters
            };

            var result = await _selectCommandHandler.HandleAsync(command);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task HandleSelectStatementWithFilter_Should_Be_Exception()
        {
            var command = new SelectCommand()
            {
                SQLQuery = QueryTemplate.InvalidSelectWithFilter
            };

            var result = _selectCommandHandler.HandleAsync(command);

            result.Exception.Should().NotBeNull();
            //Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }

        [Fact]
        public async Task HandleSelectStatementWithFilters_Should_Be_Exception()
        {
            var command = new SelectCommand()
            {
                SQLQuery = QueryTemplate.InvalidSelectWithFilters
            };

            var result = _selectCommandHandler.HandleAsync(command);

            result.Exception.Should().NotBeNull();
            Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }
    }
}
