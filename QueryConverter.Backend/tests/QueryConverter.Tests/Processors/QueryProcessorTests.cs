using QueryConverter.Core.Processor;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Tests.Templates;
using QueryConverter.Types.Shared.Dto;

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
            result.Should().BeOfType<ResultModel>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ProccesQuerySelectWithFilters_Should_Return_Valid_Query()
        {
            // Act
            var result = await _queryProcessor.ProcessSqlQuery(QueryTemplate.SelectWithFilters);

            // Assert
            result.Should().BeOfType<ResultModel>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ProccesQuerySelectWithGroupBy_Should_Return_Valid_Query()
        {
            // Act
            var result = await _queryProcessor.ProcessSqlQuery(QueryTemplate.SelectWithFilterAndGroupBy);

            // Assert
            result.Should().BeOfType<ResultModel>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void ProccesQuery_Should_Be_Exception()
        {
            // Arrange 
            string template = "fdsfsdfsfsdfsdfsfsfs";

            // Act
            var result = _queryProcessor.ProcessSqlQuery(template);

            // Assert
            result.Exception.Should().NotBeNull();
            Assert.ThrowsAnyAsync<QueryConverterException>(() => result);
        }
    }
}
