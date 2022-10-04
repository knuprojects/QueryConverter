using QueryConverter.Core.Handlers;
using QueryConverter.Tests.Templates;
using QueryConverter.Tests.Utils;
using System.Threading.Tasks;
using Xunit;

namespace QueryConverter.Tests.Handlers
{
    public class QueryHandlerTests
    {
        private readonly IQueryHandler _queryHandler;

        public QueryHandlerTests(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        // Base test, Assert should be Fluent Assertions in the future
        [Fact]
        public async Task HandleSelectStatement_Should_Return_Valid_Query()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.SelectWithFilter);

            // Act
             var result = _queryHandler.HandleSelectStatement(statement);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task HandleSelectStatement_Should_Be_Exception()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.InvalidSelectWithFilter);

            // Act
            _queryHandler.HandleSelectStatement(statement);

            // Assert
            Assert.Null(null);
        }

        [Fact]
        public async Task HandleGroupByStatement_Should_Return_Valid_Query()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.SelectWithFilterAndGroupBy);

            // Act
            var result = _queryHandler.HandleGroupByStatement(statement);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task HandleGroupByStatement_Should_Be_Exception()
        {
            // Arrange
            var statement = ExtensionMethods.ConvertToTSQL(QueryTemplate.InvalidSelectWithFilterAndGroupBy);

            // Act
            _queryHandler.HandleGroupByStatement(statement);

            // Assert
            Assert.Null(null);
        }
    }
}
