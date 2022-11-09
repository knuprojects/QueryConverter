using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Presentation.Handlers
{
    public class OrderByCommand : ICommand<ResultModel>
    {
        public string SQLQuery { get; set; }
    }
}
