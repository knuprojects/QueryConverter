using QueryConverter.Core.Handlers;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Tests.Templates;
using QueryConverter.Tests.Utils;
using System.Threading.Tasks;
using Xunit;

namespace QueryConverter.Tests.Handlers
{
    // Base test, Assert should be Fluent Assertions in the future
    public class QueryHandlerTests
    {
        private readonly IQueryHandler _queryHandler;

        public QueryHandlerTests(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [Fact]
        public async Task HandleSelectStatement_Should_Return_Valid_Query()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.SelectWithFilter);

            // Act
             var result = await _queryHandler.HandleSelectStatement(statement);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HandleSelectStatement_Should_Be_Exception()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.InvalidSelectWithFilter);

            // Act
            var result = _queryHandler.HandleSelectStatement(statement);

            // Assert
            Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }

        [Fact]
        public async Task HandleGroupByStatement_Should_Return_Valid_Query()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.SelectWithFilterAndGroupBy);

            // Act
            var result = await _queryHandler.HandleGroupByStatement(statement);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HandleGroupByStatement_Should_Be_Exception()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.InvalidSelectWithFilterAndGroupBy);

            // Act
            var result = _queryHandler.HandleGroupByStatement(statement);

            // Assert
            Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }
    }
}
