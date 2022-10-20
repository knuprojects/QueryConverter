using QueryConverter.Core.Processor;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Tests.Templates;

namespace QueryConverter.Tests.Processors
{
    public class QueryProcessorTests
    {
        private readonly IQueryProcessor _queryProcessor;

        public QueryProcessorTests(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        [Fact]
        public async Task ProccesQuerySelectWithFilter_Should_Return_Valid_Query()
        {
            // Act
            var result = await _queryProcessor.ProcessSqlQuery(QueryTemplate.SelectWithFilter);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProccesQuerySelectWithFilters_Should_Return_Valid_Query()
        {
            // Act
            var result = await _queryProcessor.ProcessSqlQuery(QueryTemplate.SelectWithFilters);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProccesQuerySelectWithGroupBy_Should_Return_Valid_Query()
        {
            // Act
            var result = await _queryProcessor.ProcessSqlQuery(QueryTemplate.SelectWithFilterAndGroupBy);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ProccesQuery_Should_Be_Exception()
        {
            // Arrange 
            string template = "fdsfsdfsfsdfsdfsfsfs";

            // Act
            var result = _queryProcessor.ProcessSqlQuery(template);

            // Assert
            Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }
    }
}
