using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Core.Handlers
{
    public class OrderByCommand : ICommand<ResultModel>
    {
        public string SQLQuery { get; set; }
    }
}
